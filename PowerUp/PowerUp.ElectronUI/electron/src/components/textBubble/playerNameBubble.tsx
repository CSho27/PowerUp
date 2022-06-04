import { PropsWithChildren } from "react";
import styled from "styled-components";
import { EntitySourceType } from "../../app/shared/entitySourceType";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { textOutline } from "../../style/outlineHelper";
import { toIdentifier } from "../../utils/getIdentifier";
import { FlyoutAnchor, useFlyoutState } from "../flyout/flyout";
import { Icon } from "../icon/icon";
import { SourceTypeStamp } from "../sourceTypeStamp/sourceTypeStamp";
import { TextBubble, TextBubbleProps } from "./textBubble";

export interface PlayerNameBubbleProps extends TextBubbleProps {
  playerId: number;
  sourceType: EntitySourceType;
  removeInfoFlyout?: boolean;
  title?: string;
}

export function PlayerNameBubble(props: PropsWithChildren<PlayerNameBubbleProps>) {
  const { size, sourceType, playerId, removeInfoFlyout, children } = props;
  const isLarge = size === 'Large'

  return <NameBubble {...props}>
    <PlayerNameSection>{children}</PlayerNameSection>
    <PlayerInfoSection isLarge={isLarge}>{toIdentifier('Player', playerId)}</PlayerInfoSection>
    <SourceTypeStamp
      theme='Light'
      size={isLarge
        ? 'Medium'
        : 'Small'}
      sourceType={sourceType} />
    {!removeInfoFlyout && 
    <PlayerInfoSection isLarge={isLarge}>
      <FlyoutAnchor 
        {...useFlyoutState()}
        trigger='click'
        flyout={<div style={{ padding: '16px', width: '200px', height: '200px' }}>
          This is a flyout!
        </div>}>
          <Icon icon='circle-info' />  
      </FlyoutAnchor>
    </PlayerInfoSection>}
  </NameBubble>
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