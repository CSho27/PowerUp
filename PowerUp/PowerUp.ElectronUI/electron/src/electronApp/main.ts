import { app, BrowserWindow, dialog, ipcMain, OpenDialogOptions, session, shell } from 'electron';
import { basename } from 'path';
import { FileFilter } from '../components/fileSelector/fileSelector';
import { readFileSync } from 'fs';
import path from 'path';

// This allows TypeScript to pick up the magic constants that's auto-generated by Forge's Webpack
// plugin that tells the Electron app where to look for the Webpack-bundled app code (depending on
// whether you're running in development or production).
declare const MAIN_WINDOW_WEBPACK_ENTRY: string;
declare const MAIN_WINDOW_PRELOAD_WEBPACK_ENTRY: string;

const isDev = !app.isPackaged;
const configPath = isDev
  ? 'config.json'
  : path.join(process.resourcesPath, 'config.json');

// Read and parse config.json
const config = JSON.parse(readFileSync(configPath, 'utf8'));

if (require('electron-squirrel-startup')) app.quit();

app.whenReady().then(() => {
  createWindow();
});
app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') app.quit();
});
app.on('activate', () => {
  if (BrowserWindow.getAllWindows().length === 0) createWindow();
});

ipcMain.on('get-config', event => {
  event.returnValue = config;
})
ipcMain.handle('openFileSelector', async (_, filter) => await openFileSelector(filter));
ipcMain.handle('openInNewTab', async (_, url) => shell.openExternal(url));

async function createWindow() {
  session.defaultSession.webRequest.onHeadersReceived((details, callback) => {
    callback({
      responseHeaders: {
        ...details.responseHeaders,
        "Content-Security-Policy": csp
      }
    });
  });
  
  const win = new BrowserWindow({
    height: 600,
    width: 800,
    show: false,
    icon: './public/favicon.ico',
    webPreferences: {
      preload: MAIN_WINDOW_PRELOAD_WEBPACK_ENTRY
    }
  });
  win.maximize();
  win.show();
  win.loadURL(MAIN_WINDOW_WEBPACK_ENTRY);
}

async function openFileSelector(filter?: FileFilter): Promise<File|null> {
  const options: OpenDialogOptions = {
    properties: ['openFile'], 
    filters: filter
      ? [{ name: filter.name, extensions: filter.allowedExtensions }]
      : undefined
  }
  const result = await dialog.showOpenDialog(options);
  const filePath = result.filePaths[0];
  if(!filePath) return null;
  
  const buffer = readFileSync(filePath);
  return new File([buffer], basename(filePath));
}

const csp = `
  default-src 'self'; 
  script-src 'self' 'unsafe-inline' https://kit.fontawesome.com https://ka-f.fontawesome.com;
  style-src 'self' 'unsafe-inline' https://fonts.googleapis.com;
  font-src 'self' 'unsafe-inline' https://ka-f.fontawesome.com https://fonts.googleapis.com https://fonts.gstatic.com;
  img-src 'self' data: https://localhost:44388;
  connect-src 'self' https://localhost:44388 https://kit.fontawesome.com https://ka-f.fontawesome.com;
`