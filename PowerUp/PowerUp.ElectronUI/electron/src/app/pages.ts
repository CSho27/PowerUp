import { AppContext } from "./appContext";

export type PageLoadDefinition =
| { page: 'HomePage' }
| { page: 'RosterEditorPage', rosterId: number }
| { page: 'PlayerEditorPage', playerId: number }
| { page: 'TeamEditorPage', teamId: number, tempTeamId?: number }
| { page: 'DraftPage', rosterId: number }

export type PageCleanupFunction = (appContext: AppContext, pageData: PageLoadDefinition) => Promise<void>;
export interface PageRegistryEntry {
  cleanup?: PageCleanupFunction;
}

export type PageProps<TPageProps> = Omit<TPageProps, 'appContext'> & { title: string; updatedPageLoadDef?: PageLoadDefinition };
export type PagePropsLoadFunction<TPageProps> = (appContext: AppContext, pageData: PageLoadDefinition) => Promise<PageProps<TPageProps>>;