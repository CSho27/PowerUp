import { ReactElement } from "react";
import styled from "styled-components";
import { COLORS, FONT_SIZES } from "../../style/constants";

export interface BreadcrumbsProps {
  children: ReactElement<CrumbProps>[];
}


export function Breadcrumbs(props: BreadcrumbsProps) {
  return <nav aria-label="Breadcrumb">
    <BreadcrumbList>{props.children.map(toCrumb)}</BreadcrumbList>
  </nav>

  function toCrumb(child: ReactElement<CrumbProps>) {
    return <Wrapper key={child.key}>{child}</Wrapper>;
  }
}

const BreadcrumbList = styled.ol`
  padding: 0;
  margin: 0;
  list-style-type: none;
  color: ${COLORS.primaryBlue.regular_45};
  font-weight: bold;
  font-size: ${FONT_SIZES._18};
  font-style: italic;
`

const Wrapper = styled.li`
  display: inline;

  &:not(:first-of-type){
    margin-left: 6px;

    &:before {
      margin-right: 6px;
      font-weight: 900;
      content: '>';
    }
  }
`

export interface CrumbProps {
  children?: React.ReactNode;
  onClick?: () => void;
}

export function Crumb(props: CrumbProps) {
  return <CrumbLink onClick={props.onClick}>
    {props.children}
  </CrumbLink>;
}

const CrumbLink = styled.a`
  text-decoration: none;
 
  ${p => p.onClick
    ? `&:hover {
        text-decoration: underline;
        cursor: pointer;
      }`
    : undefined
  }
`