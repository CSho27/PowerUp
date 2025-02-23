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

ipcMain.handle('openFileSelector', async (_, filter) => await openFileSelector(filter));
ipcMain.handle('openInNewTab', async (_, url) => shell.openExternal(url));

async function createWindow() {
  const win = new BrowserWindow({
    height: 600,
    width: 800,
    webPreferences: {
      preload: path.resolve('dist/preload.js')
    }
  });
  win.loadFile('dist/renderer.html');  
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