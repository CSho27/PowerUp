import { Dispatch } from "react";
import styled from "styled-components"
import { Button } from "../../components/button/button";
import { CheckboxField } from "../../components/checkboxField/checkboxField";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { SelectField } from "../../components/SelectField/selectField";
import { toKeyedCode, toOption, toOptions, toSimpleCode } from "../../components/SelectField/selectFieldHelpers";
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
    <FlexRow gap='16px' withBottomPadding>
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
        <FlexRow gap='16px' vAlignCenter>
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
    <FlexRow gap='16px' withBottomPadding>
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
          disabled={details.position.key !== 'Pitcher'}
          onChange={pitcherType => update({ type: 'updatePitcherType', pitcherType: toKeyedCode(options.pitcherTypes, pitcherType)})} 
        >
          {toOptions(options.pitcherTypes)}
        </SelectField>
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Voice</FieldLabel>
        <SelectField 
          value={details.voice?.id} 
          onChange={voice => update({ type: 'updateVoice', voice: toSimpleCode(options.voiceOptions, voice)})}
        >
          {toOptions(options.voiceOptions)}
        </SelectField>
      </FlexFracItem>
      <FlexFracItem frac='1/4' style={{ display: 'flex', alignItems: 'flex-end' }}>
        <Button variant='Fill' size='Small' onClick={() =>  console.log('Find Closest Clicked')}>
          Find Closest
        </Button>
      </FlexFracItem>
    </FlexRow>
    <FlexRow gap='16px' withBottomPadding>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Bats</FieldLabel>
        <SelectField 
          value={details?.battingSide.key} 
          onChange={key => update({ type: 'updateBattingSide', battingSide: toKeyedCode(options.battingSideOptions, key)})} 
        >
          {toOptions(options.battingSideOptions)}
        </SelectField>
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Batting Stance</FieldLabel>
        <SelectField 
          value={details.battingStance?.id} 
          onChange={id => update({ type: 'updateBattingStance', battingStance: toSimpleCode(options.battingStanceOptions, id)})} 
        >
          {toOptions(options.battingStanceOptions)}
        </SelectField>
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Throws</FieldLabel>
        <SelectField 
          value={details.throwingArm?.key}
          onChange={throwingArm => update({ type: 'updateThrowingArm', throwingArm: toKeyedCode(options.throwingArmOptions, throwingArm)})}
        >
          {toOptions(options.throwingArmOptions)}
        </SelectField>
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Pitching Mechanics</FieldLabel>
        <SelectField 
          value={details.pitchingMechanics?.id} 
          onChange={id => update({ type: 'updatePitchingMechanics', mechanics: toSimpleCode(options.pitchingMechanicsOptions, id)})} 
        >
          {toOptions(options.pitchingMechanicsOptions)}
        </SelectField>
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

const FlexRow = styled.div<{ gap: FlexRowGap, withBottomPadding?: boolean, vAlignCenter?: boolean }>`
  display: flex;
  gap: ${p => p.gap};
  align-items: ${p => p.vAlignCenter ? 'center' : undefined};
  padding-bottom: ${p => p.withBottomPadding ? '16px' : undefined};
`

type FlexFrac = 
| '1/4'

const FlexFracItem = styled.div<{ frac: FlexFrac }>`
  --width: calc(100% * ${p => `(${p.frac})`});
  width: var(--width);
  flex: 0 1 var(--width);
`