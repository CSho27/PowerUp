import { useRef, useState } from "react";
import { Button } from "../../components/button/button";
import { FileSystemSelector } from "../../components/fileSystemSelector/fileSystemSelector";
import { Modal } from "../../components/modal/modal";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { GetGameSaveManagerDirectoryApiClient } from "./getGameSaveManagerDirectoryApiClient";
import { InitializeGameSaveManagerApiClient } from "./initializeGameSaveManagerApiClient";
import { AppContext } from "../../app/appContext";

export async function openGameSaveManagerInitializationModal(appContext: AppContext): Promise<boolean> {
  const apiClient = new GetGameSaveManagerDirectoryApiClient(appContext.commandFetcher);  
  const response = await apiClient.execute();
  return new Promise(resolve => {
    appContext.openModal(closeDialog => <GameSaveManagerInitializationModal
      appContext={appContext}
      initialGameSaveManagerDirectory={response.gameSaveManagerDirectoryPath ?? undefined}
      closeDialog={isInitialized => {
        closeDialog();
        resolve(isInitialized);
      }}
    />);
  })
}

interface GameSaveManagerInitializationModalProps {
  appContext: AppContext;
  initialGameSaveManagerDirectory: string | undefined
  closeDialog: (isInitialized: boolean) => void;
}

function GameSaveManagerInitializationModal(props: GameSaveManagerInitializationModalProps) {
  const { appContext, initialGameSaveManagerDirectory, closeDialog } = props;
  const initializationApiClientRef = useRef(new InitializeGameSaveManagerApiClient(appContext.commandFetcher));
  const [selectedDirectory, setSelectedDirectory] = useState<string | undefined>(initialGameSaveManagerDirectory);

  return <Modal ariaLabel='Export Roster'>
    <div style={{ paddingBottom: '16px' }}>
      <FieldLabel htmlFor='gameSaveManagerFolder'>Game Save Management Folder</FieldLabel>
      <div>Select the Dolphin PowerPros data folder if you plan to use Dolphin</div>
      <FileSystemSelector
        appContext={appContext}
        id='gameSaveManagerFolder'
        type='Directory'
        selectedPath={selectedDirectory}
        onSelection={setSelectedDirectory}
      />
    </div>
    <div style={{ display: 'flex', gap: '4px' }}>
      <Button variant='Outline' size='Small' onClick={() => closeDialog(false)}>Cancel</Button>
      <Button variant='Fill' size='Small' disabled={!selectedDirectory} onClick={exportRoster}>Select Folder</Button>
    </div>
  </Modal>

  async function exportRoster() {
    const response = await initializationApiClientRef.current.execute({
      gameSaveManagerDirectoryPath: selectedDirectory
    });
    closeDialog(response.success);
  }
}