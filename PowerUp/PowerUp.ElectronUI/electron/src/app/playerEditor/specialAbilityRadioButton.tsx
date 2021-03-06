import styled from "styled-components";
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader";
import { RadioButton } from "../../components/radioButton/radioButton";
import { COLORS, FONT_SIZES } from "../../style/constants";

export interface SpecialAbilityRadioButtonProps {
  name: string;
  label: string;
  effect: SpecialAbilityEffect;
  numericValue?: string;
  checked: boolean;
  onSelect: () => void;
  id?: string;
  disabled?: boolean;
}

export type SpecialAbilityEffect = 'Neutral' | 'Positive' | 'Negative';

export function SpecialAbilityRadioButton(props: SpecialAbilityRadioButtonProps) {
  const { name, label, effect, numericValue, checked, disabled, onSelect, id } = props;
  
  return <OuterWrapper title={label} effect={effect} checked={checked} disabled={disabled} onClick={disabled ? () => {} : onSelect}>
    <InnerWrapper>
      <RadioButtonContainer>
        <RadioButton 
          id={id}
          groupName={name}
          value={label}
          checked={checked}
          disabled={disabled}
          onSelect={() => {}}
        />
      </RadioButtonContainer>
      <LabelContainer>
        <OutlineHeader
          fontSize={FONT_SIZES._18}
          strokeWeight={1}  
          textColor={COLORS.white.regular_100}
          strokeColor={COLORS.richBlack.regular_5}
          >
          {label}
        </OutlineHeader>
      </LabelContainer>
    </InnerWrapper>
    <NumericSection>
      {numericValue}
    </NumericSection>   
  </OuterWrapper>
}

const colors: { [key in SpecialAbilityEffect]: string } = {
  Neutral: `
    --inner-color: ${COLORS.jet.lighter_71};
    --outer-color: ${COLORS.jet.light_50};
  `,
  Positive: `
    --inner-color: ${COLORS.primaryBlue.lighter_69};
    --outer-color: ${COLORS.primaryBlue.regular_45};
  `,
  Negative: `
    --inner-color: ${COLORS.secondaryRed.light_69};
    --outer-color: ${COLORS.secondaryRed.regular_44};
  `,
}

const OuterWrapper = styled.div<{ effect: SpecialAbilityEffect, checked: boolean, disabled?: boolean }>`
  ${p => colors[p.effect]}
  background-color: var(--outer-color);
  border-radius: 6px;
  min-width: 180px;
  padding: 3px;
  display: flex;
  align-items: center;
  white-space: nowrap;
  overflow-x: hidden;
  line-height: 1;
  opacity: ${p => p.checked ? '1' : '.4'};
  cursor: ${p => p.disabled ? 'undefined' : 'pointer' };
  user-select: none;

  :hover {
    opacity: ${p => p.disabled 
      ? 'undefined'
      : p.checked ? '1' : '.5'};
  }
`

const InnerWrapper = styled.div`
  flex: auto;
  overflow-x: hidden;
  border-radius: 4px;
  background-color: var(--inner-color);
  padding: 4px;
  display: flex;
  align-items: center;
`

const RadioButtonContainer = styled.div`
  flex: 0 0 auto;
`

const LabelContainer = styled.div`
  flex: auto;
  text-align: center;
`

const NumericSection = styled.div`
  flex: 0 0 24px;
  font-size: ${FONT_SIZES._18};
  color: ${COLORS.white.regular_100};
  text-align: center;
`
