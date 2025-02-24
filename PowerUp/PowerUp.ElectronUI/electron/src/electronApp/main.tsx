import { app, BrowserWindow, dialog, ipcMain, OpenDialogOptions, shell } from 'electron';
import path, { basename } from 'path';
import { FileFilter } from '../components/fileSelector/fileSelector';
import { readFileSync } from 'fs';

app.whenReady().then(() => {
  createWindow();
});
app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') app.quit();
});
app.on('activate', () => {
  if (BrowserWindow.getAllWindows().length === 0) createWindow();
});

ipcMain.on('get-env', event => {
  event.returnValue = JSON.stringify(process.env)
})
ipcMain.handle('openFileSelector', async (_, filter) => await openFileSelector(filter));
ipcMain.handle('openInNewTab', async (_, url) => shell.openExternal(url));

async function createWindow() {
  const win = new BrowserWindow({
    height: 600,
    width: 800,
    show: false,
    icon: './public/favicon.ico',
    webPreferences: {
      preload: path.resolve('dist/renderer/preload.js')
    }
  });
  win.maximize();
  win.show();
  win.loadFile('dist/renderer/renderer.html');
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