import styled from "styled-components";
import { COLORS } from "../../style/constants";

export interface ToggleSwitchProps {
  isOn: boolean;
  onToggle: () => void;
  id?: string;
  disabled?: boolean;
}

export function ToggleSwitch(props: ToggleSwitchProps) {
  const { isOn, onToggle, id, disabled } = props;
  
  return <ToggleBackground isOn={isOn} disabled={!!disabled}>
    <ToggleSwitchNode />
    <ToggleInput
      id={id} 
      type='checkbox'
      checked={isOn}
      onChange={onToggle}
      disabled={disabled}
    />
  </ToggleBackground>
}

const ToggleBackground = styled.div<{ isOn: boolean, disabled: boolean }>`
  border: inset 1px ${COLORS.richBlack.regular_5};
  border-radius: 10px;
  position: relative;
  display: flex;
  align-items: center;
  justify-content: ${p => p.isOn ? 'flex-end' : 'flex-start'};
  background-color: ${p => p.isOn ? COLORS.primaryBlue.regular_45 : COLORS.jet.light_50 };
  color: ${COLORS.white.regular_100};
  width: 40px;
  height: 20px;
  cursor: ${p => p.disabled ? 'default' : 'pointer' };
  opacity: ${p => p.disabled ? '.45' : '1' };
`

const ToggleSwitchNode = styled.div`
  background-color: ${COLORS.white.regular_100};
  width: 18px;
  height: 18px;
  border-radius: 50%;
`

const ToggleInput = styled.input`
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