import styled from "styled-components";
import { COLORS } from "../../style/constants";

export interface RadioButtonProps {
  groupName: string;
  value: string;
  checked: boolean;
  onSelect: () => void;
  id?: string;
  disabled?: boolean;
}

export function RadioButton(props: RadioButtonProps) {
  const { groupName, value, checked, onSelect, id, disabled } = props;
  
  return <RadioButtonWrapper>
    <RadioButtonCircle>
      {checked && <RadioButtonSelectionCircle/>}
    </RadioButtonCircle>
    <RadioButtonInput 
      id={id}
      type='radio'
      name={groupName}
      value={value}
      checked={checked}
      disabled={disabled}
      onChange={onSelect}
    />
  </RadioButtonWrapper> 
}

const RadioButtonWrapper = styled.div`
  height: 16px;
  width: 16px;
  position: relative;
`

const RadioButtonCircle = styled.div`
  height: 100%;
  width: 100%;
  border-radius: 50%;
  padding: 2px;
  background-color: ${COLORS.white.regular_100};
  border: 1px solid ${COLORS.jet.lighter_71};
`

const RadioButtonSelectionCircle = styled.div`
  height: 100%;
  width: 100%;
  border-radius: 50%;
  background-color: ${COLORS.primaryBlue.regular_45};
`

const RadioButtonInput = styled.input`
  position: absolute;
  top: 0;
  left: 0;
  height: 100%;
  width: 100%;
  cursor: pointer;
  opacity: 0;

  &:disabled {
    cursor: default;
  }
`