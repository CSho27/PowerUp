import { ChangeEvent } from "react";
import styled from "styled-components";
import { COLORS } from "../../style/constants";

export interface TextFieldProps {
  value: string | undefined;
  placeholder?: string;
  autoFocus?: boolean;
  maxLength?: number;
  allowedCharacters?: string[]; 
  onChange: (value: string) => void;
}

export function TextField(props: TextFieldProps) {
  const { value, placeholder, autoFocus, maxLength, allowedCharacters, onChange } = props;

  return <TextFieldWrapper>
    <TextInput
      placeholder={placeholder ?? 'Enter text'}
      value={value ?? ''}
      maxLength={maxLength}
      onChange={handleTextChanged}
      autoFocus={autoFocus}
      />
  </TextFieldWrapper> 

  function handleTextChanged(event: ChangeEvent<HTMLInputElement>) {
    const text = event.target.value;
    const trimmedText = text;
    onChange(trimmedText);
  }
}

const TextFieldWrapper = styled.div`
  border: solid 2px ${COLORS.transparent.regular_100};
  border-radius: 4px;
  width: 100%;

  &:focus-within {
    border-color: ${COLORS.primaryBlue.regular_45};
  }
`

const TextInput = styled.input`
  width: 100%;
  border: none;
  outline: none;
`