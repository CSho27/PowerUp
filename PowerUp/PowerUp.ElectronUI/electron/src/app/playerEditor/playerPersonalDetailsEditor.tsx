import { Dispatch } from "react";
import styled from "styled-components"
import { CheckboxField } from "../../components/checkboxField/checkboxField";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { digits, powerProsCharacters, TextField } from "../../components/textField/textField"
import { FONT_SIZES } from "../../style/constants";
import { PlayerPersonalDetailsAction } from "./playerEditorState";

export interface PlayerPersonalDetailsEditorProps {
  firstName: string;
  lastName: string;
  initiallyHadSpecialSavedName: boolean;
  hasSpecialSavedName: boolean;
  savedName: string;
  uniformNumber: string;
  update: Dispatch<PlayerPersonalDetailsAction>;
}

export function PlayerPersonalDetailsEditor(props: PlayerPersonalDetailsEditorProps) {
  const { 
    firstName, 
    lastName, 
    initiallyHadSpecialSavedName,
    hasSpecialSavedName, 
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
          maxLength={14}
          allowedCharacters={powerProsCharacters}
          onChange={firstName => update({ type: 'updateFirstName', firstName: firstName })}
        />
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Last Name</FieldLabel>
        <TextField 
          value={lastName}
          maxLength={14}
          allowedCharacters={powerProsCharacters}
          onChange={lastName => update({ type: 'updateLastName', lastName: lastName })}
        />
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FlexRow gap='8px' vAlignCenter>
          <FieldLabel>Saved Name</FieldLabel>
          {initiallyHadSpecialSavedName &&
          <FlexRow gap='4px' vAlignCenter style={{ flex: 'auto' }}>
            <CheckboxField 
              checked={hasSpecialSavedName}
              size='Small'
              onChecked={() => update({ type: 'toggleUseSpecialSavedName' })}
            />
            <span style={{ fontSize: FONT_SIZES._14 }}>Use Special Saved Name</span>
          </FlexRow>}
        </FlexRow>
        <TextField 
          value={savedName}
          maxLength={10}
          allowedCharacters={powerProsCharacters}
          disabled={hasSpecialSavedName}
          onChange={savedName => update({ type: 'updateSavedName', savedName: savedName })}
        />
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Uniorm Number</FieldLabel>
        <TextField 
          value={uniformNumber}
          maxLength={3}
          allowedCharacters={digits}
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

const FlexRow = styled.div<{ gap: FlexRowGap, vAlignCenter?: boolean }>`
  display: flex;
  gap: ${p => p.gap};
  align-items: ${p => p.vAlignCenter ? 'center' : undefined};
`

type FlexFrac = 
| '1/4'

const FlexFracItem = styled.div<{ frac: FlexFrac }>`
  flex: 1 1 calc(100% * ${p => `(${p.frac})`});
`