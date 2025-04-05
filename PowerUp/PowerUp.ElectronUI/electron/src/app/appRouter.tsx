import {
  createBrowserRouter,
  createRoutesFromChildren,
  Outlet,
  Params,
  Route,
  RouterProvider,
  useParams,
} from 'react-router-dom';
import { HomePage } from './home/homePage';
import { ReactNode } from 'react';
import { useQuery } from '../components/hooks/useQuery';
import { RosterEditorPage } from './rosterEditor/rosterEditorPage';
import { PlayerEditorPage } from './playerEditor/playerEditorPage';
import { TeamEditorPage } from './teamEditor/teamEditorPage';
import { DraftPage } from './draftPage/draftPage';
import { Page } from './app';
import { AppContext, useAppContext } from './appContext';

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
        <Route path='/team/:teamId' element={<TeamEditorPage />} />
        <Route path='/player/:playerId' element={<PlayerEditorPage />} />
        <Route path='/draft/:rosterId' element={<DraftPage />} />
      </Route>
    </>
  )
);

export function AppRouter() {
  return <RouterProvider router={router} />;
}

export type RouteParams = Readonly<Params<string>>;
export interface PageLoaderProps<TPageProps> {
  loadProps: (
    appContext: AppContext,
    params: RouteParams
  ) => Promise<TPageProps>;
  renderPage: (props: TPageProps) => ReactNode;
}

export function PageLoader<TPageProps>({
  loadProps,
  renderPage,
}: PageLoaderProps<TPageProps>) {
  const appContext = useAppContext();
  const params = useParams();
  const { data } = useQuery(
    {
      queryFn: () => loadProps(appContext, params),
    },
    []
  );

  return <>{data && renderPage(data)}</>;
}
