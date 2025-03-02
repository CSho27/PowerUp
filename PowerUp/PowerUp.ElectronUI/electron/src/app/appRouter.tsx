import { BrowserRouter, createBrowserRouter, createRoutesFromElements, Outlet, Params, Route, RouterProvider, Routes, useNavigate, useNavigation, useParams } from "react-router-dom";
import { useAppContext } from "./appContext";
import { HomePage, loadHomePageProps } from "./home/homePage";
import { PropsWithChildren, ReactNode } from "react";
import { useQuery } from "../components/hooks/useQuery";
import { loadRosterEditorPageProps, RosterEditorPage } from "./rosterEditor/rosterEditorPage";
import { PageLoadDefinition, PageProps, PagePropsLoadFunction } from "./pages";
import { loadPlayerEditorPageProps, PlayerEditorPage } from "./playerEditor/playerEditorPage";
import { loadTeamEditorPageProps, TeamEditorPage } from "./teamEditor/teamEditorPage";
import { DraftPage, loadDraftPageProps } from "./draftPage/draftPage";

export interface AppRouterProps {
  renderPage: (page: ReactNode) => ReactNode;
}

export function AppRouter({ renderPage }: AppRouterProps) {
  return <BrowserRouter>
    <Routes>
      <Route path='' element={<>{renderPage(<Outlet />)}</>}>
        <Route 
          index
          element={<PageLoader 
            pageDef={() => ({ page: 'HomePage' })} 
            loadProps={loadHomePageProps} 
            renderPage={props => <HomePage {...props} />}
          />} 
        />
        <Route 
          path='roster/:rosterId' 
          element={<PageLoader 
            pageDef={p => ({ page: 'RosterEditorPage', rosterId: Number.parseInt(p.rosterId ?? '') })} 
            loadProps={loadRosterEditorPageProps} 
            renderPage={props => <RosterEditorPage {...props} />}
          />} 
        />
        <Route 
          path='team/:teamId' 
          element={<PageLoader 
            pageDef={p => ({ 
              page: 'TeamEditorPage', 
              teamId: Number.parseInt(p.teamId ?? ''), 
              tempTeamId: p.tempTeamId
                ? Number.parseInt(p.tempTeamId)
                : undefined  
            })} 
            loadProps={loadTeamEditorPageProps} 
            renderPage={props => <TeamEditorPage {...props} />}
          />} 
        />
        <Route 
          path='player/:playerId' 
          element={<PageLoader 
            pageDef={p => ({ page: 'PlayerEditorPage', playerId: Number.parseInt(p.playerId ?? '') })} 
            loadProps={loadPlayerEditorPageProps} 
            renderPage={props => <PlayerEditorPage {...props} />}
          />}
        />
        <Route 
          path='draft/:rosterId' 
          element={<PageLoader 
            pageDef={p => ({ page: 'DraftPage', rosterId: Number.parseInt(p.rosterId ?? '') })} 
            loadProps={loadDraftPageProps} 
            renderPage={props => <DraftPage {...props} />}
          />}
        />
      </Route>
    </Routes>
  </BrowserRouter>
}

interface PageLoaderProps<TPageProps> {
  pageDef: (params: Readonly<Params<string>>) => PageLoadDefinition;
  loadProps: PagePropsLoadFunction<TPageProps>
  renderPage: (props: TPageProps) => ReactNode;
}

function PageLoader<TPageProps>({ pageDef, loadProps, renderPage }: PageLoaderProps<TPageProps>) {
  const appContext = useAppContext();
  const params = useParams();
  const navigate = useNavigate()
  const { data } = useQuery({ 
    queryFn: () => {
      const props = loadProps(appContext, pageDef(params))
      // TODO: figure out what to do with updatedPageDef
      return props;
    } 
  }, []);

  return <>
    {data && renderPage({ ...data, appContext: appContext } as TPageProps)}
  </>
}