//import { shell } from "electron";

export function openInBroswer(url: string): void {
  window.open(url, '_blank');
  //shell.openExternal(url);
}

export function openInBrowserOnClick(url: string): () => void {
  return () => openInBroswer(url);
}