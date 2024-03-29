import { ChangeEvent } from "react";
import styled from "styled-components";
import { COLORS } from "../../style/constants";

export interface TextFieldProps {
  value: string | undefined;
  id?: string;
  disabled?: boolean;
  placeholder?: string;
  autoFocus?: boolean;
  maxLength?: number;
  allowedCharacters?: string[]; 
  onChange: (value: string) => void;
}

export function TextField(props: TextFieldProps) {
  const { value, id, disabled, placeholder, autoFocus, maxLength, allowedCharacters, onChange } = props;

  return <TextFieldWrapper disabled={!!disabled}>
    <TextInput
      id={id}
      type='text'
      disabled={disabled}
      placeholder={placeholder ?? 'Enter text'}
      value={value ?? ''}
      maxLength={maxLength}
      onChange={handleTextChanged}
      autoFocus={autoFocus}
    />
  </TextFieldWrapper> 

  function handleTextChanged(event: ChangeEvent<HTMLInputElement>) {
    const text = event.target.value;
    const trimmedText = removeDisallowedCharacters(text);
    onChange(trimmedText);
  }

  function removeDisallowedCharacters(text: string): string {
    if(!allowedCharacters)
      return text;

    let trimmedText = '';
    for(let i=0; i<text.length; i++) {
      const currentChar = text.charAt(i);
      if(allowedCharacters.some(c => c === currentChar))
        trimmedText = trimmedText.concat(currentChar);
    }

    return trimmedText;
  } 
}

const TextFieldWrapper = styled.div<{ disabled: boolean }>`
  background-color: ${p => p.disabled ? COLORS.jet.superlight_85 : COLORS.white.regular_100};
  border: solid 2px ${COLORS.transparent.regular_100};
  border-radius: 2px;
  width: 100%;

  &:focus-within {
    border-color: ${COLORS.primaryBlue.regular_45};
  }
`

const TextInput = styled.input`
  width: 100%;
  border: none;
  border-radius: 2px;
  outline: none;

  &:disabled {
    background-color: ${COLORS.jet.superlight_85};
    color: ${COLORS.richBlack.regular_5};
  }
`

export const digits = Array.from(Array(10), (v, i) => i.toString());
export const powerProsCharacters = [
  ' ',
  '.',
  ';',
  '?',
  '!',
  '^',
  '_',
  '\u2013',
  '/',
  '|',
  '',
  '(',
  ')',
  '[',
  ']',
  '{',
  '}',
  '+',
  '-',
  '=',
  '<',
  '>',
  '0',
  '1',
  '2',
  '3',
  '4',
  '5',
  '6',
  '7',
  '8',
  '9',
  'A',
  'B',
  'C',
  'D',
  'E',
  'F',
  'G',
  'H',
  'I',
  'J',
  'K',
  'L',
  'M',
  'N',
  'O',
  'P',
  'Q',
  'R',
  'S',
  'T',
  'U',
  'V',
  'W',
  'X',
  'Y',
  'Z',
  'a',
  'b',
  'c',
  'd',
  'e',
  'f',
  'g',
  'h',
  'i',
  'j',
  'k',
  'l',
  'm',
  'n',
  'o',
  'p',
  'q',
  'r',
  's',
  't',
  'u',
  'v',
  'w',
  'x',
  'y',
  'z'
]

export const fileSystemCharacters = [
  ' ',
  ';',
  '!',
  '^',
  '_',
  '\u2013',
  '',
  '(',
  ')',
  '[',
  ']',
  '{',
  '}',
  '+',
  '-',
  '=',
  '0',
  '1',
  '2',
  '3',
  '4',
  '5',
  '6',
  '7',
  '8',
  '9',
  'A',
  'B',
  'C',
  'D',
  'E',
  'F',
  'G',
  'H',
  'I',
  'J',
  'K',
  'L',
  'M',
  'N',
  'O',
  'P',
  'Q',
  'R',
  'S',
  'T',
  'U',
  'V',
  'W',
  'X',
  'Y',
  'Z',
  'a',
  'b',
  'c',
  'd',
  'e',
  'f',
  'g',
  'h',
  'i',
  'j',
  'k',
  'l',
  'm',
  'n',
  'o',
  'p',
  'q',
  'r',
  's',
  't',
  'u',
  'v',
  'w',
  'x',
  'y',
  'z'
]