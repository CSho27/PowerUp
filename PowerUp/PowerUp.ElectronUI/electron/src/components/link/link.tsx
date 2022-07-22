import styled from "styled-components";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { Icon, IconType } from "../icon/icon";

export interface LinkProps extends React.DetailedHTMLProps<React.AnchorHTMLAttributes<HTMLAnchorElement>, HTMLAnchorElement> {
  icon?: IconType;
}

export function Link(props: LinkProps) {
 const { ref, ...propsWithoutRef } = props;

  return <LinkWrapper {...propsWithoutRef}>
    {props.icon && <Icon icon={props.icon} />}
    <LinkText>{props.children}</LinkText>
  </LinkWrapper>
}

const LinkWrapper = styled.a`
  display: flex;
  align-items: center;
  gap: 4px;
  color: ${COLORS.primaryBlue.regular_45};
  font-size: ${FONT_SIZES._18};

  &:hover {
    cursor: pointer;
  }
`

const LinkText = styled.span`
  font-weight: bold;
  white-space: nowrap;

  &:hover {
    text-decoration: underline;
  }
`
