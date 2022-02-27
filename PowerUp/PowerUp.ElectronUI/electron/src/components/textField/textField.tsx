import styled from "styled-components";
import { COLORS } from "../../style/constants";

export interface TextFieldProps {
  value: string | undefined;
  placeholder?: string;
  autoFocus?: boolean;
  onChange: (value: string) => void;
}

export function TextField(props: TextFieldProps) {
  const { value, placeholder, autoFocus, onChange } = props;

  return <TextFieldWrapper>
    <TextInput
      placeholder={placeholder ?? 'Enter text'}
      value={value ?? ''}
      onChange={(event: React.ChangeEvent<HTMLInputElement>) => onChange(event.target.value)}
      autoFocus={autoFocus}
      />
  </TextFieldWrapper> 
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