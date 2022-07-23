import { PropsWithChildren } from "react";
import styled from "styled-components";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { openInBrowserOnClick } from "../../utils/openInBroswer";
import { Icon, IconType } from "../icon/icon";

interface BaseLinkProps {
  icon?: IconType;
}

interface UrlLinkProps extends BaseLinkProps {
  url: string;
}

interface OnClickLinkProps extends BaseLinkProps {
  onClick: () => void;
}

export type LinkProps = UrlLinkProps | OnClickLinkProps;

export function Link(props: PropsWithChildren<LinkProps>) {
  const onClick = isUrlLinkProps(props)
    ? openInBrowserOnClick(props.url)
    : props.onClick; 

  return <LinkWrapper onClick={onClick}>
    {props.icon && <Icon icon={props.icon} />}
    <LinkText>{props.children}</LinkText>
  </LinkWrapper>
}

function isUrlLinkProps(props: LinkProps): props is UrlLinkProps {
  return !!(props as UrlLinkProps).url;
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
