import { Dispatch } from "react";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow";
import { GradeLetter } from "../../components/gradeLetter/gradeLetter";
import { NumberField } from "../../components/numberField/numberField";
import { getGradeFor0_15, getGradeForPower, HitterAbilities, HitterAbilitiesAction } from "./playerEditorState";

export interface HitterAbilitiesEditorProps {
  details: HitterAbilities;
  update: Dispatch<HitterAbilitiesAction>;
}

export function HitterAbilitiesEditor(props: HitterAbilitiesEditorProps) {
  const { details, update } = props;
  
  return <>
    <FlexRow gap='16px'>
      <FlexFracItem frac='1/2'>
        <FlexRow gap='16px' withBottomPadding>
          <FlexFracItem frac='1/3'>
            <FieldLabel htmlFor='trajectory'>Trajectory</FieldLabel>
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <NumberField 
              id='trajectory'
              type='Defined'
              value={details.trajectory}
              min={1}
              max={4}
              onChange={trajectory => update({ type: 'updateTrajectory', trajectory: trajectory })}
            />
          </FlexFracItem>
        </FlexRow>
        <FlexRow gap='16px' withBottomPadding>
          <FlexFracItem frac='1/3'>
            <FieldLabel htmlFor='contact'>Contact</FieldLabel>
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <NumberField 
              id='contact'
              type='Defined'
              value={details.contact}
              min={1}
              max={15}
              onChange={contact => update({ type: 'updateContact', contact: contact })}
            />
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <GradeLetter grade={getGradeFor0_15(details.contact)}/>
          </FlexFracItem>
        </FlexRow>
        <FlexRow gap='16px' withBottomPadding>
          <FlexFracItem frac='1/3'>
            <FieldLabel htmlFor='power'>Power</FieldLabel>
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <NumberField 
              id='power'
              type='Defined'
              value={details.power}
              min={1}
              max={255}
              stepSize={10}
              onChange={power => update({ type: 'updatePower', power: power })}
            />
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <GradeLetter grade={getGradeForPower(details.power)}/>
          </FlexFracItem>
        </FlexRow>
        <FlexRow gap='16px' withBottomPadding>
          <FlexFracItem frac='1/3'>
            <FieldLabel htmlFor='runSpeed'>Run Speed</FieldLabel>
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <NumberField 
              id='runSpeed'
              type='Defined'
              value={details.runSpeed}
              min={1}
              max={15}
              onChange={runSpeed => update({ type: 'updateRunSpeed', runSpeed: runSpeed })}
            />
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <GradeLetter grade={getGradeFor0_15(details.runSpeed)}/>
          </FlexFracItem>
        </FlexRow>
        <FlexRow gap='16px' withBottomPadding>
          <FlexFracItem frac='1/3'>
            <FieldLabel htmlFor='armStrength'>Arm Strength</FieldLabel>
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <NumberField 
              id='armStrength'
              type='Defined'
              value={details.armStrength}
              min={1}
              max={15}
              onChange={armStrength => update({ type: 'updateArmStrength', armStrength: armStrength })}
            />
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <GradeLetter grade={getGradeFor0_15(details.armStrength)}/>
          </FlexFracItem>
        </FlexRow>
        <FlexRow gap='16px' withBottomPadding>
          <FlexFracItem frac='1/3'>
            <FieldLabel htmlFor='fielding'>Fielding</FieldLabel>
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <NumberField 
              id='fielding'
              type='Defined'
              value={details.fielding}
              min={1}
              max={15}
              onChange={fielding => update({ type: 'updateFielding', fielding: fielding })}
            />
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <GradeLetter grade={getGradeFor0_15(details.fielding)}/>
          </FlexFracItem>
        </FlexRow>
        <FlexRow gap='16px' withBottomPadding>
          <FlexFracItem frac='1/3'>
            <FieldLabel htmlFor='errorResistance'>Error Resistance</FieldLabel>
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <NumberField 
              id='errorResistance'
              type='Defined'
              value={details.errorResistance}
              min={1}
              max={15}
              onChange={errorResistance => update({ type: 'updateErrorResistance', errorResistance: errorResistance })}
            />
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <GradeLetter grade={getGradeFor0_15(details.errorResistance)}/>
          </FlexFracItem>
        </FlexRow>
      </FlexFracItem>
      <FlexFracItem frac='1/2'>
      </FlexFracItem>
    </FlexRow>
  </>
}