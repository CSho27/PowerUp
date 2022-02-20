import React from "react";

export interface AppState {
  currentPage: React.ReactNode;
  isLoading: boolean;
}

export type AppStateAction =
| { type: 'updatePage', newPage: React.ReactNode }
| { type: 'updateIsLoading', isLoading: boolean }

export function AppStateReducer(state: AppState, action: AppStateAction): AppState {
  switch(action.type) {
    case 'updatePage':
      return {
        ...state,
        currentPage: action.newPage
      }
    case 'updateIsLoading':
      return {
        ...state,
        isLoading: action.isLoading
      }
  }
}

