import { FileSelectionFn } from "../components/fileSelector/fileSelector";

export type OpenInNewTabFn = (url: string) => void;

export interface AppConfig {
  openFileSelector: FileSelectionFn;
  openInNewTab: OpenInNewTabFn;
}

export let openFileSelector: FileSelectionFn = () => new Promise(r => r(null));
export let openInNewTab: OpenInNewTabFn = () => {};
