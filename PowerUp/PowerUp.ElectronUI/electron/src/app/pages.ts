import { ReactNode } from "react"
import { AppContext } from "./app";
import { loadHomePage } from "./home/homePage";
import { loadPlayerEditorPage } from "./playerEditor/playerEditorPage";
import { RosterEditorResponse } from "./rosterEditor/rosterEditorDTOs";
import { loadRosterEditorPage } from "./rosterEditor/rosterEditorPage";

export type PageLoadDefinition =
| { page: 'HomePage', importUrl: string }
| { page: 'RosterEditorPage', rosterLoadDef: RosterLoadDefinition }
| { page: 'PlayerEditorPage', playerId: number }

export type RosterLoadDefinition =
| { type: 'Base' }
| { type: 'Existing', rosterId: number }
| { type: 'Import', importUrl: string, selectedFile: File, importSource: string }

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
}
