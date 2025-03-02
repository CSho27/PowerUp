import { ReactElement } from "react";
import { ModalProps } from "../components/modal/modal";
import { GenerateId } from "../utils/generateId";
import { PageLoadDefinition } from "./pages";

export interface AppState {
  breadcrumbs: BreadcrumbDefinition[];
  currentPage: PageLoadDefinition;
  modals: ModalDefinition[];
  isLoading: boolean;
}

export interface ModalDefinition {
  key: string;
  modal: ReactElement<ModalProps>;
}

export interface BreadcrumbDefinition {
  id: number;
  title: string;
  pageLoadDef: PageLoadDefinition;
}

export type AppStateAction =
| { type: 'updatePage', pageLoadDef: PageLoadDefinition }
| { type: 'updatePageFromBreadcrumb', breadcrumbId: number, pageLoadDef: PageLoadDefinition }
| { type: 'openModal', modal: ModalDefinition }
| { type: 'closeModal', modalKey: string }
| { type: 'updateIsLoading', isLoading: boolean }

export function AppStateReducer(state: AppState, action: AppStateAction): AppState {
  switch(action.type) {
    case 'updatePage':
      return {
        ...state,
        breadcrumbs: [...state.breadcrumbs, { id: GenerateId(), title: action.pageLoadDef.page, pageLoadDef: action.pageLoadDef }],
        currentPage: action.pageLoadDef,
        modals: []
      }
    case 'updatePageFromBreadcrumb':
      const targetPageIndex = state.breadcrumbs.findIndex(c => c.id === action.breadcrumbId);
      return {
        ...state,
        breadcrumbs: state.breadcrumbs.slice(0, targetPageIndex + 1),
        currentPage: action.pageLoadDef,
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

