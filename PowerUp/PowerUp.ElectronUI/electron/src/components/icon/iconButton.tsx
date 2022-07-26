import { PropsWithChildren } from "react";
import styled from "styled-components";
import { COLORS } from "../../style/constants";
import { Icon, IconProps } from "./icon";

export interface IconButtonProps extends IconProps {
  onClick: () => void;
  disabled?: boolean;
}

export function IconButton(props: PropsWithChildren<IconButtonProps>) {
  const { onClick, disabled, ...iconProps } = props;
  
  return <IconLink onClick={disabled ? undefined : onClick} disabled={!!disabled}>
    <Icon {...iconProps} />
    {props.children && <span>{props.children}</span>}
  </IconLink>
}

const IconLink = styled.a<{ disabled: boolean }>`
  color: ${p => p.disabled ? COLORS.primaryBlue.regular_45_t70 : COLORS.primaryBlue.regular_45};
  cursor: ${p => p.disabled ? 'default' : 'pointer'};

  &:hover {
    color: ${p => p.disabled ? '' : COLORS.primaryBlue.lighter_69};
  }
`