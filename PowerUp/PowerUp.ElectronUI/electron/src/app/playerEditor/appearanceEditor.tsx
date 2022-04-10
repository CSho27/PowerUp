import { Dispatch } from "react";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow"
import { SelectField } from "../../components/SelectField/selectField";
import { fromOptions, toOptions, tryFromOptions } from "../../components/SelectField/selectFieldHelpers";
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
    <FlexFracItem frac='1/2'>
      <FlexRow gap='16px' withBottomPadding>
        <FlexFracItem frac='1/2'>
          <FieldLabel htmlFor='face'>Face</FieldLabel>
          <SelectField 
            id='Face'
            value={details.face?.id}
            onChange={faceId => update({ type: 'updateFace', face: fromOptions(options.faceOptions, faceId) })}
          >{toOptions(options.faceOptions)}</SelectField>
        </FlexFracItem>
        <FlexFracItem frac='1/2'>
          {details.face?.canChooseEyebrows && <>
          <FieldLabel htmlFor='eyebrows'>Eyebrows</FieldLabel>
          <SelectField 
            id='eyebrows'
            value={details.eyebrows.key}
            onChange={eyebrows => update({ type: 'updateEyebrows', eyebrows: fromOptions(options.eyebrowThicknessOptions, eyebrows) })}
          >{toOptions(options.eyebrowThicknessOptions)}</SelectField> 
          </>}
        </FlexFracItem>
      </FlexRow>
      {(details.face.canChooseSkin || details.face.canChooseEyes) &&
      <FlexRow gap='16px' withBottomPadding>
        <FlexFracItem frac='1/2'>
          {details.face?.canChooseSkin && <>
          <FieldLabel htmlFor='skin'>Skin</FieldLabel>
          <SelectField 
            id='skin'
            value={details.skinColor.key}
            onChange={skin => update({ type: 'updateSkinColor', skinColor: fromOptions(options.skinColorOptions, skin) })}
          >{toOptions(options.skinColorOptions)}</SelectField> 
          </>}
        </FlexFracItem> 
        <FlexFracItem frac='1/2'>
          {details.face?.canChooseEyes && <>
          <FieldLabel htmlFor='eyes'>Eyes</FieldLabel>
          <SelectField 
            id='eyes'
            value={details.eyeColor.key}
            onChange={eyes => update({ type: 'updateEyeColor', eyeColor: fromOptions(options.eyeColorOptions, eyes) })}
          >{toOptions(options.eyeColorOptions)}</SelectField> 
          </>}
        </FlexFracItem> 
      </FlexRow>}
      <FlexRow gap='16px' withBottomPadding>
        <FlexFracItem frac='1/2'>
          <FieldLabel htmlFor='hair'>Hair</FieldLabel>
          <SelectField 
            id='hair'
            value={details.hairStyle?.key}
            onChange={hairStyle => update({ type: 'updateHairStyle', hairStyle: tryFromOptions(options.hairStyleOptions, hairStyle) })}
          >{toOptions(options.hairStyleOptions, true)}</SelectField> 
        </FlexFracItem> 
        <FlexFracItem frac='1/2'>
          {!!details.hairStyle && <>
          <FieldLabel htmlFor='hairColor'>Color</FieldLabel>
          <SelectField 
            id='hairColor'
            value={details.hairColor.key}
            onChange={color => update({ type: 'updateHairColor', hairColor: fromOptions(options.hairColorOptions, color) })}
          >{toOptions(options.hairColorOptions)}</SelectField> 
          </>}
        </FlexFracItem> 
      </FlexRow>
      <FlexRow gap='16px' withBottomPadding>
        <FlexFracItem frac='1/2'>
          <FieldLabel htmlFor='facialHair'>Facial Hair</FieldLabel>
          <SelectField 
            id='facialHair'
            value={details.facialHairStyle?.key}
            onChange={hairStyle => update({ type: 'updateFacialHairStyle', facialHairStyle: tryFromOptions(options.facialHairStyleOptions, hairStyle) })}
          >{toOptions(options.facialHairStyleOptions, true)}</SelectField> 
        </FlexFracItem> 
        <FlexFracItem frac='1/2'>
          {!!details.facialHairStyle && <>
          <FieldLabel htmlFor='facialHairColor'>Color</FieldLabel>
          <SelectField 
            id='facialHairColor'
            value={details.facialHairColor.key}
            onChange={color => update({ type: 'updateFacialHairColor', facialHairColor: fromOptions(options.hairColorOptions, color) })}
          >{toOptions(options.hairColorOptions)}</SelectField> 
          </>}
        </FlexFracItem> 
      </FlexRow>
      <h3>Accessories</h3>
      <FlexRow gap='16px' withBottomPadding>
        <FlexFracItem frac='1/2'>
          <FieldLabel htmlFor='bat'>Bat</FieldLabel>
          <SelectField 
            id='bat'
            value={details.batColor.key}
            onChange={bat => update({ type: 'updateBatColor', batColor: fromOptions(options.batColorOptions, bat) })}
          >{toOptions(options.batColorOptions)}</SelectField> 
        </FlexFracItem> 
        <FlexFracItem frac='1/2'>
          <FieldLabel htmlFor='glove'>Glove</FieldLabel>
          <SelectField 
            id='glove'
            value={details.gloveColor.key}
            onChange={glove => update({ type: 'updateGloveColor', gloveColor: fromOptions(options.gloveColorOptions, glove) })}
          >{toOptions(options.gloveColorOptions)}</SelectField> 
        </FlexFracItem> 
      </FlexRow>
      <FlexRow gap='16px' withBottomPadding>
        <FlexFracItem frac='1/3'>
          <FieldLabel htmlFor='eyewearType'>Eyewear</FieldLabel>
          <SelectField 
            id='eyewearType'
            value={details.eyewearType?.key}
            onChange={eyewear => update({ type: 'updateEyewearType', eyewearType: tryFromOptions(options.eyewearTypeOptions, eyewear) })}
          >{toOptions(options.eyewearTypeOptions, true)}</SelectField> 
        </FlexFracItem> 
        {!!details.eyewearType && details.eyewearType.key != 'EyeBlack' &&
        <FlexFracItem frac='1/3'>
          <FieldLabel htmlFor='eyewearFrameColor'>Frame</FieldLabel>
          <SelectField 
            id='eyewearFrameColor'
            value={details.eyewearFrameColor.key}
            onChange={color => update({ type: 'updateEyewearFrameColor', frameColor: fromOptions(options.eyewearFrameColorOptions, color) })}
          >{toOptions(options.eyewearFrameColorOptions)}</SelectField> 
        </FlexFracItem>}
        {!!details.eyewearType && details.eyewearType.key != 'EyeBlack' && 
        <FlexFracItem frac='1/3'>
          <FieldLabel htmlFor='eyewearLensColor'>Lens</FieldLabel>
          <SelectField 
            id='eyewearLensColor'
            value={details.eyewearLensColor.key}
            onChange={color => update({ type: 'updateEyewearLensColor', lensColor: fromOptions(options.eyewearLensColorOptions, color) })}
          >{toOptions(options.eyewearLensColorOptions)}</SelectField> 
        </FlexFracItem>}
      </FlexRow>
      <FlexRow gap='16px' withBottomPadding>
        <FlexFracItem frac='1/2'>
          <FieldLabel htmlFor='earrings'>Earrings</FieldLabel>
          <SelectField 
            id='earrings'
            value={details.earringSide?.key}
            onChange={earringSide => update({ type: 'updateEarringSide', earringSide: tryFromOptions(options.earringSideOptions, earringSide) })}
          >{toOptions(options.earringSideOptions, true)}</SelectField> 
        </FlexFracItem> 
        <FlexFracItem frac='1/2'>
          {!!details.earringSide && <>
          <FieldLabel htmlFor='earringColor'>Color</FieldLabel>
          <SelectField 
            id='earringColor'
            value={details.earringColor.key}
            onChange={color => update({ type: 'updateEarringColor', earringColor: fromOptions(options.accessoryColorOptions, color) })}
          >{toOptions(options.accessoryColorOptions)}</SelectField> 
          </>}
        </FlexFracItem> 
      </FlexRow>
      <FlexRow gap='16px' withBottomPadding>
        <FlexFracItem frac='1/2'>
          <FieldLabel htmlFor='rightWristband'>Right Wristband</FieldLabel>
          <SelectField 
            id='rightWristband'
            value={details.rightWristbandColor?.key}
            onChange={color => update({ type: 'updateRightWristbandColor', color: tryFromOptions(options.accessoryColorOptions, color) })}
          >{toOptions(options.accessoryColorOptions, true)}</SelectField> 
        </FlexFracItem> 
        <FlexFracItem frac='1/2'>
          <FieldLabel htmlFor='leftWristband'>Left Wristband</FieldLabel>
          <SelectField 
            id='leftWristband'
            value={details.leftWristbandColor?.key}
            onChange={color => update({ type: 'updateLeftWristbandColor', color: tryFromOptions(options.accessoryColorOptions, color) })}
          >{toOptions(options.accessoryColorOptions, true)}</SelectField> 
        </FlexFracItem> 
      </FlexRow>
    </FlexFracItem>
  </FlexRow>
}