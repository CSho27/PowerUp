import { Dispatch } from "react";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow";
import { Grade, GradeLetter } from "../../components/gradeLetter/gradeLetter";
import { SelectField } from "../../components/SelectField/selectField";
import { fromOptions, toOptions } from "../../components/SelectField/selectFieldHelpers";
import { KeyedCode } from "../shared/keyedCode";
import { Position, PositionCode } from "../shared/positionCode";
import { PositionCapabilityDetails, PositionCapabilityDetailsAction } from "./playerEditorState";

export interface PositionCapabilitiesEditorProps {
  primaryPosition: PositionCode;
  details: PositionCapabilityDetails;
  options: KeyedCode[];
  disabled?: boolean;
  update: Dispatch<PositionCapabilityDetailsAction>;
}

export function PositionCapabilitiesEditor(props: PositionCapabilitiesEditorProps) {
  const { primaryPosition, details, options, disabled: editorDisabled, update } = props;
  
  return <>
    <PositionCapabilityRow 
      position='Pitcher'
      label='Pitcher'
      grade={details.pitcher.key as Grade}
      disabled={editorDisabled}
      update={gradeCode => update({ position: 'Pitcher', ability: gradeCode })}
    />
    <PositionCapabilityRow 
      position='Catcher'
      label='Catcher'
      grade={details.catcher.key as Grade}
      disabled={editorDisabled}
      update={gradeCode => update({ position: 'Catcher', ability: gradeCode })}
    />
    <PositionCapabilityRow 
      position='FirstBase'
      label='First Base'
      grade={details.firstBase.key as Grade}
      disabled={editorDisabled}
      update={gradeCode => update({ position: 'FirstBase', ability: gradeCode })}
    />
    <PositionCapabilityRow 
      position='SecondBase'
      label='Second Base'
      grade={details.secondBase.key as Grade}
      disabled={editorDisabled}
      update={gradeCode => update({ position: 'SecondBase', ability: gradeCode })}
    />
    <PositionCapabilityRow 
      position='ThirdBase'
      label='Third Base'
      grade={details.thirdBase.key as Grade}
      disabled={editorDisabled}
      update={gradeCode => update({ position: 'ThirdBase', ability: gradeCode })}
    />
    <PositionCapabilityRow 
      position='Shortstop'
      label='Shortstop'
      grade={details.shortstop.key as Grade}
      disabled={editorDisabled}
      update={gradeCode => update({ position: 'Shortstop', ability: gradeCode })}
    />
    <PositionCapabilityRow 
      position='LeftField'
      label='Left Field'
      grade={details.leftField.key as Grade}
      disabled={editorDisabled}
      update={gradeCode => update({ position: 'LeftField', ability: gradeCode })}
    />
    <PositionCapabilityRow 
      position='CenterField'
      label='Center Field'
      grade={details.centerField.key as Grade}
      disabled={editorDisabled}
      update={gradeCode => update({ position: 'CenterField', ability: gradeCode })}
    />
    <PositionCapabilityRow 
      position='RightField'
      label='Right Field'
      grade={details.rightField.key as Grade}
      disabled={editorDisabled}
      update={gradeCode => update({ position: 'RightField', ability: gradeCode })}
    />
  </>

  function PositionCapabilityRow(props: { 
    position: Position,
    label: string, 
    grade: Grade,
    disabled?: boolean,
    update: (grade: KeyedCode) => void 
  }) {
    const { position, label, grade, disabled, update } = props;
    const id = `${label}-capability`;

    return <FlexRow gap='16px' withBottomPadding>
    <FlexFracItem frac='1/6'>
      <FieldLabel htmlFor={id}>{label}</FieldLabel>
    </FlexFracItem>
    <FlexFracItem frac='1/8'>
      <SelectField
        id={id}
        value={grade}
        disabled={disabled || primaryPosition.key === position}
        onChange={grade => update(fromOptions(options, grade))}
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