import { PropsWithChildren } from "react";
import styled from "styled-components";
import { EntitySourceType } from "../../app/shared/entitySourceType";
import { COLORS } from "../../style/constants";
import { textOutline } from "../../style/outlineHelper";
import { SourceTypeStamp } from "../sourceTypeStamp/sourceTypeStamp";
import { TextBubble, TextBubbleProps } from "./textBubble";

export interface PlayerNameBubbleProps extends TextBubbleProps {
  sourceType: EntitySourceType;
  title?: string;
}

export function PlayerNameBubble(props: PropsWithChildren<PlayerNameBubbleProps>) {
  const { size, sourceType, children } = props;
  
  return <NameBubble {...props}>
    <PlayerNameSection>{children}</PlayerNameSection>
    <SourceTypeStamp
      theme='Light'
      size={size === 'Large'
        ? 'Medium'
        : 'Small'}
      sourceType={sourceType} />
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