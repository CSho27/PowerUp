import { ListboxOption } from "@reach/listbox";
import { Dispatch, ReactNode } from "react";
import styled from "styled-components";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow"
import { Icon } from "../../components/icon/icon";
import { OptionElement, SelectField } from "../../components/SelectField/selectField";
import { fromOptions, toOptions, tryFromOptions, withEmptyOption } from "../../components/SelectField/selectFieldHelpers";
import { KeyedCode } from "../shared/keyedCode";
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
          >{options.skinColorOptions.map(c => toColorOption(c, getSkinColor))}</SelectField> 
          </>}
        </FlexFracItem> 
        <FlexFracItem frac='1/2'>
          {details.face?.canChooseEyes && <>
          <FieldLabel htmlFor='eyes'>Eyes</FieldLabel>
          <SelectField 
            id='eyes'
            value={details.eyeColor.key}
            onChange={eyes => update({ type: 'updateEyeColor', eyeColor: fromOptions(options.eyeColorOptions, eyes) })}
          >{options.eyeColorOptions.map(c => toColorOption(c, getEyeColor))}</SelectField> 
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
          >{options.hairColorOptions.map(c => toColorOption(c, getHairColor))}</SelectField> 
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
          >{options.hairColorOptions.map(c => toColorOption(c, getHairColor))}</SelectField> 
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
          >{options.batColorOptions.map(toBatColorOption)}</SelectField> 
        </FlexFracItem> 
        <FlexFracItem frac='1/2'>
          <FieldLabel htmlFor='glove'>Glove</FieldLabel>
          <SelectField 
            id='glove'
            value={details.gloveColor.key}
            onChange={glove => update({ type: 'updateGloveColor', gloveColor: fromOptions(options.gloveColorOptions, glove) })}
          >{options.gloveColorOptions.map(c => toColorOption(c, getGloveColor))}</SelectField> 
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
          >{options.eyewearFrameColorOptions.map(c => toColorOption(c, getFrameColor))}</SelectField> 
        </FlexFracItem>}
        {!!details.eyewearType && details.eyewearType.key != 'EyeBlack' && 
        <FlexFracItem frac='1/3'>
          <FieldLabel htmlFor='eyewearLensColor'>Lens</FieldLabel>
          <SelectField 
            id='eyewearLensColor'
            value={details.eyewearLensColor.key}
            onChange={color => update({ type: 'updateEyewearLensColor', lensColor: fromOptions(options.eyewearLensColorOptions, color) })}
          >{options.eyewearLensColorOptions.map(c => toColorOption(c, getLensColor))}</SelectField> 
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
          >{options.accessoryColorOptions.map(c => toColorOption(c, getAccessoryColor))}</SelectField> 
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
          >{withEmptyOption(options.accessoryColorOptions.map(c => toColorOption(c, getAccessoryColor)))}</SelectField> 
        </FlexFracItem> 
        <FlexFracItem frac='1/2'>
          <FieldLabel htmlFor='leftWristband'>Left Wristband</FieldLabel>
          <SelectField 
            id='leftWristband'
            value={details.leftWristbandColor?.key}
            onChange={color => update({ type: 'updateLeftWristbandColor', color: tryFromOptions(options.accessoryColorOptions, color) })}
          >{withEmptyOption(options.accessoryColorOptions.map(c => toColorOption(c, getAccessoryColor)))}</SelectField> 
        </FlexFracItem> 
      </FlexRow>
    </FlexFracItem>
  </FlexRow>
}

function toColorOption(code: KeyedCode, getColor: (key: string) => string | undefined) {
  return <ListboxOption 
    key={code.key} 
    value={code.key}
  >
    <div style={{ display: 'flex', alignItems: 'center', gap: '4px' }}>
      <Icon icon='square' style={{ color: getColor(code.key) }}/>
      <div>
        {code.name}
      </div>
    </div>
  </ListboxOption>
}

function toBatColorOption(code: KeyedCode): OptionElement {
  switch(code.key) {
    case 'Natural':
      return toColorOption(code, () => 'hsl(37deg 85% 62%)'); 
    case 'Black':
      return toColorOption(code, () => 'hsl(25deg 54% 14%)'); 
    case 'Natural_Black':
      return getSplitColorOption(code, 'hsl(52deg 76% 66%)', 'hsl(25deg 54% 14%)')
    case 'Black_Natural': 
      return getSplitColorOption(code, 'hsl(25deg 54% 14%)', 'hsl(52deg 76% 66%)')
    case 'Red':
      return toColorOption(code, () => 'hsl(11deg 100% 44%)'); 
    case 'Brown': 
      return toColorOption(code, () => 'hsl(25deg 81% 33%)'); 
    case 'Red_Black':
      return getSplitColorOption(code, 'hsl(11deg 100% 44%)', 'hsl(25deg 54% 14%)')
    default: 
      throw 'Unexpected bat color';
  }

  function getSplitColorOption(code: KeyedCode, firstColor: string, secondColor: string) {
    return <ListboxOption 
      key={code.key} 
      value={code.key}
    >
      <div style={{ display: 'flex', alignItems: 'center', gap: '4px' }}>
        <SplitColorIcon icon='square' firstColor={firstColor} secondColor={secondColor} />
        <div>
          {code.name}
        </div>
      </div>
    </ListboxOption> 
    }
}

function getSkinColor(key: string): string | undefined {
  switch(key) {
    case 'One':
      return 'hsl(12deg 91% 83%)';
    case 'Two':
      return 'hsl(30deg 94% 73%)';
    case 'Three':
      return 'hsl(21 49% 54%)';
    case 'Four':
      return 'hsl(13 36% 40%)';
    case 'Five':
      return 'hsl(21 30% 32%)';
    default:
      return undefined;
  }
}

function getEyeColor(key: string): string | undefined {
  switch(key) {
    case 'Blue':
      return 'hsl(227deg 100% 49%)';
    case 'Brown':
      return 'hsl(20deg 18% 25%)';
    default:
      return undefined;
  }
}

function getHairColor(key: string): string | undefined {
  switch(key) {
    case 'LightBlonde':
      return 'hsl(43deg 44% 75%)';
    case 'Blonde':
      return 'hsl(30deg 45% 57%)';
    case 'DarkBlonde':
      return 'hsl(34deg 26% 43%)';
    case 'DarkBrown':
      return 'hsl(45deg 26% 25%)';
    case 'VeryLightBrown':
      return 'hsl(23deg 32% 60%)';
    case 'VeryLightBrownWithRed':
      return 'hsl(11deg 43% 59%)';
    case 'LightBrown':
      return 'hsl(22deg 29% 45%)';
    case 'Brown':
      return 'hsl(21deg 30% 32%)';
    case 'Black':
      return 'hsl(0deg 0% 22%)';
    case 'Gray':
      return 'hsl(0deg 0% 76%)';
    case 'Yellow':
      return 'hsl(50deg 63% 70%)';
    case 'Red':
      return 'hsl(7deg 67% 57%)';
    case 'Blue':
      return 'hsl(227deg 67% 57%)';
    case 'Green':
      return 'hsl(104deg 67% 57%)';
    default: 
      return undefined;
  }
}

function getGloveColor(key: string): string | undefined {
  switch(key) {
    case 'Orange':
      return 'hsl(19deg 93% 48%)'; 
    case 'Black':
      return 'hsl(0deg 0% 24%)';
    case 'Tan':
      return 'hsl(38deg 94% 42%)'; 
    case 'Blue':
      return 'hsl(228deg 86% 45%)';
    case 'Brown':
      return 'hsl(24deg 97% 31%)'; 
    case 'Red':
      return 'hsl(3deg 74% 51%)'; 
    default:
      return undefined;
  }
}

function getFrameColor(key: string): string | undefined {
  switch(key) {
    case 'Black':
      return 'hsl(0deg 0% 25%)';
    case 'Gray':
      return 'hsl(0deg 0% 80%)';
    case 'Red':
      return 'hsl(5deg 73% 51%)';
    case 'Blue':
      return 'hsl(228deg 52% 46%)'
    case 'Gold':
      return 'hsl(52deg 35% 68%)'
    default:
      return undefined;
  }
}

function getLensColor(key: string): string | undefined {
  switch(key) {
    case 'Clear':
      return 'hsl(0deg 2% 90%)';
    case 'Orange':
      return 'hsl(36deg 86% 55%)';
    case 'Black':
      return 'hsl(0deg 0% 20%)';
    default:
      return undefined;
  }
}

function getAccessoryColor(key: string): string | undefined {
  switch(key) {
    case 'Black':
      return 'hsl(0deg 0% 9%)';
    case 'White':
      return 'hsl(0deg 0% 98%)';
    case 'Red':
      return 'hsl(4deg 100% 49%)';
    case 'DarkBlue':
      return 'hsl(227deg 100% 49%)';
    case 'Pink':
      return 'hsl(300deg 96% 60%)';
    case 'Gray':
      return 'hsl(0deg 0% 47%)';
    case 'Orange':
      return 'hsl(30deg 97% 54%)';
    case 'Yellow':
      return 'hsl(56deg 100% 49%)';
    case 'LightBlue':
      return 'hsl(196deg 100% 49%)';
    case 'Green':
      return 'hsl(124deg 88% 27%)';
    default:
      return undefined;
  }
}

const SplitColorIcon = styled(Icon)<{ firstColor: string, secondColor: string }>`
  background: linear-gradient(-45deg, ${p=> p.firstColor} 0 50%, ${p => p.secondColor} 50% )
`