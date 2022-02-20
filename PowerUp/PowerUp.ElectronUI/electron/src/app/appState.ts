import React from "react";

export interface AppState {
  currentPage: React.ReactNode;
}

export type AppStateAction =
| { type: 'updatePage', newPage: React.ReactNode }

export function AppStateReducer(state: AppState, action: AppStateAction): AppState {
  switch(action.type) {
    case 'updatePage':
      return {
        ...state,
        currentPage: action.newPage
      }
  }
}

