import { Dispatch } from "react";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow";
import { NumberField } from "../../components/numberField/numberField";
import { HitterAbilities, HitterAbilitiesAction } from "./playerEditorState";

export interface HitterAbilitiesEditorProps {
  details: HitterAbilities;
  update: Dispatch<HitterAbilitiesAction>;
}

export function HitterAbilitiesEditor(props: HitterAbilitiesEditorProps) {
  const { details, update } = props;
  
  return <>
    <FlexRow gap='16px'>
      <FlexFracItem frac='1/2'>
        <FlexRow gap='16px'>
          <FlexFracItem frac='1/2'>
            <FieldLabel htmlFor='trajectory'>Trajectory</FieldLabel>
          </FlexFracItem>
          <FlexFracItem frac='1/2'>
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
      </FlexFracItem>
      <FlexFracItem frac='1/2'>
      </FlexFracItem>
    </FlexRow>
  </>
}