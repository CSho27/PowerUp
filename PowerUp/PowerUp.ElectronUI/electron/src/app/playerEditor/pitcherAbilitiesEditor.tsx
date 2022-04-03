import { Dispatch } from "react";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow";
import { GradeLetter } from "../../components/gradeLetter/gradeLetter";
import { NumberField } from "../../components/numberField/numberField";
import { SelectField } from "../../components/SelectField/selectField";
import { toOptions, tryToKeyedCode } from "../../components/SelectField/selectFieldHelpers";
import { KeyedCode } from "../shared/keyedCode";
import { PitcherAbilitiesOptions } from "./loadPlayerEditorApiClient";
import { getGradeForControl, getGradeForStamina, PitcherAbilities, PitcherAbilitiesAction } from "./playerEditorState";

export interface PitcherAbilitiesEditorProps {
  options: PitcherAbilitiesOptions;
  details: PitcherAbilities;
  update: Dispatch<PitcherAbilitiesAction>;
}

export function PitcherAbilitiesEditor(props: PitcherAbilitiesEditorProps) {
  const { options, details, update } = props;
  
  return <>
    <FlexRow gap='16px'>
      <FlexFracItem frac='1/3'>
        <FlexRow gap='16px' withBottomPadding>
          <FlexFracItem frac='1/3'>
            <FieldLabel htmlFor='topSpeed'>Top Speed</FieldLabel>
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <NumberField 
              id='topSpeed'
              type='Defined'
              value={details.topSpeed}
              min={49}
              max={105}
              onChange={topSpeed => update({ type: 'updateTopSpeed', topSpeed: topSpeed })}
            />
          </FlexFracItem>
          <FieldLabel htmlFor='topSpeed'>MPH</FieldLabel>
        </FlexRow>
        <FlexRow gap='16px' withBottomPadding>
          <FlexFracItem frac='1/3'>
            <FieldLabel htmlFor='control'>Control</FieldLabel>
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <NumberField 
              id='control'
              type='Defined'
              value={details.control}
              min={1}
              max={255}
              onChange={control => update({ type: 'updateControl', control: control })}
            />
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <GradeLetter grade={getGradeForControl(details.control)}/>
          </FlexFracItem>
        </FlexRow>
        <FlexRow gap='16px' withBottomPadding>
          <FlexFracItem frac='1/3'>
            <FieldLabel htmlFor='stamina'>Stamina</FieldLabel>
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <NumberField 
              id='stamina'
              type='Defined'
              value={details.stamina}
              min={1}
              max={255}
              onChange={stamina => update({ type: 'updateStamina', stamina: stamina })}
            />
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <GradeLetter grade={getGradeForStamina(details.stamina)}/>
          </FlexFracItem>
        </FlexRow>
      </FlexFracItem>
      <FlexFracItem frac='2/3'>
        <PitchTypeRow
          id='twoSeam'
          typeLabel='Two Seam'
          typeOptions={options.twoSeamOptions}
          typeValue={details.twoSeamType}
          onTypeChange={type => update({ type: 'updateTwoSeamType', twoSeamType: type })}
          movementMin={1}
          movementMax={3}
          movementValue={details.twoSeamMovement}
          onMovementChange={movement => update({ type: 'updateTwoSeamMovement', movement: movement })}
        />
        <PitchTypeRow
          id='slider1'
          typeLabel='Slider'
          typeOptions={options.sliderOptions}
          typeValue={details.slider1Type}
          onTypeChange={type => update({ type: 'updateSlider1Type', sliderType: type })}
          movementMin={1}
          movementMax={7}
          movementValue={details.slider1Movement}
          onMovementChange={movement => update({ type: 'updateSlider1Movement', movement: movement })}
        />
        {!!details.slider1Type &&
        <PitchTypeRow
          id='slider2'
          typeLabel='Slider 2'
          typeOptions={options.sliderOptions}
          typeValue={details.slider2Type}
          onTypeChange={type => update({ type: 'updateSlider2Type', sliderType: type })}
          movementMin={1}
          movementMax={7}
          movementValue={details.slider2Movement}
          onMovementChange={movement => update({ type: 'updateSlider2Movement', movement: movement })}
        />}
        <PitchTypeRow
          id='curve1'
          typeLabel='Curve'
          typeOptions={options.curveOptions}
          typeValue={details.curve1Type}
          onTypeChange={type => update({ type: 'updateCurve1Type', curveType: type })}
          movementMin={1}
          movementMax={7}
          movementValue={details.curve1Movement}
          onMovementChange={movement => update({ type: 'updateCurve1Movmement', movement: movement })}
        />
        {!!details.curve1Type &&
        <PitchTypeRow
          id='curve2'
          typeLabel='Curve 2'
          typeOptions={options.curveOptions}
          typeValue={details.curve2Type}
          onTypeChange={type => update({ type: 'updateCurve2Type', curveType: type })}
          movementMin={1}
          movementMax={7}
          movementValue={details.curve2Movement}
          onMovementChange={movement => update({ type: 'updateCurve2Movement', movement: movement })}
        />}
        <PitchTypeRow
          id='fork1'
          typeLabel='Fork'
          typeOptions={options.forkOptions}
          typeValue={details.fork1Type}
          onTypeChange={type => update({ type: 'updateFork1Type', forkType: type })}
          movementMin={1}
          movementMax={7}
          movementValue={details.fork1Movement}
          onMovementChange={movement => update({ type: 'updateFork1Movement', movement: movement })}
        />
        {!!details.fork1Type &&
        <PitchTypeRow
          id='fork2'
          typeLabel='Fork 2'
          typeOptions={options.forkOptions}
          typeValue={details.fork2Type}
          onTypeChange={type => update({ type: 'updateFork2Type', forkType: type })}
          movementMin={1}
          movementMax={7}
          movementValue={details.fork2Movement}
          onMovementChange={movement => update({ type: 'updateFork2Movement', movement: movement })}
        />}
        <PitchTypeRow
          id='sinker1'
          typeLabel='Sinker'
          typeOptions={options.sinkerOptions}
          typeValue={details.sinker1Type}
          onTypeChange={type => update({ type: 'updateSinker1Type', sinkerType: type })}
          movementMin={1}
          movementMax={7}
          movementValue={details.sinker1Movement}
          onMovementChange={movement => update({ type: 'updateSinker1Movement', movement: movement })}
        />
        {!!details.sinker1Type &&
        <PitchTypeRow
          id='sinker2'
          typeLabel='Sinker 2'
          typeOptions={options.sinkerOptions}
          typeValue={details.sinker2Type}
          onTypeChange={type => update({ type: 'updateSinker2Type', sinkerType: type })}
          movementMin={1}
          movementMax={7}
          movementValue={details.sinker2Movement}
          onMovementChange={movement => update({ type: 'updateSinker2Movement', movement: movement })}
        />}
        <PitchTypeRow
          id='sinkingFastball1'
          typeLabel='Sinking Fastball'
          typeOptions={options.sinkingFastballOptions}
          typeValue={details.sinkingFastball1Type}
          onTypeChange={type => update({ type: 'updateSinkingFastball1Type', sinkingFastballType: type })}
          movementMin={1}
          movementMax={7}
          movementValue={details.sinkingFastball1Movement}
          onMovementChange={movement => update({ type: 'updateSinkingFastball1Movement', movement: movement })}
        />
        {!!details.sinkingFastball1Type &&
        <PitchTypeRow
          id='sinkingFastball2'
          typeLabel='Sinking Fastball 2'
          typeOptions={options.sinkingFastballOptions}
          typeValue={details.sinkingFastball2Type}
          onTypeChange={type => update({ type: 'updateSinkingFastball2Type', sinkingFastballType: type })}
          movementMin={1}
          movementMax={7}
          movementValue={details.sinkingFastball2Movement}
          onMovementChange={movement => update({ type: 'updateSinkingFastball2Movement', movement: movement })}
        />}
      </FlexFracItem>
    </FlexRow>
  </>
}

interface PitchTypeRowProps {
  id: string;
  typeLabel: string;
  typeOptions: KeyedCode[];
  typeValue: KeyedCode | undefined;
  onTypeChange: (option: KeyedCode | undefined) => void;
  movementMin: number;
  movementMax: number;
  movementValue: number;
  onMovementChange: (movement: number) => void;
}

function PitchTypeRow(props: PitchTypeRowProps) {
  const { 
    id, 
    typeLabel,
    typeOptions,
    typeValue, 
    onTypeChange,
    movementMin,
    movementMax, 
    movementValue, 
    onMovementChange 
  } = props;

  const typeId = `${id}Type`;
  const movementId = `${id}Movement`;
  
  return <FlexRow gap='16px' withBottomPadding>
    <FlexFracItem frac='1/4'>
      <FieldLabel htmlFor={typeId}>{typeLabel}</FieldLabel>
    </FlexFracItem>
    <FlexFracItem frac='1/4'>
      <SelectField 
        id={typeId}
        value={typeValue?.key}
        onChange={type => onTypeChange(tryToKeyedCode(typeOptions, type))}
      >{toOptions(typeOptions, true)}</SelectField>
    </FlexFracItem>
    <FlexFracItem frac='1/24' />
    {!!typeValue && <>
      <FlexFracItem frac='1/4'>
        <FieldLabel htmlFor={movementId}>Movement</FieldLabel>
      </FlexFracItem>
      <FlexFracItem frac='1/4'>
        <NumberField
          id={movementId}
          type='Defined'
          value={movementValue}
          min={movementMin}
          max={movementMax}
          onChange={onMovementChange}
        />
      </FlexFracItem>
    </>}
  </FlexRow>
}