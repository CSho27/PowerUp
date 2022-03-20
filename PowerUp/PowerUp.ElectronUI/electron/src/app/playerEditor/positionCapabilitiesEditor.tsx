import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow";
import { SelectField } from "../../components/SelectField/selectField";
import { KeyedCode } from "../shared/keyedCode";
import { PositionCode } from "../shared/positionCode";

export interface PositionCapabilitiesEditorProps {
  primaryPosition: PositionCode;
  positionCapabilityDetails: any;
  options: KeyedCode[];
  update: any;
}

export function PositionCapabilitiesEditor(props: PositionCapabilitiesEditorProps) {
  const { primaryPosition, positionCapabilityDetails, options, update } = props;
  
  return <>
    <FlexRow gap='16px'>
      <FlexFracItem frac='1/4'>
        Something
      </FlexFracItem>
    </FlexRow>
  </>
}