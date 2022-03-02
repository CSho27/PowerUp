import { ChangeEvent, DetailedHTMLProps, ReactElement, ReactNode, useRef, useState } from "react";
import styled from "styled-components";
import { COLORS } from "../../style/constants";
import { Icon } from "../icon/icon";

export interface SelectFieldProps {
  value: string | number | undefined;
  disabled?: boolean;
  onChange: (value: string) => void;
  children?: OptionElement[];
}

type OptionElement = ReactElement<DetailedHTMLProps<React.OptionHTMLAttributes<HTMLOptionElement>, HTMLOptionElement>>

export function SelectField(props: SelectFieldProps) {
  const { value, disabled, onChange, children } = props;

  const displayedValue = value && getDisplayedValue(value?.toString(), children);

  return <ContentWrapper disabled={!!disabled}>
    <SelectedContent isEmpty={!displayedValue}>
      {displayedValue ?? '.'}
    </SelectedContent>
    <Icon icon='chevron-down'/>
    <Selector disabled={disabled} value={value} onChange={handleChange}>
      {children}
    </Selector>
  </ContentWrapper>

  function handleChange(event: ChangeEvent<HTMLSelectElement>) {
    onChange(event.target.value);
  }

  function getDisplayedValue(value: string | undefined, children: OptionElement[] | undefined): string | undefined {
    if(!children || !value)
      return
    
    const selectedOption = children.find(c => c.props.value === value);
    return !!selectedOption
      ? selectedOption.props.children as string
      : undefined
  }
}

const ContentWrapper = styled.div<{ disabled: boolean }>`
  position: relative;
  border-radius: 2px;
  background-color: ${p => p.disabled ? COLORS.jet.superlight_85 : COLORS.white.regular_100};
  width: 100%;
  padding: 1px 2px;
  padding-right: 8px;
  display: flex;
  gap: 8px;
  align-items: center;

  border: solid 2px ${COLORS.transparent.regular_100};

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

  &:disabled {
    cursor: default;
  }
`