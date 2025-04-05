import {
  createBrowserRouter,
  createRoutesFromChildren,
  Outlet,
  Params,
  Route,
  RouterProvider,
  useNavigate,
  useParams,
} from 'react-router-dom';
import { useAppContext } from './appContext';
import { HomePage } from './home/homePage';
import { ReactNode } from 'react';
import { useQuery } from '../components/hooks/useQuery';
import { RosterEditorPage } from './rosterEditor/rosterEditorPage';
import { PageLoadDefinition, PagePropsLoadFunction } from './pages';
import {
  loadPlayerEditorPageProps,
  PlayerEditorPage,
} from './playerEditor/playerEditorPage';
import {
  loadTeamEditorPageProps,
  TeamEditorPage,
} from './teamEditor/teamEditorPage';
import { DraftPage, loadDraftPageProps } from './draftPage/draftPage';
import { Page } from './app';

const router = createBrowserRouter(
  createRoutesFromChildren(
    <>
      <Route
        path=''
        element={
          <Page>
            <Outlet />
          </Page>
        }
      >
        <Route index element={<HomePage />} />
        <Route path='/roster/:rosterId' element={<RosterEditorPage />} />
        <Route
          path='/team/:teamId'
          element={
            <PageLoader
              pageDef={p => ({
                page: 'TeamEditorPage',
                teamId: Number.parseInt(p.teamId ?? ''),
                tempTeamId: p.tempTeamId
                  ? Number.parseInt(p.tempTeamId)
                  : undefined,
              })}
              loadProps={loadTeamEditorPageProps}
              renderPage={props => <TeamEditorPage {...props} />}
            />
          }
        />
        <Route
          path='/player/:playerId'
          element={
            <PageLoader
              pageDef={p => ({
                page: 'PlayerEditorPage',
                playerId: Number.parseInt(p.playerId ?? ''),
              })}
              loadProps={loadPlayerEditorPageProps}
              renderPage={props => <PlayerEditorPage {...props} />}
            />
          }
        />
        <Route
          path='/draft/:rosterId'
          element={
            <PageLoader
              pageDef={p => ({
                page: 'DraftPage',
                rosterId: Number.parseInt(p.rosterId ?? ''),
              })}
              loadProps={loadDraftPageProps}
              renderPage={props => <DraftPage {...props} />}
            />
          }
        />
      </Route>
    </>
  )
);

export function AppRouter() {
  return <RouterProvider router={router} />;
}

interface PageLoaderProps<TPageProps> {
  pageDef: (params: Readonly<Params<string>>) => PageLoadDefinition;
  loadProps: PagePropsLoadFunction<TPageProps>;
  renderPage: (props: TPageProps) => ReactNode;
}

function PageLoader<TPageProps>({
  pageDef,
  loadProps,
  renderPage,
}: PageLoaderProps<TPageProps>) {
  const appContext = useAppContext();
  const params = useParams();
  const navigate = useNavigate();
  const { data } = useQuery(
    {
      queryFn: () => {
        const props = loadProps(appContext, pageDef(params));
        // TODO: figure out what to do with updatedPageDef
        return props;
      },
    },
    []
  );

  return (
    <>{data && renderPage({ ...data, appContext: appContext } as TPageProps)}</>
  );
}
