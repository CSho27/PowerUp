import { Route, Routes, useNavigate, useNavigation, useParams } from "react-router-dom";
import { useAppContext } from "./appContext";
import { HomePage, loadHomePageProps } from "./home/homePage";
import { ReactNode } from "react";
import { useQuery } from "../components/hooks/useQuery";
import { loadRosterEditorPageProps, RosterEditorPage } from "./rosterEditor/rosterEditorPage";
import { PageLoadDefinition, PageProps, PagePropsLoadFunction } from "./pages";
import { loadPlayerEditorPageProps, PlayerEditorPage } from "./playerEditor/playerEditorPage";
import { loadTeamEditorPageProps, TeamEditorPage } from "./teamEditor/teamEditorPage";
import { DraftPage, loadDraftPageProps } from "./draftPage/draftPage";

export function AppRoutes() {
  const params = useParams();
  return <Routes>
    <Route 
      index 
      element={<PageLoader 
        pageDef={{ page: 'HomePage' }} 
        loadProps={loadHomePageProps} 
        renderPage={HomePage}
      />} 
    />
    <Route 
      path='roster/:rosterId' 
      element={<PageLoader 
        pageDef={{ page: 'RosterEditorPage', rosterId: Number.parseInt(params.rosterId ?? '') }} 
        loadProps={loadRosterEditorPageProps} 
        renderPage={RosterEditorPage}
      />} 
    />
    <Route 
      path='team/:teamId' 
      element={<PageLoader 
        pageDef={{ 
          page: 'TeamEditorPage', 
          teamId: Number.parseInt(params.teamId ?? ''), 
          tempTeamId: params.tempTeamId
            ? Number.parseInt(params.tempTeamId)
            : undefined  
        }} 
        loadProps={loadTeamEditorPageProps} 
        renderPage={TeamEditorPage}
      />} 
    />
    <Route 
      path='player/:playerId' 
      element={<PageLoader 
        pageDef={{ page: 'PlayerEditorPage', playerId: Number.parseInt(params.playerId ?? '') }} 
        loadProps={loadPlayerEditorPageProps} 
        renderPage={PlayerEditorPage}
      />}
    />
    <Route 
      path='draft/:rosterId' 
      element={<PageLoader 
        pageDef={{ page: 'DraftPage', rosterId: Number.parseInt(params.rosterId ?? '') }} 
        loadProps={loadDraftPageProps} 
        renderPage={DraftPage}
      />}
    />
  </Routes>
}

interface PageLoaderProps<TPageProps> {
  pageDef: PageLoadDefinition;
  loadProps: PagePropsLoadFunction<TPageProps>
  renderPage: (props: TPageProps) => ReactNode;
}

function PageLoader<TPageProps>({ pageDef, loadProps, renderPage }: PageLoaderProps<TPageProps>) {
  const appContext = useAppContext();
  const { location } = useNavigation();
  const navigate = useNavigate()
  const { data } = useQuery({ 
    queryFn: () => {
      const props = loadProps(appContext, pageDef)
      // TODO: figure out what to do with updatedPageDef
      return props;
    } 
  }, []);

  return <>
    {data && renderPage({ ...data, appContext: appContext } as TPageProps)}
  </>
}