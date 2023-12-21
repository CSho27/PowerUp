import { useMemo } from "react";
import { AppContext } from "../app";
import { DraftPoolApiClient } from "../shared/draftPoolApiClient";
import { PageLoadFunction } from "../pages";
import { useReducerWithContext } from "../../utils/reducerWithContext";
import { DraftStateReducer, getInitialState } from "./draftState";

interface DraftPageProps {
  appContext: AppContext;
  rosterId: number;
}

const playersDrafting = 2;

function DraftPage({ appContext, rosterId }: DraftPageProps) {
  const state = useReducerWithContext(
    DraftStateReducer, 
    getInitialState(playersDrafting), 
    { playersDrafting: playersDrafting }
  )
  const draftPoolApiClient = useMemo(() => new DraftPoolApiClient(appContext.commandFetcher), [appContext.commandFetcher]);

  return <div>Draft Page {rosterId}</div>
}

export const loadDraftPage: PageLoadFunction = async (_, pageDef) => {
  if(pageDef.page !== 'DraftPage') throw '';

  return {
    title: 'Draft Teams',
    renderPage: appContext => <DraftPage appContext={appContext} rosterId={pageDef.rosterId} />
  }
}