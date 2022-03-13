import React, { ReactElement, ReactNode } from "react";
import { ModalProps } from "../components/modal/modal";

export interface AppState {
  currentPage: React.ReactNode;
  modals: ModalDefinition[];
  isLoading: boolean;
}

export interface ModalDefinition {
  key: string;
  modal: ReactElement<ModalProps>;
}

export type AppStateAction =
| { type: 'updatePage', newPage: ReactNode }
| { type: 'openModal', modal: ModalDefinition }
| { type: 'closeModal', modalKey: string }
| { type: 'updateIsLoading', isLoading: boolean }

export function AppStateReducer(state: AppState, action: AppStateAction): AppState {
  switch(action.type) {
    case 'updatePage':
      return {
        ...state,
        currentPage: action.newPage,
        modals: []
      }
    case 'openModal':
      return {
        ...state,
        modals: [...state.modals, action.modal]
      }
    case 'closeModal':
      return {
        ...state,
        modals: state.modals.filter(m => m.key !== action.modalKey)
      }
    case 'updateIsLoading':
      return {
        ...state,
        isLoading: action.isLoading
      }
  }
}

