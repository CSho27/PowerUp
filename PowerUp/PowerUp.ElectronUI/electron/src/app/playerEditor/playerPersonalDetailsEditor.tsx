import { Dispatch } from "react";
import styled from "styled-components"
import { CheckboxField } from "../../components/checkboxField/checkboxField";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { SelectField } from "../../components/SelectField/selectField";
import { toKeyedCode, toOption, toOptions } from "../../components/SelectField/selectFieldHelpers";
import { digits, powerProsCharacters, TextField } from "../../components/textField/textField"
import { FONT_SIZES } from "../../style/constants";
import { PlayerEditorOptions } from "./loadPlayerEditorApiClient";
import { PlayerPersonalDetails, PlayerPersonalDetailsAction } from "./playerEditorState";

export interface PlayerPersonalDetailsEditorProps {
  options: PlayerEditorOptions;
  initiallyHadSpecialSavedName: boolean;
  details: PlayerPersonalDetails;
  update: Dispatch<PlayerPersonalDetailsAction>;
}

export function PlayerPersonalDetailsEditor(props: PlayerPersonalDetailsEditorProps) {
  const { 
    options,
    initiallyHadSpecialSavedName,
    details,
    update
  } = props;
  
  return <PersonalDetailsEditorContainer>
    <FlexRow gap='8px'>
      <FlexFracItem frac='1/4'>
        <FieldLabel>First Name</FieldLabel>
        <TextField 
          value={details.firstName}
          maxLength={14}
          allowedCharacters={powerProsCharacters}
          onChange={firstName => update({ type: 'updateFirstName', firstName: firstName })}
        />
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Last Name</FieldLabel>
        <TextField 
          value={details.lastName}
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
              checked={details.useSpecialSavedName}
              size='Small'
              onChecked={() => update({ type: 'toggleUseSpecialSavedName' })}
            />
            <span style={{ fontSize: FONT_SIZES._14 }}>Use Special Saved Name</span>
          </FlexRow>}
        </FlexRow>
        <TextField 
          value={details.savedName}
          maxLength={10}
          allowedCharacters={powerProsCharacters}
          disabled={details.useSpecialSavedName}
          onChange={savedName => update({ type: 'updateSavedName', savedName: savedName })}
        />
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Uniorm Number</FieldLabel>
        <TextField 
          value={details.uniformNumber}
          maxLength={3}
          allowedCharacters={digits}
          onChange={uniformNumber => update({ type: 'updateUniformNumber', uniformNumber: uniformNumber })}
        />
      </FlexFracItem>
    </FlexRow>
    <FlexRow gap='8px'>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Primary Position</FieldLabel>
        <SelectField 
          value={details.position?.key} 
          onChange={position => update({ type: 'updatePosition', position: toKeyedCode(options.positions, position)})} 
        >
          {toOptions(options.positions)}
        </SelectField>
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Pitcher Type</FieldLabel>
        <SelectField 
          value={details.pitcherType?.key} 
          onChange={pitcherType => update({ type: 'updatePitcherType', pitcherType: toKeyedCode(options.pitcherTypes, pitcherType)})} 
        >
          {toOptions(options.pitcherTypes)}
        </SelectField>
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
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
  --width: calc(100% * ${p => `(${p.frac})`});
  width: var(--width);
  flex: 0 1 var(--width);
`