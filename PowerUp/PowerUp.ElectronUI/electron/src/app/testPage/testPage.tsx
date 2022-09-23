import { useState } from "react";
import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow";
import { AppContext } from "../app";
import { PageLoadDefinition, PageLoadFunction } from "../pages"

interface TestPageProps {
  appContext: AppContext;
}

function TestPage(props: TestPageProps) {
  const [state, setState] = useState({
    month: 1,
    day: 1
  });
  
  return <FlexRow gap='4px'>
    <FlexFracItem frac='1/4'>
    </FlexFracItem>
  </FlexRow>
}

export const loadTestPage: PageLoadFunction = async (appContext: AppContext, pageDef: PageLoadDefinition) => {
  if(pageDef.page !== 'TestPage') throw '';
  
  return {
    title: 'TestPage',
    renderPage: appContext => <TestPage appContext={appContext} />,
    updatedPageLoadDef: { ...pageDef }
  }
}