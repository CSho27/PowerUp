import { ReactElement } from "react";
import styled from "styled-components";
import { AppContext } from "../../app/app";
import { BreadcrumbDefinition } from "../../app/appState";
import { COLORS, FONT_SIZES } from "../../style/constants";

export interface BreadcrumbsProps {
  appContext: AppContext;
}

export function Breadcrumbs(props: BreadcrumbsProps) {
  const { breadcrumbs, popBreadcrumb } = props.appContext
  
  return <nav aria-label="Breadcrumb">
    <BreadcrumbList>{breadcrumbs.map(toCrumb)}</BreadcrumbList>
  </nav>

  function toCrumb(breadcrumb: BreadcrumbDefinition, index: number) {
    const isCurrentPage = index == breadcrumbs.length-1;
    return <Wrapper key={breadcrumb.id}>
      <CrumbLink
        isCurrentPage={isCurrentPage}
        onClick={!isCurrentPage
          ? () => popBreadcrumb(breadcrumb.id) 
          : undefined}
      >{breadcrumb.title}</CrumbLink>
    </Wrapper>;
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
  white-space: nowrap;
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

const CrumbLink = styled.a<{ isCurrentPage: boolean }>`
  text-decoration: none;

  ${p => !p.isCurrentPage && `
    &:hover {
      text-decoration: underline;
      cursor: pointer;
    }
  `}  
`