import styled from "styled-components";
import { COLORS } from "../../style/constants";

export interface CheckboxFieldProps {
  checked: boolean;
  onToggle: () => void;
  id?: string;
  size?: CheckboxFieldSize;
  disabled?: boolean;
}

export type CheckboxFieldSize = 'Small' | 'Medium'

export function CheckboxField(props: CheckboxFieldProps) {
  const { checked, onToggle, id, size, disabled } = props;
  
  return <CheckboxWrapper size={size ?? 'Medium'} checked={checked} disabled={disabled}>
    {checked ? '\u2713': ''}
    <CheckboxInput
      id={id} 
      type='checkbox'
      checked={checked}
      onChange={onToggle}
      disabled={disabled}
    />
  </CheckboxWrapper>
}

const sizingStyles: { [key in CheckboxFieldSize]: string } = {
  Small: `
    --font-size: 10px;
    --box-size: 12px;
    --border-radius: 2px;
  `,
  Medium: `
    --font-size: 12px;
    --box-size: 14px;
    --border-radius: 3px;
  `
}

const CheckboxWrapper = styled.div<{ size: CheckboxFieldSize, checked: boolean, disabled?: boolean }>`
  ${p => sizingStyles[p.size]}
  border: solid 1px ${COLORS.richBlack.regular_5};
  border-radius: var(--border-radius);
  position: relative;
  text-align: center;
  vertical-align: middle;
  line-height: 1;
  background-color: ${p => p.checked 
    ? COLORS.primaryBlue.regular_45 
    : p.disabled
      ? COLORS.jet.regular_25_t20
      : COLORS.transparent.regular_100 };
  color: ${COLORS.white.regular_100};
  font-weight: 700;
  font-size: var(--font-size);
  width: var(--box-size);
  height: var(--box-size);
  cursor: ${p => p.disabled ? 'default' : 'pointer' };
  opacity: ${p => p.disabled ? '.5' : '1' };
`

const CheckboxInput = styled.input<{ disabled?: boolean }>`
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  opacity: 0;
  cursor: pointer;

  &:disabled {
    cursor: default;
  }
`