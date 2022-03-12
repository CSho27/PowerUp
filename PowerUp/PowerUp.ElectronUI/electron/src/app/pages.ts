import { ReactNode } from "react"
import { AppContext } from "./app";
import { loadHomePage } from "./home/homePage";
import { loadPlayerEditorPage } from "./playerEditor/playerEditorPage";
import { loadRosterEditorPage } from "./rosterEditor/rosterEditorPage";

export type PageLoadDefinition =
| { page: 'HomePage' }
| { page: 'RosterEditorPage', rosterId: number }
| { page: 'PlayerEditorPage', playerId: number }

export type PageLoadFunction = (appContext: AppContext, pageData: PageLoadDefinition) => Promise<ReactNode>;

export const pageRegistry: { [page in PageLoadDefinition['page']]: PageLoadFunction } = {
  HomePage: loadHomePage,
  RosterEditorPage: loadRosterEditorPage,
  PlayerEditorPage: loadPlayerEditorPage,
}
