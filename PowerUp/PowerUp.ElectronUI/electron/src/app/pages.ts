import { ReactNode } from "react"
import { AppContext } from "./appContext";
import { loadHomePage } from "./home/homePage";
import { loadPlayerEditorPage } from "./playerEditor/playerEditorPage";
import { loadRosterEditorPage } from "./rosterEditor/rosterEditorPage";
import { cleanupTeamEditorPage, loadTeamEditorPage } from "./teamEditor/teamEditorPage";
import { loadTestPage } from "./testPage/testPage";
import { loadDraftPage } from "./draftPage/draftPage";

export type PageLoadDefinition =
| { page: 'HomePage' }
| { page: 'RosterEditorPage', rosterId: number }
| { page: 'PlayerEditorPage', playerId: number }
| { page: 'TeamEditorPage', teamId: number, tempTeamId?: number }
| { page: 'TestPage' }
| { page: 'DraftPage', rosterId: number }

export interface PageDefinition {
  title: string;
  renderPage: PageRenderCallback;
  updatedPageLoadDef?: PageLoadDefinition;
}

export type PageLoadFunction = (appContext: AppContext, pageData: PageLoadDefinition) => Promise<PageDefinition>;
export type PageCleanupFunction = (appContext: AppContext, pageData: PageLoadDefinition) => Promise<void>;
export interface PageRegistryEntry {
  load: PageLoadFunction;
  cleanup?: PageCleanupFunction;
}

export type PageRenderCallback = (appContext: AppContext) => ReactNode;

export const pageRegistry: { [page in PageLoadDefinition['page']]: PageRegistryEntry } = {
  HomePage: { load: loadHomePage },
  RosterEditorPage: { load: loadRosterEditorPage },
  PlayerEditorPage: { load: loadPlayerEditorPage },
  TeamEditorPage: { load: loadTeamEditorPage, cleanup: cleanupTeamEditorPage },
  TestPage: { load: loadTestPage },
  DraftPage: { load: loadDraftPage }
}