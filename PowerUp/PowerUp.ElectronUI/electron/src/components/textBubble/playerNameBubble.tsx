import { PropsWithChildren } from "react";
import styled from "styled-components";
import { EntitySourceType } from "../../app/shared/entitySourceType";
import { COLORS } from "../../style/constants";
import { textOutline } from "../../style/outlineHelper";
import { TextBubble, TextBubbleProps } from "./textBubble";

export interface PlayerNameBubbleProps extends TextBubbleProps {
  sourceType: EntitySourceType;
}

export function PlayerNameBubble(props: PropsWithChildren<PlayerNameBubbleProps>) {
  const { sourceType, children } = props;
  
  return <NameBubble {...props}>
    <PlayerNameSection>{children}</PlayerNameSection>
    <SourceTypeStamp title={getSourceTypeTooltip(sourceType)}>{sourceType}</SourceTypeStamp>
  </NameBubble>
}

const NameBubble = styled(TextBubble)`
  display: flex;
  align-items: center;
  gap: 16px;
`

const PlayerNameSection = styled.div`
  flex: 1 1 auto;
  ${textOutline('1px', COLORS.richBlack.regular_5)}
  text-align: left;
  white-space: nowrap;
  text-overflow: ellipsis;
`

const SourceTypeStamp = styled.div`
  flex: 0 0 auto;
  font-size: .8em;
  line-height: 1;
  color: ${COLORS.white.regular_100};
  border: solid 2px ${COLORS.white.regular_100};
  border-radius: 2px;
  cursor: default;
`

function getSourceTypeTooltip(sourceType: EntitySourceType): string {
  switch(sourceType) {
    case 'Base':
      return 'Included in the original version of the game. Cannot be edited.';
    case 'Imported':
      return 'Imported from a Game Same file. Cannot be edited.';
    case 'Custom':
      return 'Created through the app. Fully editable.';
    case 'Generated':
      return 'Created using Autogeneration. Fully editable.';
  }
}
