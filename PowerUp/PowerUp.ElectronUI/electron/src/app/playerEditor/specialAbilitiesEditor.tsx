import { Dispatch } from "react";
import styled from "styled-components";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { FlexRow } from "../../components/flexRow/flexRow";
import { KeyedCode } from "../shared/keyedCode";
import { SpecialAbilitiesOptions } from "./loadPlayerEditorApiClient";
import { getBaseRunningSpecialAbilitiesReducer, getFieldingSpecialAbilitiesReducer, getGeneralSpecialAbilitiesReducer, getHittingApproachSpecialAbilitiesReducer, getPitchingDemeanorSpecialAbilitiesReducer, getPitchingMechanicsSpecialAbilitiesReducer, getPitchQualitiesSpecialAbilitiesReducer, getSituationalHittingSpecialAbilitiesReducer, getSituationalPitchingSpecialAbilitiesReducer, getSmallBallSpecialAbilitiesReducer, SpecialAbilities, SpecialAbilitiesAction } from "./specialAbilitiesState"
import { SpecialAbilityEffect, SpecialAbilityRadioButton } from "./specialAbilityRadioButton";

export interface SpecialAbilitiesEditorProps {
  options: SpecialAbilitiesOptions;
  details: SpecialAbilities;
  isPitcher: boolean;
  update: Dispatch<SpecialAbilitiesAction>;
}

export function SpecialAbilitiesEditor(props: SpecialAbilitiesEditorProps) {
  const { options, details, isPitcher, update } = props;
  const [general, updateGeneral] = getGeneralSpecialAbilitiesReducer(details, update);

  return <>
    <SpecialAbilitiesSection>
      <h2>General</h2>
      <SpecialBooleanRow 
        label='Star'
        value={general.isStar}
        onChange={value => updateGeneral({ type: 'updateIsStar', isStar: value })}
        />
      <SpecialNumericRow
        label='Durability'
        value={general.durability}
        options={options.special2_4Options}
        onChange={value => updateGeneral({ type: 'updateDurability', durability: value })}
        />
      <SpecialPositiveNegativeRow
        positiveLabel='Gd Morale'
        negativeLabel='Pr Morale'
        options={options.specialPositive_NegativeOptions}
        value={general.morale}
        onChange={value => updateGeneral({ type: 'updateMorale', morale: value })}
        />
    </SpecialAbilitiesSection>
    {!isPitcher && getHitterSection()}
    {!isPitcher && getPitcherSection()}
    {isPitcher && getPitcherSection()}
    {isPitcher && getHitterSection()}
  </>

  function getHitterSection() {
    const [situationalHitting, updateSituationalHitting] = getSituationalHittingSpecialAbilitiesReducer(details, update);
    const [approach, updateApproach] = getHittingApproachSpecialAbilitiesReducer(details, update);
    const [smallBall, updateSmallBall] = getSmallBallSpecialAbilitiesReducer(details, update);
    const [baseRunning, updateBaseRunning] = getBaseRunningSpecialAbilitiesReducer(details, update);
    const [fielding, updateFielding] = getFieldingSpecialAbilitiesReducer(details, update);

    return <SpecialAbilitiesSection>
      <h2>Hitter</h2>
      <h3>Situational</h3>
      <SpecialNumericRow
        label='Consistency'
        value={situationalHitting.hittingConsistency}
        options={options.special2_4Options}
        onChange={value => updateSituationalHitting({ type: 'updateConsistency', consistency: value })}
      />
      <SpecialNumericRow
        label='Vs Lefty'
        value={situationalHitting.versusLefty}
        options={options.special1_5Options}
        onChange={value => updateSituationalHitting({ type: 'updateVersusLefty', versusLefty: value })}
      />
      <SpecialBooleanRow
        label='Table Setter'
        value={situationalHitting.isTableSetter}
        onChange={value => updateSituationalHitting({ type: 'updateIsTableSetter', isTableSetter: value })}
      />
      <SpecialBooleanRow
        label='Good Back to Back Hitter'
        abbrevLabel='Gd B2B Htr'
        value={situationalHitting.isBackToBackHitter}
        onChange={value => updateSituationalHitting({ type: 'updateIsBackToBackHitter', isBackToBackHitter: value })}
      />
      <SpecialBooleanRow
        label='Hot Hitter'
        value={situationalHitting.isHotHitter}
        onChange={value => updateSituationalHitting({ type: 'updateIsHotHitter', isHotHitter: value })}
      />
      <SpecialBooleanRow
        label='Rally Hitter'
        value={situationalHitting.isRallyHitter}
        onChange={value => updateSituationalHitting({ type: 'updateIsRallyHitter', isRallyHitter: value })}
      />
      <SpecialBooleanRow
        label='Good Pinch Hitter'
        abbrevLabel='Gd Pinch Hitter'
        value={situationalHitting.isGoodPinchHitter}
        onChange={value => updateSituationalHitting({ type: 'updateIsGoodPinchHitter', isGoodPinchHitter: value })}
      />
      <SpecialMultiValueRow
        label='Bases Loaded Hitter'
        value={situationalHitting.basesLoadedHitter}
        options={options.basesLoadedHitterOptions}
        onChange={value => updateSituationalHitting({ type: 'updateBasesLoadedHitter', basesLoadedHitter: value })}
      />
      <SpecialMultiValueRow
        label='Walk-Off Hitter'
        value={situationalHitting.walkOffHitter}
        options={options.walkOffHitterOptions}
        onChange={value => updateSituationalHitting({ type: 'updateWalkOffHitter', walkOffHitter: value })}
      />
      <SpecialNumericRow
        label='Clutch Hitter'
        value={situationalHitting.clutchHitter}
        options={options.special1_5Options}
        onChange={value => updateSituationalHitting({ type: 'updateClutchHitter', clutchHitter: value })}
      />
      <h3>Approach</h3>
      <SpecialBooleanRow
        label='Contact Hitter'
        value={approach.isContactHitter}
        onChange={value => updateApproach({ type: 'updateIsContactHitter', isContactHitter: value })}
      />
      <SpecialBooleanRow
        label='Power Hitter'
        value={approach.isPowerHitter}
        onChange={value => updateApproach({ type: 'updateIsPowerHitter', isPowerHitter: value })}
      />
      <SpecialMultiValueRow
        label='Slugger or Slap Hitter'
        value={approach.sluggerOrSlapHitter}
        options={options.sluggerOrSlapHitterOptions}
        onChange={value => updateApproach({ type: 'updateSluggerOrSlapHitter', sluggerOrSlapHitter: value })}
      />
      <SpecialBooleanRow
        label='Push Hitter'
        value={approach.isPushHitter}
        onChange={value => updateApproach({ type: 'updateIsPushHitter', isPushHitter: value })}
      />
      <SpecialBooleanRow
        label='Pull Hitter'
        value={approach.isPullHitter}
        onChange={value => updateApproach({ type: 'updateIsPullHitter', isPullHitter: value })}
      />
      <SpecialBooleanRow
        label='Spray Hitter'
        value={approach.isSprayHitter}
        onChange={value => updateApproach({ type: 'updateIsSprayHitter', isSprayHitter: value })}
      />
      <SpecialBooleanRow
        label='Firstball Hitter'
        value={approach.isFirstballHitter}
        onChange={value => updateApproach({ type: 'updateIsFirstballHitter', isFirstballHitter: value })}
      />
      <SpecialMultiValueRow
        label='Aggressive or Patient Hitter'
        value={approach.aggressiveOrPatientHitter}
        options={options.aggressiveOrPatientHitterOptions}
        onChange={value => updateApproach({ type: 'updateAggressiveOrPatientHitter', aggressiveOrPatientHitter: value })}
      />
      <SpecialBooleanRow
        label='Refined Hitter'
        value={approach.isRefinedHitter}
        onChange={value => updateApproach({ type: 'updateIsRefinedHitter', isRefinedHitter: value })}
      />
      <SpecialBooleanRow
        label='Tough Out'
        value={approach.isToughOut}
        onChange={value => updateApproach({ type: 'updateIsToughOut', isToughOut: value })}
      />
      <SpecialBooleanRow
        label='Intimidator'
        value={approach.isIntimidatingHitter}
        onChange={value => updateApproach({ type: 'updateIsIntimidatingHitter', isIntimidatingHitter: value })}
      />
      <SpecialBooleanRow
        label='Sparkplug'
        value={approach.isSparkplug}
        onChange={value => updateApproach({ type: 'updateIsSparkplug', isSparkplug: value })}
      />
      <h3>Small Ball</h3>
      <SpecialPositiveNegativeRow
        positiveLabel='Gd Small Ball'
        negativeLabel='Pr Small Ball'
        options={options.specialPositive_NegativeOptions}
        value={smallBall.smallBall}
        onChange={value => updateSmallBall({ type: 'updateSmallBall', smallBall: value })}
      />
      <SpecialMultiValueRow
        label='Bunting'
        value={smallBall.bunting}
        options={options.buntingAbilityOptions}
        onChange={value => updateSmallBall({ type: 'updateBunting', bunting: value })}
      />
      <SpecialMultiValueRow
        label='Infield Hitter'
        value={smallBall.infieldHitter}
        options={options.infieldHittingAbilityOptions}
        onChange={value => updateSmallBall({ type: 'updateInfieldHitter', infieldHitter: value })}
      />
      <h3>Base Running</h3>
      <SpecialNumericRow
        label='Base Running'
        value={baseRunning.baseRunning}
        options={options.special2_4Options}
        onChange={value => updateBaseRunning({ type: 'updateBaseRunning', baseRunning: value })}
      />
      <SpecialNumericRow
        label='Stealing'
        value={baseRunning.stealing}
        options={options.special2_4Options}
        onChange={value => updateBaseRunning({ type: 'updateStealing', stealing: value })}
      />
      <SpecialBooleanRow
        label='Aggressive Runner'
        abbrevLabel='Aggressiv Run'
        value={baseRunning.isAggressiveRunner}
        onChange={value => updateBaseRunning({ type: 'updateIsAggressiveRunner', isAggressiveRunner: value })}
      />
      <SpecialMultiValueRow
        label='Agressive or Cautious Base Stealer'
        value={baseRunning.aggressiveOrPatientBaseStealer}
        options={options.aggressiveOrCautiousBaseStealerOptions}
        onChange={value => updateBaseRunning({ type: 'updateAggressiveOrPatientBaseStealer', aggressiveOrPatientBaseStealer: value })}
      />
      <SpecialBooleanRow
        label='Tough Runner'
        value={baseRunning.isToughRunner}
        onChange={value => updateBaseRunning({ type: 'updateIsToughRunner', isToughRunner: value })}
      />
      <SpecialBooleanRow
        label='Will Break Up Double Play'
        abbrevLabel='Breakup DP'
        value={baseRunning.willBreakupDoublePlay}
        onChange={value => updateBaseRunning({ type: 'updateWillBreakupDoublePlay', willBreakupDoublePlay: value })}
      />
      <SpecialBooleanRow
        label='Will Slide Head First'
        abbrevLabel='Hd 1st Slide'
        value={baseRunning.willSlideHeadFirst}
        onChange={value => updateBaseRunning({ type: 'updateWillSlideHeadFirst', willSlideHeadFirst: value })}
      />
      <h3>Fielding</h3>
      <SpecialBooleanRow
        label='Gold Glover'
        value={fielding.isGoldGlover}
        onChange={value => updateFielding({ type: 'updateIsGoldGlover', isGoldGlover: value })}
      />
      <SpecialBooleanRow
        label='Spider Catch'
        value={fielding.canSpiderCatch}
        onChange={value => updateFielding({ type: 'updateCanSpiderCatch', canSpiderCatch: value })}
      />
      <SpecialBooleanRow
        label='Barehand Catch'
        abbrevLabel='Barehand Cth'
        value={fielding.canBarehandCatch}
        onChange={value => updateFielding({ type: 'updateCanBarehandCatch', canBarehandCatch: value })}
      />
      <SpecialBooleanRow
        label='Aggressive Fielder'
        abbrevLabel='Aggressiv Fld'
        value={fielding.isAggressiveFielder}
        onChange={value => updateFielding({ type: 'updateIsAggressiveFielder', isAggressiveFielder: value })}
      />
      <SpecialBooleanRow
        label='Pivot Man'
        value={fielding.isPivotMan}
        onChange={value => updateFielding({ type: 'updateIsPivotMan', isPivotMan: value })}
      />
      <SpecialBooleanRow
        label='Is Error Prone'
        value={fielding.isErrorProne}
        effectIsNegative
        onChange={value => updateFielding({ type: 'updateIsErrorProne', isErrorProne: value })}
      />
      <SpecialBooleanRow
        label='Good Blocker'
        value={fielding.isGoodBlocker}
        onChange={value => updateFielding({ type: 'updateIsGoodBlocker', isGoodBlocker: value })}
      />
      <SpecialMultiValueRow
        label='Catching'
        value={fielding.catching}
        options={options.catchingAbilityOptions}
        onChange={value => updateFielding({ type: 'updateCatching', catching: value })}
      />
      <SpecialNumericRow
        label='Throwing'
        value={fielding.throwing}
        options={options.special2_4Options}
        onChange={value => updateFielding({ type: 'updateThrowing', throwing: value })}
      />
      <SpecialBooleanRow
        label='Cannon Arm'
        value={fielding.hasCannonArm}
        onChange={value => updateFielding({ type: 'updateHasCannonArm', hasCannonArm: value })}
      />
      <SpecialBooleanRow
        label='Trash Talker'
        value={fielding.isTrashTalker}
        onChange={value => updateFielding({ type: 'updateIsTrashTalker', isTrashTalker: value })}
      />
    </SpecialAbilitiesSection>
  }

  function getPitcherSection() {
    const [situationalPitching, updateSituationalPitching] = getSituationalPitchingSpecialAbilitiesReducer(details, update);
    const [demeanor, updateDemeanor] = getPitchingDemeanorSpecialAbilitiesReducer(details, update);
    const [mechanics, updateMechanics] = getPitchingMechanicsSpecialAbilitiesReducer(details, update);
    const [pitchQualities, updatePitchQualities] = getPitchQualitiesSpecialAbilitiesReducer(details, update);

    return <SpecialAbilitiesSection>
      <h2>Pitcher</h2>
      <h3>Situational</h3>
      <SpecialNumericRow
        label='Consistency'
        value={situationalPitching.consistency}
        options={options.special2_4Options}
        onChange={value => updateSituationalPitching({ type: 'updateConsistency', consistency: value })}
      />
      <SpecialNumericRow
        label='Versus Lefty'
        value={situationalPitching.versusLefty}
        options={options.special2_4Options}
        onChange={value => updateSituationalPitching({ type: 'updateVersusLefty', versusLefty: value })}
      />
      <SpecialNumericRow
        label='Poise'
        value={situationalPitching.poise}
        options={options.special2_4Options}
        onChange={value => updateSituationalPitching({ type: 'updatePoise', poise: value })}
      />
      <FieldLabel>Vs Runner</FieldLabel>
      <FlexRow gap='4px' withBottomPadding>
        <SpecialAbilityRadioButton
          name='Vs Runner'
          label='Vs Runner'
          effect='Negative'
          numericValue='2'
          checked={situationalPitching.poorVersusRunner}
          onSelect={() => updateSituationalPitching({ type: 'updatePoorVersusRunner', poorVersusRunner: true })}
        />
        <SpecialAbilityRadioButton
          name='Vs Runner'
          label='Vs Runner'
          effect='Neutral'
          numericValue='3'
          checked={!situationalPitching.poorVersusRunner}
          onSelect={() => updateSituationalPitching({ type: 'updatePoorVersusRunner', poorVersusRunner: false })}
        />
      </FlexRow>
      <SpecialNumericRow
        label='With Runners in Scoring Position'
        abbrevLabel='w/ RISP'
        value={situationalPitching.withRunnersInScoringPosition}
        options={options.special2_4Options}
        onChange={value => updateSituationalPitching({ type: 'updateWithRunnersInScoringPosition', withRunnersInScoringPosition: value })}
      />
      <SpecialBooleanRow
        label='Slow Starter'
        value={situationalPitching.isSlowStarter}
        effectIsNegative
        onChange={value => updateSituationalPitching({ type: 'updateIsSlowStarter', isSlowStarter: value })}
      />
      <SpecialBooleanRow
        label='Starter/Finisher (Complete Game Ability)'
        abbrevLabel='Str Finisher'
        value={situationalPitching.isStarterFinisher}
        onChange={value => updateSituationalPitching({ type: 'updateIsStarterFinisher', isStarterFinisher: value })}
      />
      <SpecialBooleanRow
        label='Choke Artist'
        value={situationalPitching.isChokeArtist}
        effectIsNegative
        onChange={value => updateSituationalPitching({ type: 'updateIsChokeArtist', isChokeArtist: value })}
      />
      <SpecialBooleanRow
        label='Sandbag'
        value={situationalPitching.isSandbag}
        effectIsNegative
        onChange={value => updateSituationalPitching({ type: 'updateIsSandbag', isSandbag: value })}
      />
      <SpecialBooleanRow
        label='Doctor K'
        abbrevLabel='Dr. K'
        value={situationalPitching.doctorK}
        onChange={value => updateSituationalPitching({ type: 'updateDoctorK', doctorK: value })}
      />
      <SpecialBooleanRow
        label='Walk Prone'
        value={situationalPitching.isWalkProne}
        effectIsNegative
        onChange={value => updateSituationalPitching({ type: 'updateIsWalkProne', isWalkProne: value })}
      />
      <SpecialPositiveNegativeRow
        positiveLabel='Lucky'
        negativeLabel='Unlucky'
        options={options.specialPositive_NegativeOptions}
        value={situationalPitching.luck}
        onChange={value => updateSituationalPitching({ type: 'updateLuck', luck: value })}
      />
      <SpecialNumericRow
        label='Recovery'
        value={situationalPitching.recovery}
        options={options.special2_4Options}
        onChange={value => updateSituationalPitching({ type: 'updateRecovery', recovery: value })}
      />
      <h3>Demeanor</h3>
      <SpecialBooleanRow
        label='Intimidator'
        value={demeanor.isIntimidatingPitcher}
        onChange={value => updateDemeanor({ type: 'updateIsIntimidatingPitcher', isIntimidatingPitcher: value })}
      />
      <SpecialMultiValueRow
        label='Battler/Poker Face'
        value={demeanor.battlerOrPokerFace}
        options={options.battlerPokerFaceOptions}
        onChange={value => updateDemeanor({ type: 'updateBattlerOrPokerFace', battlerOrPokerFace: value })}
      />
      <SpecialBooleanRow
        label='Hot Head'
        value={demeanor.isHotHead}
        effectIsNegative
        onChange={value => updateDemeanor({ type: 'updateIsHotHead', isHotHead: value })}
      />
      <h3>Mechanics</h3>
      <SpecialBooleanRow
        label='Good Release'
        value={mechanics.goodDelivery}
        onChange={value => updateMechanics({ type: 'updateGoodDelivery', goodDelivery: value })}
      />
      <SpecialNumericRow
        label='Release'
        value={mechanics.release}
        options={options.special2_4Options}
        onChange={value => updateMechanics({ type: 'updateRelease', release: value })}
      />
      <SpecialBooleanRow
        label='Good Pace'
        value={mechanics.goodPace}
        onChange={value => updateMechanics({ type: 'updateGoodPace', goodPace: value })}
      />
      <SpecialBooleanRow
        label='Good Reflexes'
        value={mechanics.goodReflexes}
        onChange={value => updateMechanics({ type: 'updateGoodReflexes', goodReflexes: value })}
      />
      <SpecialBooleanRow
        label='Good Pickoff'
        value={mechanics.goodPickoff}
        onChange={value => updateMechanics({ type: 'updateGoodPickoff', goodPickoff: value })}
      />
      <h3>Pitch Qualities</h3>
      <SpecialMultiValueRow
        label='Power or Breaking Ball Pitcher'
        value={pitchQualities.powerOrBreakingBallPitcher}
        options={options.powerOrBreakingBallPitcher}
        onChange={value => updatePitchQualities({ type: 'updatePowerOrBreakingBallPitcher', powerOrBreakingBallPitcher: value })}
      />
      <SpecialNumericRow
        label='Fastball Life'
        value={pitchQualities.fastballLife}
        options={options.special2_4Options}
        onChange={value => updatePitchQualities({ type: 'updateFastballLife', fastballLife: value })}
      />
      <SpecialNumericRow
        label='Spin'
        value={pitchQualities.spin}
        options={options.special2_4Options}
        onChange={value => updatePitchQualities({ type: 'updateSpin', spin: value })}
      />
      <SpecialPositiveNegativeRow
        positiveLabel='Safe Pitch'
        negativeLabel='Fat Pitch'
        options={options.specialPositive_NegativeOptions}
        value={pitchQualities.safeOrFatPitch}
        onChange={value => updatePitchQualities({ type: 'updateSafeOrFatPitch', safeOrFatPitch: value })}
      />
      <SpecialPositiveNegativeRow
        positiveLabel='Ground Ball P'
        negativeLabel='Fly Ball P'
        options={options.specialPositive_NegativeOptions}
        value={pitchQualities.groundBallOrFlyBallPitcher}
        onChange={value => updatePitchQualities({ type: 'updateGroundBallOrFlyBallPitcher', groundBallOrFlyBallPitcher: value })}
      />
      <SpecialBooleanRow
        label='Good Low Pitch'
        abbrevLabel='Gd Low Pitch'
        value={pitchQualities.goodLowPitch}
        onChange={value => updatePitchQualities({ type: 'updateGoodLowPitch', goodLowPitch: value })}
      />
      <SpecialBooleanRow
        label='Gyroball'
        value={pitchQualities.gyroball}
        onChange={value => updatePitchQualities({ type: 'updateGyroball', gyroball: value })}
      />
      <SpecialBooleanRow
        label='Shutto Spin'
        value={pitchQualities.shuttoSpin}
        effectIsNegative
        onChange={value => updatePitchQualities({ type: 'updateShuttoSpin', shuttoSpin: value })}
      />
    </SpecialAbilitiesSection>
  }
}

export interface SpecialBooleanRowProps {
  label: string;
  abbrevLabel?: string;
  value: boolean;
  effectIsNegative?: boolean;
  onChange: (value: boolean) => void;
}

export function SpecialBooleanRow(props: SpecialBooleanRowProps) {
  const { label, abbrevLabel, value, effectIsNegative, onChange } = props;
  
  return <>
    <FieldLabel>{label}</FieldLabel>
    <FlexRow gap='4px' withBottomPadding>
      <SpecialAbilityRadioButton
        name={label}
        label='Off'
        effect='Neutral'
        checked={!value}
        onSelect={() => onChange(false)}
      />
      <SpecialAbilityRadioButton
        name={label}
        label={abbrevLabel ?? label}
        effect={effectIsNegative ? 'Negative' : 'Positive'}
        checked={value}
        onSelect={() => onChange(true)}
      />
    </FlexRow>
  </> 
}

export interface SpecialNumericRowProps {
  label: string;
  abbrevLabel?: string;
  value: KeyedCode;
  options: KeyedCode[];
  onChange: (value: KeyedCode) => void;
}

export function SpecialNumericRow(props: SpecialNumericRowProps) {
  const { label, abbrevLabel, value, options, onChange } = props;
  
  return <>
    <FieldLabel>{label}</FieldLabel>
    <FlexRow gap='4px' withBottomPadding>
      {options.map(toRadioButton)}
    </FlexRow>
  </> 

  function toRadioButton(option: KeyedCode) {
    return <SpecialAbilityRadioButton
      key={option.key}
      name={label}
      label={abbrevLabel ?? label}
      numericValue={option.name}
      effect={getEffect(option.key)}
      checked={value.key === option.key}
      onSelect={() => onChange(option)}
    />
  }
}

export interface SpecialPositiveNegativeRowProps {
  positiveLabel: string;
  negativeLabel: string;
  value: KeyedCode;
  options: KeyedCode[];
  onChange: (value: KeyedCode) => void;
}

export function SpecialPositiveNegativeRow(props: SpecialPositiveNegativeRowProps) {
  const { positiveLabel, negativeLabel, value, options, onChange } = props;
  
  return <>
    <FieldLabel>{positiveLabel}/{negativeLabel}</FieldLabel>
    <FlexRow gap='4px' withBottomPadding>
      {options.map(toRadioButton)}
    </FlexRow>
  </> 

  function toRadioButton(option: KeyedCode) {
    return <SpecialAbilityRadioButton
      key={option.key}
      name={positiveLabel}
      label={option.key === 'Positive' 
        ? positiveLabel
        : option.key === 'Negative'
          ? negativeLabel
          : 'Off'}
      effect={option.key === 'Positive' 
      ? 'Positive'
      : option.key === 'Negative'
        ? 'Negative'
        : 'Neutral'}
      checked={value.key === option.key}
      onSelect={() => onChange(option)}
    />
  }
}

export interface SpecialMultiValueRowProps {
  label: string;
  value: KeyedCode | undefined;
  options: KeyedCode[];
  onChange: (value: KeyedCode | undefined) => void;
}

export function SpecialMultiValueRow(props: SpecialMultiValueRowProps) {
  const { label, value, options, onChange } = props;
  
  return <>
    <FieldLabel>{label}</FieldLabel>
    <FlexRow gap='4px' withBottomPadding>
      <SpecialAbilityRadioButton
        name={label}
        label='Off'
        effect='Neutral'
        checked={!value}
        onSelect={() => onChange(undefined)}
      />
      {options.map(toRadioButton)}
    </FlexRow>
  </> 

  function toRadioButton(option: KeyedCode) {
    return <SpecialAbilityRadioButton
      key={option.key}
      name={option.key}
      label={option.name}
      effect='Positive'
      checked={value?.key === option.key}
      onSelect={() => onChange(option)}
    />
  }
}

function getEffect(numberKey: string): SpecialAbilityEffect {
  switch(numberKey) {
    case 'One':
      return 'Negative';
    case 'Two':
      return 'Negative';
    case 'Three':
      return 'Neutral';
    case 'Four':
      return 'Positive';
    case 'Five':
      return 'Positive';
    default:
      return 'Neutral';
  }
}

const SpecialAbilitiesSection = styled.div`
  padding-bottom: 32px;
`