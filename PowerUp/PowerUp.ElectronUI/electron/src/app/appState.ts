
export type AppPage =
| 'Home'
| 'RosterEditor'
| 'TeamEditor'
| 'PlayerEditor';

export interface AppState {
  currentPage: AppPage;
}

export type AppStateAction =
| { type: 'updatePage', newPage: AppPage }

export function AppStateReducer(state: AppState, action: AppStateAction): AppState {
  switch(action.type) {
    case 'updatePage':
      return {
        ...state,
        currentPage: action.newPage
      }
  }
}

export const initialAppState: AppState = {
  currentPage: 'Home'
}

