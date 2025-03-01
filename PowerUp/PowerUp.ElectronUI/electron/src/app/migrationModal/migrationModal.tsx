import { useRef, useState } from "react";
import { Button } from "../../components/button/button";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { FileSystemSelector } from "../../components/fileSystemSelector/fileSystemSelector";
import { Modal } from "../../components/modal/modal";
import { AppContext } from "../appContext";
import { MigrateExistingDatabaseApiClient } from "./migrateExistingDatabaseApiClient";

export async function openMigrationModal(appContext: AppContext) {
  appContext.openModal(closeDialog => <MigrationModal 
    appContext={appContext}
    closeDialog={closeDialog}
  />);
}


export interface MigrationModalProps {
  appContext: AppContext;
  closeDialog: () => void;
}

export function MigrationModal(props: MigrationModalProps) {
  const { appContext, closeDialog } = props;
  const migrationApiClientRef = useRef(new MigrateExistingDatabaseApiClient(appContext.commandFetcher));
  const [selectedFile, setSelectedFile] = useState<string|undefined>(undefined);

  return <Modal ariaLabel='Import Old Data'>
    <div style={{ paddingBottom: '16px' }}>
      <div>Select another version of the PowerUp app to import its data.</div>
      <FieldLabel htmlFor='powerup-file-selector'>PowerUp Path to import</FieldLabel>
      <FileSystemSelector
        appContext={appContext}
        id='powerup-file-selector'
        type='File'
        selectedPath={selectedFile}
        onSelection={setSelectedFile}
        />
    </div>
    <div style={{ display: 'flex', gap: '4px' }}>
      <Button variant='Outline' size='Small' onClick={closeDialog}>Cancel</Button>
      <Button variant='Fill' size='Small' disabled={!selectedFile} onClick={importData}>Import</Button>
    </div>
  </Modal>

  async function importData() {
    const response = await migrationApiClientRef.current.execute({ powerUpDirectory: selectedFile! });
    if(response.success)
      closeDialog();
    else
      await appContext.commandFetcher.log('Error', 'Failed to migrate data');
  }
}