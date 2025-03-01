import { contextBridge, ipcRenderer } from 'electron'
import { ElectronApi, Config } from './renderer';

const config = ipcRenderer.sendSync('get-config') as Config;
const electronApi: ElectronApi = {
  openFileSelector: filter => ipcRenderer.invoke('openFileSelector', filter),
  openInNewTab: url => ipcRenderer.invoke('openInNewTab', url),
  config: {
    apiBaseUrl: config.apiBaseUrl
  }
}
contextBridge.exposeInMainWorld('electronApi', electronApi);

