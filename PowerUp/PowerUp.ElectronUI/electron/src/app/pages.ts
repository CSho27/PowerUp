import { ReactNode } from "react"
import { AppContext } from "./app";
import { loadHomePage } from "./home/homePage";
import { loadPlayerEditorPage } from "./playerEditor/playerEditorPage";
import { loadRosterEditorPage } from "./rosterEditor/rosterEditorPage";
import { loadTeamEditorPage } from "./teamEditor/teamEditorPage";

export type PageLoadDefinition =
| { page: 'HomePage' }
| { page: 'RosterEditorPage', rosterId: number }
| { page: 'PlayerEditorPage', playerId: number }
| { page: 'TeamEditorPage', teamId: number }

export interface PageDefinition {
  title: string;
  renderPage: PageRenderCallback;
}

export type PageLoadFunction = (appContext: AppContext, pageData: PageLoadDefinition) => Promise<PageDefinition>;
export type PageRenderCallback = (appContext: AppContext) => ReactNode;

export const pageRegistry: { [page in PageLoadDefinition['page']]: PageLoadFunction } = {
  HomePage: loadHomePage,
  RosterEditorPage: loadRosterEditorPage,
  PlayerEditorPage: loadPlayerEditorPage,
  TeamEditorPage: loadTeamEditorPage
}
