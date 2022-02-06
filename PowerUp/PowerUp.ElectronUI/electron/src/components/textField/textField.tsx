import styled, { CSSProperties } from "styled-components";
import { COLORS } from "../../style/constants";

export interface TextFieldProps {
  value: string | undefined;
  placeholder?: string;
  autoFocus?: boolean;
  onChange: (value: string) => void;
}

export function TextField(props: TextFieldProps) {
  const { value, placeholder, autoFocus, onChange } = props;

  return <TextInput
    placeholder={placeholder ?? 'Enter text'}
    value={value ?? ''}
    onChange={(event: React.ChangeEvent<HTMLInputElement>) => onChange(event.target.value)}
    autoFocus={autoFocus}
  />
}

const TextInput = styled.input`
  ${/*
  outline: none;

  &:focus-within {
    outline: solid 2px ${COLORS.PP_Blue70};
  }
  */''}
`