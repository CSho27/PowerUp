import styled from "styled-components";

export const MaxWidthWrapper = styled.div<{ maxWidth: string }>`
  max-width: ${p => p.maxWidth};
  padding-left: 32px;
  padding-right: 32px;
  margin-left: auto;
  margin-right: auto;
`