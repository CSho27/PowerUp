import { shell } from "electron";

export function openInBroswer(url: string): void {
  shell.openExternal(url);
}

export function openInBrowserOnClick(url: string): () => void {
  return () => openInBroswer(url);
}