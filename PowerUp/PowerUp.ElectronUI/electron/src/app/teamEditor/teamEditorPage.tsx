import { AppContext } from "../app";
import { PageLoadDefinition, PageLoadFunction } from "../pages";
import { LoadTeamEditorApiClient } from "./loadTeamEditorApiClient";

interface TeamEditorPageProps {

}

function TeamEditorPage(props: TeamEditorPageProps) {
  return <div>
    TEAM
  </div>
}

export const loadTeamEditorPage: PageLoadFunction = async (appContext: AppContext, pageDef: PageLoadDefinition) => {
  if(pageDef.page !== 'TeamEditorPage') throw '';
  
  const apiClient = new LoadTeamEditorApiClient(appContext.commandFetcher);
  const response = await apiClient.execute({ teamId: pageDef.teamId });

  return {
    title: `Edit Team`,
    renderPage: (appContext) => <TeamEditorPage />
  }
}