import { useRef, useState } from "react";
import { Button } from "../../components/button/button";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { Modal } from "../../components/modal/modal";
import { AppContext } from "../app";
import { DirectorySelectionApiClient } from "./directorySelectionApiClient";

export interface RosterExportModalProps {
  appContext: AppContext;
  closeDialog: () => void;
}

export function RosterExportModal(props: RosterExportModalProps) {
  const { appContext, closeDialog } = props;
  const directorySelectionApiClientRef = useRef(new DirectorySelectionApiClient());
  const [selectedDirectory, setSelectedDirectory] = useState<string|undefined>(undefined);

  return <Modal ariaLabel='Export Roster'>
    <div style={{ paddingBottom: '16px' }}>
      <FieldLabel htmlFor='gameSaveExportLocationSelector'>Export Location</FieldLabel>
      <div style={{ display: 'flex', gap: '16px', alignItems: 'baseline' }}>
        <Button 
          id='gameSaveExportLocationSelector'
          size='Small'
          variant='Outline'
          onClick={selectDirectory}
          >
          Choose Directory
        </Button>
        <span>{selectedDirectory}</span>
      </div>
    </div>
    <div style={{ display: 'flex', gap: '4px' }}>
      <Button variant='Outline' size='Small' onClick={closeDialog}>Cancel</Button>
      <Button variant='Fill' size='Small' disabled={!selectedDirectory} onClick={() => {}}>Open</Button>
    </div>
  </Modal>

  async function selectDirectory() {
    const response = await directorySelectionApiClientRef.current.execute();
    setSelectedDirectory(response.directoryPath);
  }
}