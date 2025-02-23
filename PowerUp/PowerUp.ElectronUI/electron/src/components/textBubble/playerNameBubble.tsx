import { PropsWithChildren, useRef } from "react";
import styled from "styled-components";
import { AppContext } from "../../app/appContext";
import { GetPlayerFlyoutDetailsApiClient } from "../../app/playerDetailsFlyout/getPlayerFlyoutDetailsApiClient";
import { PlayerDetailsFlyout } from "../../app/playerDetailsFlyout/playerDetailsFlyout";
import { EntitySourceType } from "../../app/shared/entitySourceType";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { textOutline } from "../../style/outlineHelper";
import { toIdentifier } from "../../utils/getIdentifier";
import { FlyoutAnchor, useFlyoutState } from "../flyout/flyout";
import { Icon } from "../icon/icon";
import { SourceTypeStamp } from "../sourceTypeStamp/sourceTypeStamp";
import { TextBubble, TextBubbleProps } from "./textBubble";

export interface PlayerNameBubbleProps extends TextBubbleProps {
  appContext: AppContext;
  playerId: number;
  sourceType: EntitySourceType;
  withoutInfoFlyout?: boolean;
  withoutPID?: boolean;
  withoutSourceType?: boolean;
  title?: string;
}

export function PlayerNameBubble(props: PropsWithChildren<PlayerNameBubbleProps>) {
  const { appContext, size, sourceType, playerId, withoutInfoFlyout, children, withoutPID, withoutSourceType } = props;
  const isLarge = size === 'Large'

  const apiClientRef = useRef(new GetPlayerFlyoutDetailsApiClient(appContext.commandFetcher))

  return <NameBubble {...props}>
    <PlayerNameSection>{children}</PlayerNameSection>
    {!withoutPID && <PlayerInfoSection isLarge={isLarge}>{toIdentifier('Player', playerId)}</PlayerInfoSection>}
    {!withoutSourceType && <SourceTypeStamp
      theme='Light'
      size={isLarge
        ? 'Medium'
        : 'Small'}
      sourceType={sourceType} />}
    {!withoutInfoFlyout && 
    <PlayerInfoSection isLarge={isLarge}>
      <FlyoutAnchor 
        {...useFlyoutState()}
        trigger='click'
        withoutBackground
        borderRadius={'16px'}
        flyout={getPlayerDetailsFlyout}>
          <Icon icon='circle-info' />  
      </FlyoutAnchor>
    </PlayerInfoSection>}
  </NameBubble>

  async function getPlayerDetailsFlyout() {
    const response = await apiClientRef.current.execute({ playerId: playerId });
    return <PlayerDetailsFlyout appContext={appContext} response={response} />
  }
}

const NameBubble = styled(TextBubble)`
  display: flex;
  align-items: center;
  gap: 8px;
`

const PlayerInfoSection = styled.div<{ isLarge: boolean }>`
  font-size: ${p => p.isLarge ? FONT_SIZES._24 : FONT_SIZES.default_16};
`

const PlayerNameSection = styled.div`
  flex: 1 1 auto;
  ${textOutline('1px', COLORS.richBlack.regular_5)}
  text-align: left;
  white-space: nowrap;
  text-overflow: ellipsis;
`