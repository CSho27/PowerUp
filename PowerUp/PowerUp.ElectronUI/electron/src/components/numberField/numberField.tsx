import { FocusEvent, ChangeEvent, useEffect, useState } from "react";
import styled from "styled-components";
import { COLORS } from "../../style/constants";
import { Icon } from "../icon/icon";

interface BaseNumberFieldProps {
  id?: string;
  disabled?: boolean;
  placeholder?: string;
  autoFocus?: boolean;
  min?: number;
  max?: number;
}

export interface PossiblyUndefinedNumberFieldProps extends BaseNumberFieldProps {
  type: 'PossiblyUndefined'
  value: number | undefined;
  onChange: (value: number | undefined) => void;
}

export interface DefinedNumberFieldProps extends BaseNumberFieldProps {
  type: 'Defined'
  value: number;
  onChange: (value: number) => void;
}

export function NumberField(props: PossiblyUndefinedNumberFieldProps | DefinedNumberFieldProps) {
  const { id, type, disabled, placeholder, autoFocus, max, min, onChange } = props;
  
  const [value, setValue]  = useState(props.value);
  useEffect(() => {
    if(props.value != value)
      setValue(props.value);
  }, [props.value])

  return <NumberFieldWrapper disabled={!!disabled}>
    <NumberInput
      id={id}
      type='number'
      disabled={disabled}
      placeholder={placeholder ?? 'Enter number'}
      value={value ?? ''}
      onChange={handleNumberChanged}
      onBlur={handleBlur}
      autoFocus={autoFocus}
    />
    <NumberInputStepperWrapper>
      <NumberInputStepper 
        tabIndex={0} 
        onClick={() => setValue(scrubValue((value ?? 0) + 1))}
      >
        <Icon icon='chevron-up'/>
      </NumberInputStepper>
      <NumberInputStepper  
        tabIndex={0} 
        onClick={() => setValue(scrubValue((value ?? 0) - 1))}
      >
        <Icon icon='chevron-down' />
      </NumberInputStepper>
    </NumberInputStepperWrapper>
  </NumberFieldWrapper> 

  function handleNumberChanged(event: ChangeEvent<HTMLInputElement>) {
    const textValue = event.target.value;
    const numberValue = textValue?.length > 0
      ? Number.parseInt(textValue)
      : undefined
    setValue(numberValue);
  }

  function handleBlur() {
    if(type === 'Defined')
      handleDefinedBlur();
    else 
      handlePossiblyUndefinedBlur();
  }

  function scrubValue(value: number | undefined): number | undefined {
    if(type === 'Defined')
      return scrubValueForDefined(value);
    else
      return scrubValueForPossiblyUndefined(value);
  }

  function scrubValueForDefined(value: number | undefined): number {
    let newValue: number = value ?? min ?? 0;
    if(min && newValue < min)
      return min;
    else if(max && newValue > max)
      return max;
    else
      return newValue    
  }

  function scrubValueForPossiblyUndefined(value: number| undefined): number | undefined {
    let newValue = value;
    if(min && newValue && newValue < min)
      return newValue;
    else if(max && newValue && newValue > max)
      return newValue;
    else
      return newValue;    
  }

  function handleDefinedBlur() {
    if(type !== 'Defined')
      throw 'invalid operation';
    const newValue = scrubValueForDefined(value)
    setValue(newValue)
    onChange(newValue);
  }

  function handlePossiblyUndefinedBlur() {
    if(type !== 'PossiblyUndefined')
      throw 'invalid operation';
    const newValue = scrubValueForPossiblyUndefined(value)
    setValue(newValue)
    onChange(newValue);
    (onChange as (value: number | undefined) => void)(newValue); 
  }
}

const NumberFieldWrapper = styled.div<{ disabled: boolean }>`
  position: relative;
  background-color: ${p => p.disabled ? COLORS.jet.superlight_85 : COLORS.white.regular_100};
  border: solid 2px ${COLORS.transparent.regular_100};
  border-radius: 2px;
  width: 100%;

  &:focus-within {
    border-color: ${COLORS.primaryBlue.regular_45};
  }
`

const NumberInput = styled.input`
  width: 100%;
  border: none;
  border-radius: 2px;
  outline: none;

  &:disabled {
    background-color: ${COLORS.jet.superlight_85};
    color: ${COLORS.richBlack.regular_5};
  }
`

const NumberInputStepperWrapper = styled.div`
  position: absolute;
  right: 0;
  top: 0;
  height: 100%;
  width: 1.1rem;
  text-align: center;
  background-color: ${COLORS.white.regular_100};
`

const NumberInputStepper = styled.a`
  display: block;
  line-height: 0;
  font-size: .8rem;
  cursor: pointer;

  &:hover {
    color: ${COLORS.primaryBlue.regular_45};
  }
`