import { ChangeEvent, DetailedHTMLProps, ReactElement, ReactNode, useRef, useState } from "react";
import styled from "styled-components";
import { COLORS } from "../../style/constants";
import { Icon } from "../icon/icon";

export interface SelectFieldProps {
  value: string | undefined;
  onChange: (value: string) => void;
  children: OptionElement[];
}

type OptionElement = ReactElement<DetailedHTMLProps<React.OptionHTMLAttributes<HTMLOptionElement>, HTMLOptionElement>>

export function SelectField(props: SelectFieldProps) {
  const { value, onChange, children } = props;

  const displayedValue = value && getDisplayedValue(value, children);

  return <Wrapper>
    <SelectedContent isEmpty={!displayedValue}>
      {displayedValue ?? '.'}
    </SelectedContent>
    <Icon icon='chevron-down'/>
    <Selector value={value} onChange={handleChange}>
      {children}
    </Selector>
  </Wrapper>

  function handleChange(event: ChangeEvent<HTMLSelectElement>) {
    onChange(event.target.value);
  }

  function getDisplayedValue(value: string, children: OptionElement[]): string | undefined {
    const selectedOption = children.find(c => c.props.value === value);
    return !!selectedOption
      ? selectedOption.props.children as string
      : undefined
  }
}

const Wrapper = styled.div`
  position: relative;
  border: solid 2px ${COLORS.transparent.regular_100};
  border-radius: 2px;
  background-color: ${COLORS.white.regular_100};
  width: 100%;
  padding: 1px 2px;
  display: flex;
  gap: 24px;
  align-items: center;

  &:focus-within {
    border-color: ${COLORS.primaryBlue.regular_45};
  }
`

const SelectedContent = styled.span<{ isEmpty: boolean; }>`
  opacity: ${p => p.isEmpty ? 0 : undefined};
  flex: auto;
  overflow-x: hidden;
  text-overflow: ellipsis;
`

const Selector = styled.select`
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  opacity: 0;
  cursor: pointer;
`