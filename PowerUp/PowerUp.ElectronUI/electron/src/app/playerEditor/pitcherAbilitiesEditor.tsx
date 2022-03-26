import { Dispatch } from "react";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow";
import { GradeLetter } from "../../components/gradeLetter/gradeLetter";
import { NumberField } from "../../components/numberField/numberField";
import { getGradeForControl, getGradeForStamina, PitcherAbilities, PitcherAbilitiesAction } from "./playerEditorState";

export interface PitcherAbilitiesEditorProps {
  details: PitcherAbilities;
  update: Dispatch<PitcherAbilitiesAction>;
}

export function PitcherAbilitiesEditor(props: PitcherAbilitiesEditorProps) {
  const { details, update } = props;
  
  return <>
    <FlexRow gap='16px'>
      <FlexFracItem frac='1/2'>
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
      <FlexFracItem frac='1/2'>
      </FlexFracItem>
    </FlexRow>
  </>
}