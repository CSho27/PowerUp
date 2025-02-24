import { contextBridge, ipcRenderer } from 'electron'
import { ElectronApi, Env } from './renderer';

const env = ipcRenderer.sendSync('get-env');
const parsedEnv = JSON.parse(env) as Env;
const electronApi: ElectronApi = {
  openFileSelector: filter => ipcRenderer.invoke('openFileSelector', filter),
  openInNewTab: url => ipcRenderer.invoke('openInNewTab', url),
  env: {
    BASE_URL: parsedEnv.BASE_URL
  }
}
contextBridge.exposeInMainWorld('electronApi', electronApi);

