import { ChangeEvent, ReactElement, ReactNode, useRef, useState } from "react";
import styled from "styled-components";
import { COLORS } from "../../style/constants";
import { Icon } from "../icon/icon";

export interface SelectFieldProps {
  value: string | undefined;
  options: SelectFieldOption[];
  onChange: (value: string) => void;
}

export interface SelectFieldOption {
  value: string;
  displayName: string;
  subText?: string;
}

export function SelectField(props: SelectFieldProps) {
  const { value, options, onChange } = props;
  
  const [text, setText] = useState(options.find(o => o.value === value)?.displayName ?? '');

  return <Wrapper>
    <SelectionInput list='data' value={text} onChange={e => setText(e.target.value)} onClick={() => console.log('click')} onDoubleClick={() => console.log('double-click')} />
    <datalist id='data'>
      {options.map(toOptionElement)}
    </datalist>
    <DownArrowCover>
      <Icon icon='chevron-down'/>
    </DownArrowCover>
  </Wrapper>

  function toOptionElement(option: SelectFieldOption) {
    return <option key={option.value} value={option.displayName}>{option.subText}</option>
  }

  function handleChange(event: ChangeEvent<HTMLInputElement>) {
    onChange(event.target.value);
  }
}

const Wrapper = styled.div`
  position: relative;
  border: solid 2px ${COLORS.transparent.regular_100};
  border-radius: 4px;
  background-color: ${COLORS.white.regular_100};
  width: 100%;
  display: flex;
  gap: 24px;
  align-items: center;

  &:focus-within {
    border-color: ${COLORS.primaryBlue.regular_45};
  }
`

const SelectionInput = styled.input`
  border: none;
  outline: none;
  width: 100%;
  height: 100%;
`

const DownArrowCover = styled.div`
  background-color: ${COLORS.white.regular_100};
  position: absolute;
  right: 0px;
  top: 0px;
  bottom: 0px;
  padding: 0px 8px;
  pointer-events: none;
`