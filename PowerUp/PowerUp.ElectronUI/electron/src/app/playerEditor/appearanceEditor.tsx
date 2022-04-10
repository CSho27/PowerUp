import { Dispatch } from "react";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow"
import { SelectField } from "../../components/SelectField/selectField";
import { fromOptions, toOptions } from "../../components/SelectField/selectFieldHelpers";
import { AppearanceOptions } from "./loadPlayerEditorApiClient"
import { Appearance, AppearanceAction } from "./playerEditorState";

export interface AppearanceEditorProps {
  options: AppearanceOptions;
  details: Appearance;
  update: Dispatch<AppearanceAction>;
}

export function AppearanceEditor(props: AppearanceEditorProps) {
  const { options, details, update } = props;
  
  return <FlexRow gap='16px'>
    <FlexFracItem frac='1/3'>
      <FieldLabel htmlFor='face'>Face</FieldLabel>
      <SelectField 
        id='Face'
        value={details.face?.id}
        onChange={faceId => update({ type: 'updateFace', face: fromOptions(options.faceOptions, faceId) })}
      >{toOptions(options.faceOptions)}</SelectField>
    </FlexFracItem>
  </FlexRow>
}