import { Dispatch } from "react";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow";
import { Grade, GradeLetter } from "../../components/gradeLetter/gradeLetter";
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader";
import { SelectField } from "../../components/SelectField/selectField";
import { toKeyedCode, toOptions } from "../../components/SelectField/selectFieldHelpers";
import { KeyedCode } from "../shared/keyedCode";
import { Position, PositionCode } from "../shared/positionCode";
import { PositionCapabilityDetails, PositionCapabilityDetailsAction } from "./playerEditorState";

export interface PositionCapabilitiesEditorProps {
  primaryPosition: PositionCode;
  details: PositionCapabilityDetails;
  options: KeyedCode[];
  update: Dispatch<PositionCapabilityDetailsAction>;
}

export function PositionCapabilitiesEditor(props: PositionCapabilitiesEditorProps) {
  const { primaryPosition, details, options, update } = props;
  
  return <>
    <PositionCapabilityRow 
      position='Pitcher'
      label='Pitcher'
      grade={details.pitcher.key as Grade}
      update={gradeCode => update({ type: 'updatePitcherAbility', ability: gradeCode })}
    />
    <PositionCapabilityRow 
      position='Catcher'
      label='Catcher'
      grade={details.catcher.key as Grade}
      update={gradeCode => update({ type: 'updateCatcherAbility', ability: gradeCode })}
    />
    <PositionCapabilityRow 
      position='FirstBase'
      label='First Base'
      grade={details.firstBase.key as Grade}
      update={gradeCode => update({ type: 'updateFirstBaseAbility', ability: gradeCode })}
    />
    <PositionCapabilityRow 
      position='SecondBase'
      label='Second Base'
      grade={details.secondBase.key as Grade}
      update={gradeCode => update({ type: 'updateSecondBaseAbility', ability: gradeCode })}
    />
    <PositionCapabilityRow 
      position='ThirdBase'
      label='Third Base'
      grade={details.thirdBase.key as Grade}
      update={gradeCode => update({ type: 'updateThirdBaseAbility', ability: gradeCode })}
    />
    <PositionCapabilityRow 
      position='Shortstop'
      label='Shortstop'
      grade={details.shortstop.key as Grade}
      update={gradeCode => update({ type: 'updateShortstopAbility', ability: gradeCode })}
    />
    <PositionCapabilityRow 
      position='LeftField'
      label='Left Field'
      grade={details.leftField.key as Grade}
      update={gradeCode => update({ type: 'updateLeftFieldAbility', ability: gradeCode })}
    />
    <PositionCapabilityRow 
      position='CenterField'
      label='Center Field'
      grade={details.centerField.key as Grade}
      update={gradeCode => update({ type: 'updateCenterFieldAbility', ability: gradeCode })}
    />
    <PositionCapabilityRow 
      position='RightField'
      label='Right Field'
      grade={details.rightField.key as Grade}
      update={gradeCode => update({ type: 'updateRightFieldAbility', ability: gradeCode })}
    />
  </>

  function PositionCapabilityRow(props: { 
    position: Position,
    label: string, 
    grade: Grade, 
    update: (grade: KeyedCode) => void 
  }) {
    const { position, label, grade, update } = props;
    const id = `${label}-capability`;

    return <FlexRow gap='16px' withBottomPadding>
    <FlexFracItem frac='1/6'>
      <FieldLabel htmlFor={id}>{label}</FieldLabel>
    </FlexFracItem>
    <FlexFracItem frac='1/8'>
      <SelectField
        id={id}
        value={grade}
        disabled={primaryPosition.key === position}
        onChange={grade => update(toKeyedCode(options, grade))}
      >
        {toOptions(options)}
      </SelectField>
    </FlexFracItem>
    <FlexFracItem frac='1/8'>
      <GradeLetter grade={grade} />
    </FlexFracItem>
  </FlexRow>
  }
}