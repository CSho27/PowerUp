import { Dispatch, useRef } from "react";
import { Button } from "../../components/button/button";
import { CheckboxField } from "../../components/checkboxField/checkboxField";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow";
import { NumberField } from "../../components/numberField/numberField";
import { SelectField } from "../../components/SelectField/selectField";
import { fromOptions, toOptions } from "../../components/SelectField/selectFieldHelpers";
import { digits, powerProsCharacters, TextField } from "../../components/textField/textField"
import { ToggleSwitch } from "../../components/toggleSwitch/toggleSwitch";
import { FONT_SIZES } from "../../style/constants";
import { AppContext } from "../app";
import { FindClosestVoiceApiClient } from "./findClosestVoiceApiClient";
import { PersonalDetailsOptions } from "./loadPlayerEditorApiClient";
import { PlayerPersonalDetails, PlayerPersonalDetailsAction } from "./playerEditorState";

export interface PlayerPersonalDetailsEditorProps {
  appContext: AppContext;
  options: PersonalDetailsOptions;
  initiallyHadSpecialSavedName: boolean;
  details: PlayerPersonalDetails;
  disabled?: boolean;
  update: Dispatch<PlayerPersonalDetailsAction>;
}

export function PlayerPersonalDetailsEditor(props: PlayerPersonalDetailsEditorProps) {
  const { 
    appContext,
    options,
    initiallyHadSpecialSavedName,
    details,
    disabled: editorDisabled,
    update
  } = props;

  const voiceApiClient = useRef(new FindClosestVoiceApiClient(appContext.commandFetcher));
  
  return <>
    <FlexRow gap='16px' vAlignCenter withBottomPadding>
      <FieldLabel htmlFor='personal-is-custom-player'>Custom Player</FieldLabel>
      <ToggleSwitch 
        id='personal-is-custom-player'
        isOn={details.isCustomPlayer}
        onToggle={() => update({ type: 'toggleIsCustomPlayer' })}
        disabled={editorDisabled}
      />
    </FlexRow>
    <FlexRow gap='16px' withBottomPadding>
      <FlexFracItem frac='1/4'>
        <FieldLabel>First Name</FieldLabel>
        <TextField 
          value={details.firstName}
          maxLength={14}
          allowedCharacters={powerProsCharacters}
          disabled={editorDisabled}
          onChange={firstName => update({ type: 'updateFirstName', firstName: firstName })}
        />
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Last Name</FieldLabel>
        <TextField 
          value={details.lastName}
          maxLength={14}
          allowedCharacters={powerProsCharacters}
          disabled={editorDisabled}
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
              disabled={editorDisabled}
              onToggle={() => update({ type: 'toggleUseSpecialSavedName' })}
            />
            <span style={{ fontSize: FONT_SIZES._14 }}>Use Special</span>
          </FlexRow>}
        </FlexRow>
        <TextField 
          value={details.savedName}
          maxLength={10}
          allowedCharacters={powerProsCharacters}
          disabled={editorDisabled || details.useSpecialSavedName}
          onChange={savedName => update({ type: 'updateSavedName', savedName: savedName })}
        />
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Uniform Number</FieldLabel>
        <TextField 
          value={details.uniformNumber}
          maxLength={3}
          allowedCharacters={digits}
          disabled={editorDisabled}
          onChange={uniformNumber => update({ type: 'updateUniformNumber', uniformNumber: uniformNumber })}
        />
      </FlexFracItem>
    </FlexRow>
    <FlexRow gap='16px' withBottomPadding>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Primary Position</FieldLabel>
        <SelectField 
          value={details.position?.key} 
          disabled={editorDisabled}
          onChange={position => update({ type: 'updatePosition', position: fromOptions(options.positions, position) })} 
        >
          {toOptions(options.positions)}
        </SelectField>
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Pitcher Type</FieldLabel>
        <SelectField 
          value={details.pitcherType?.key} 
          disabled={editorDisabled || details.position.key !== 'Pitcher'}
          onChange={pitcherType => update({ type: 'updatePitcherType', pitcherType: fromOptions(options.pitcherTypes, pitcherType)})} 
        >
          {toOptions(options.pitcherTypes)}
        </SelectField>
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Voice</FieldLabel>
        <SelectField 
          value={details.voice?.id} 
          disabled={editorDisabled}
          onChange={voice => update({ type: 'updateVoice', voice: fromOptions(options.voiceOptions, voice)})}
        >
          {toOptions(options.voiceOptions)}
        </SelectField>
      </FlexFracItem>
      <FlexFracItem frac='1/4' style={{ display: 'flex', alignItems: 'flex-end' }}>
        <Button 
          variant='Fill' 
          size='Small'
          onClick={findClosestVoice} 
          disabled={editorDisabled}>
            Find Closest
        </Button> 
      </FlexFracItem>
    </FlexRow>
    <FlexRow gap='16px' withBottomPadding>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Bats</FieldLabel>
        <SelectField 
          value={details?.battingSide.key} 
          disabled={editorDisabled}
          onChange={key => update({ type: 'updateBattingSide', battingSide: fromOptions(options.battingSideOptions, key)})} 
        >
          {toOptions(options.battingSideOptions)}
        </SelectField>
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Batting Stance</FieldLabel>
        <SelectField 
          value={details.battingStance?.id}
          disabled={editorDisabled} 
          onChange={id => update({ type: 'updateBattingStance', battingStance: fromOptions(options.battingStanceOptions, id)})} 
        >
          {toOptions(options.battingStanceOptions)}
        </SelectField>
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Throws</FieldLabel>
        <SelectField 
          value={details.throwingArm?.key}
          disabled={editorDisabled}
          onChange={throwingArm => update({ type: 'updateThrowingArm', throwingArm: fromOptions(options.throwingArmOptions, throwingArm)})}
        >
          {toOptions(options.throwingArmOptions)}
        </SelectField>
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Pitching Mechanics</FieldLabel>
        <SelectField 
          value={details.pitchingMechanics?.id} 
          disabled={editorDisabled}
          onChange={id => update({ type: 'updatePitchingMechanics', mechanics: fromOptions(options.pitchingMechanicsOptions, id)})} 
        >
          {toOptions(options.pitchingMechanicsOptions)}
        </SelectField>
      </FlexFracItem>
    </FlexRow>
    
    <FlexRow gap='16px' withBottomPadding>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Birth Date</FieldLabel>
        <div style={{ display: 'flex', gap: '4px' }}>
          <NumberField 
            type='Defined'
            value={details.birthMonth}
            min={1}
            max={12}
            disabled={editorDisabled}
            onChange={value => update({ type: 'updateBirthMonth', birthMonth: value })}
          />
          <span>/</span>
          <NumberField 
            type='Defined'
            value={details.birthDay}
            min={1}
            max={getDaysForMonth(details.birthMonth)}
            disabled={editorDisabled}
            onChange={value => update({ type: 'updateBirthDay', birthDay: value })}
          />
        </div>
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Age</FieldLabel>
        <NumberField 
          type='Defined'
          value={details.age}
          min={18 + details.yearsInMajors}
          max={60}
          disabled={editorDisabled}
          onChange={value => update({ type: 'updateAge', age: value })}
        />
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
      <FieldLabel>Years in Majors</FieldLabel>
        <NumberField 
          type='Defined'
          value={details.yearsInMajors}
          min={0}
          max={details.age - 18}
          disabled={editorDisabled}
          onChange={value => update({ type: 'updateYearsInMajors', yearsInMajors: value })}
        />
      </FlexFracItem>
    </FlexRow>
    <h3>Statistics</h3>
    <FlexRow gap='16px' withBottomPadding>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Batting Average</FieldLabel>
        <NumberField 
          type='PossiblyUndefined'
          value={details.battingAverage}
          disabled={editorDisabled}
          stepSize={.01}
          min={0}
          max={1}
          decimalPlaces={3}
          leadingDecimal
          placeholder='.---'
          onChange={value => update({ type: 'updateBattingAverage', battingAverage: value })}
        /> 
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>RBI</FieldLabel>
        <NumberField 
          type='PossiblyUndefined'
          value={details.runsBattedIn}
          disabled={editorDisabled}
          stepSize={1}
          min={0}
          placeholder='---'
          onChange={value => update({ type: 'updateRunsBattedIn', runsBattedIn: value })}
        /> 
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>Home Runs</FieldLabel>
        <NumberField 
          type='PossiblyUndefined'
          value={details.homeRuns}
          disabled={editorDisabled}
          stepSize={1}
          min={0}
          placeholder='---'
          onChange={value => update({ type: 'updateHomeRuns', homeRuns: value })}
        /> 
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <FieldLabel>ERA</FieldLabel>
        <NumberField 
          type='PossiblyUndefined'
          value={details.earnedRunAverage}
          disabled={editorDisabled}
          stepSize={.01}
          min={0}
          decimalPlaces={2}
          placeholder='.---'
          onChange={value => update({ type: 'updateEarnedRunAverage', earnedRunAverage: value })}
        /> 
      </FlexFracItem>
    </FlexRow>
  </>

  async function findClosestVoice() {
    const response = await voiceApiClient.current.execute({ 
      firstName: details.firstName,
      lastName: details.lastName
    });

    update({ type: 'updateVoice', voice: fromOptions(options.voiceOptions, response.id.toString())})
  }

  // For the purposes of picking birthdays we're going to allow up to 30 days in february always to account for leap years
  function getDaysForMonth(month: number): number {
    const monthsWith31Days = [1,3,5,7,8,10,12]
    return monthsWith31Days.some(m => m === month)
      ? 31
      : 30
  }
}