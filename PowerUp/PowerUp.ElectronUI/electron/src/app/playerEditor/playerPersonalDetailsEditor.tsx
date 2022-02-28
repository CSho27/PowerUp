import { Dispatch } from "react";
import styled from "styled-components"
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { TextField } from "../../components/textField/textField"
import { PlayerPersonalDetailsAction } from "./playerEditorState";

export interface PlayerPersonalDetailsEditorProps {
  firstName: string;
  lastName: string;
  isSpecialSavedName: boolean;
  savedName: string;
  uniformNumber: string;
  update: Dispatch<PlayerPersonalDetailsAction>;
}

export function PlayerPersonalDetailsEditor(props: PlayerPersonalDetailsEditorProps) {
  const { 
    firstName, 
    lastName, 
    isSpecialSavedName, 
    savedName, 
    uniformNumber,
    update 
  } = props;
  
  return <PersonalDetailsEditorContainer>
    <FlexRow gap='8px'>
      <FlexFracItem frac='1/4'>
        <FieldLabel>First Name</FieldLabel>
        <TextField 
          value={firstName}
          onChange={firstName => update({ type: 'updateFirstName', firstName: firstName })}
        />
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Last Name</FieldLabel>
        <TextField 
          value={lastName}
          onChange={lastName => update({ type: 'updateLastName', lastName: lastName })}
        />
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Saved Name</FieldLabel>
        <TextField 
          value={savedName}
          onChange={savedName => update({ type: 'updateSavedName', savedName: savedName })}
        />
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Uniorm Number</FieldLabel>
        <TextField 
          value={uniformNumber}
          onChange={uniformNumber => update({ type: 'updateUniformNumber', uniformNumber: uniformNumber })}
        />
      </FlexFracItem>
    </FlexRow>
  </PersonalDetailsEditorContainer>
}

const PersonalDetailsEditorContainer = styled.div`
  padding: 16px;
`

type FlexRowGap =
| '4px'
| '8px'
| '16px'

const FlexRow = styled.div<{ gap: FlexRowGap }>`
  display: flex;
  gap: ${p => p.gap};
`

type FlexFrac = 
| '1/4'

const FlexFracItem = styled.div<{ frac: FlexFrac }>`
  flex: 1 1 calc(100% * ${p => `(${p.frac})`});
`