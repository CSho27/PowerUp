import React, { RefObject } from "react";
import styled, { StyledComponentProps } from "styled-components";

export interface ContentWithHangingHeaderProps  {
  header: React.ReactNode;
  headerHeight: string;
  children?: React.ReactNode;
  contentRef?: RefObject<HTMLElement>;
}


export function ContentWithHangingHeader(props: ContentWithHangingHeaderProps) {
  return <Wrapper>
    <HangingHeader headerHeight={props.headerHeight}>
      {props.header}
    </HangingHeader>
    <ContentContainer ref={props.contentRef} headerHeight={props.headerHeight}>
      {props.children}
    </ContentContainer>
  </Wrapper>
}

const Wrapper = styled.div`
  height: 100%;
`

const HangingHeader = styled.section<{ headerHeight: string }>`
  padding: 4px 16px;
  box-shadow: 0px 6px 8px -8px;
  height: ${p => p.headerHeight};
`

const ContentContainer = styled.section<{ headerHeight: string}>`
  height: calc(100% - ${p => p.headerHeight});
  overflow-y: auto;
`