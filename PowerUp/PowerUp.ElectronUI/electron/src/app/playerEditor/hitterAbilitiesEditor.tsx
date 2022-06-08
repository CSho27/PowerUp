import { Dispatch } from "react";
import styled from "styled-components";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow";
import { getGradeFor0_15, getGradeForPower, GradeLetter } from "../../components/gradeLetter/gradeLetter";
import { Icon } from "../../components/icon/icon";
import { NumberField } from "../../components/numberField/numberField";
import { TrajectoryArrow, TrajectoryValue } from "../../components/trajcetoryArrow/trajectoryArrow";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { BattingSide, HotZoneGrid } from "./hotZoneGrid";
import { getHotZoneGridReducer, HitterAbilities, HitterAbilitiesAction } from "./playerEditorState";

export interface HitterAbilitiesEditorProps {
  battingSide: BattingSide;
  details: HitterAbilities;
  disabled?: boolean;
  update: Dispatch<HitterAbilitiesAction>;
}

export function HitterAbilitiesEditor(props: HitterAbilitiesEditorProps) {
  const { battingSide, details, disabled: editorDisabled, update } = props;

  const [hotZones, updateHotZones] = getHotZoneGridReducer(details, update);
  
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
              disabled={editorDisabled}
              onChange={trajectory => update({ type: 'updateTrajectory', trajectory: trajectory })}
            />
          </FlexFracItem>
          <TrajectoryArrow value={details.trajectory as TrajectoryValue}/>
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
              disabled={editorDisabled}
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
              disabled={editorDisabled}
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
              disabled={editorDisabled}
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
              disabled={editorDisabled}
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
              disabled={editorDisabled}
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
              disabled={editorDisabled}
              onChange={errorResistance => update({ type: 'updateErrorResistance', errorResistance: errorResistance })}
            />
          </FlexFracItem>
          <FlexFracItem frac='1/4'>
            <GradeLetter grade={getGradeFor0_15(details.errorResistance)}/>
          </FlexFracItem>
        </FlexRow>
      </FlexFracItem>
      <FlexFracItem frac='1/2'>
        <HotZoneGrid 
          battingSide={battingSide} 
          grid={hotZones}
          disabled={editorDisabled}
          update={updateHotZones}
        />
      </FlexFracItem>
    </FlexRow>
  </>
}