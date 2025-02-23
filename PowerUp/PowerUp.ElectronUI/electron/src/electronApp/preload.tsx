import { contextBridge, ipcRenderer } from 'electron'
import { ElectronApi } from './renderer';

const electronApi: ElectronApi = {
  openFileSelector: filter => ipcRenderer.invoke('openFileSelector', filter),
  openInNewTab: url => ipcRenderer.invoke('openInNewTab', url),
}
contextBridge.exposeInMainWorld('electronApi', electronApi);

