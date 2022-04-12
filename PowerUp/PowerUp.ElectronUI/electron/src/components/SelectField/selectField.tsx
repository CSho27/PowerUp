import { ChangeEvent, DetailedHTMLProps, ReactElement, ReactNode } from "react";
import styled from "styled-components";
import { COLORS } from "../../style/constants";
import { Icon } from "../icon/icon";
import { ListboxButton, ListboxPopover, ListboxInput } from "@reach/listbox";
import './selectField.css';

export interface SelectFieldProps {
  value: string | number | undefined;
  id?: string;
  disabled?: boolean;
  onChange: (value: string) => void;
  children?: OptionElement[];
}

type OptionElement = ReactElement<DetailedHTMLProps<React.OptionHTMLAttributes<HTMLOptionElement>, HTMLOptionElement>>

export function SelectField(props: SelectFieldProps) {
  const { disabled, id, onChange, children } = props;
  const value = props.value?.toString();

  return <ListboxInput 
    id={id}
    value={value ?? ''}
    onChange={onChange}
    disabled={disabled}
    style={{ 
      backgroundColor: disabled 
        ? COLORS.jet.superlight_85 
        : COLORS.white.regular_100,
      cursor: disabled
        ? undefined
        : 'pointer'
    }}
  >
    <ListboxButton className={disabled ? 'disabled' : undefined} arrow={<Icon icon='chevron-down'/>}>
      <SelectedContent>{getDisplayedValue(value, children)}</SelectedContent>
    </ListboxButton>
    <ListboxPopover>
      {children}
    </ListboxPopover>
  </ListboxInput>

  function getDisplayedValue(value: string | undefined, children: OptionElement[] | undefined): ReactNode {
    if(!children || !value)
      return
    
    const selectedOption = children.find(c => c.props.value === value);
    return !!selectedOption
      ? selectedOption.props.children
      : undefined
  }
}

const SelectedContent = styled.span`
  flex: auto;
  overflow-x: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
`