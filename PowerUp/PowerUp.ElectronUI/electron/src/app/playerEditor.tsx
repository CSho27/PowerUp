import styled from "styled-components"
import { ContentBox } from "../components/contentBox.tsx/contentBox";
import { FieldLabel } from "../components/fieldLabel/fieldLabel";
import { OutlineHeader } from "../components/outlineHeader/outlineHeader"
import { PlayerName, Position, TextBubble } from "../components/textBubble/textBubble"
import { COLORS } from "../style/constants"

export interface PlayerEditorProps {
  powerProsId: number;
  firstName: string;
  lastName: string;
  savedName: string;
  position: string;
  playerNumber: string;
}

export function PlayerEditor(props: PlayerEditorProps) {
  const { powerProsId, firstName, lastName, savedName, position, playerNumber } = props;

  return <>
    <HeaderWrapper>
    <OutlineHeader textColor={COLORS.PP_Blue70} strokeColor={COLORS.PP_Blue45} slanted>Edit Player</OutlineHeader>
    <TextBubble positionType='Infielder' width='250px' style={{ position: 'relative', bottom: '-2px' }}>
      <PlayerName>{savedName}</PlayerName>
    </TextBubble>
    <TextBubble positionType='Infielder' style={{ position: 'relative', bottom: '-2px' }}>
      <Position>{position}</Position>
    </TextBubble>
    <OutlineHeader textColor={COLORS.PP_Blue45} strokeColor={COLORS.white}>{playerNumber}</OutlineHeader>
  </HeaderWrapper>
  <ContentBox>
    <FieldLabel>First Name</FieldLabel>
  </ContentBox>
  </>
}

const HeaderWrapper = styled.div`
  display: flex;
  align-items: center;
  gap: 24px;
`