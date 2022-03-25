import { useRef, useState } from "react";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { Modal } from "../../components/modal/modal";
import { AppContext } from "../app";
import { DirectorySelectionApiClient } from "./directorySelectionApiClient";
import { ExportRosterApiClient } from "./exportRosterApiClient";

export interface RosterExportModalProps {
  appContext: AppContext;
  rosterId: number;
  closeDialog: () => void;
}

export function RosterExportModal(props: RosterExportModalProps) {
  const { appContext, rosterId, closeDialog } = props;
  const directorySelectionApiClientRef = useRef(new DirectorySelectionApiClient())//new FileSystemSelectionApiClient(appContext.commandFetcher));
  const exportApiClientRef = useRef(new ExportRosterApiClient(appContext.commandFetcher));
  const [selectedDirectory, setSelectedDirectory] = useState<string|undefined>(undefined);

  return <Modal ariaLabel='Export Roster'>
    <div style={{ paddingBottom: '16px' }}>
      <FieldLabel htmlFor='gameSaveExportLocationSelector'>Export Location</FieldLabel>
      <div style={{ display: 'flex', gap: '16px', alignItems: 'baseline' }}>
        <div style={{ flex: '0 0 auto' }}>
          <Button 
            id='gameSaveExportLocationSelector'
            size='Small'
            variant='Outline'
            onClick={selectDirectory}
            >
            Choose Directory
          </Button>
        </div>
        <DirectoryDisplay style={{ flex: '0 1 auto'}}>{selectedDirectory}</DirectoryDisplay>
      </div>
    </div>
    <div style={{ display: 'flex', gap: '4px' }}>
      <Button variant='Outline' size='Small' onClick={closeDialog}>Cancel</Button>
      <Button variant='Fill' size='Small' disabled={!selectedDirectory} onClick={exportRoster}>Export</Button>
    </div>
  </Modal>

  async function selectDirectory() {
    const response = await directorySelectionApiClientRef.current.execute({ selectionType: 'Directory' });
    setSelectedDirectory(response.path ?? undefined);
  }

  async function exportRoster() {
    const response = await exportApiClientRef.current.execute({
      rosterId: rosterId,
      directoryPath: selectedDirectory!
    });

    if(response.success)
      closeDialog();
  }
}

const DirectoryDisplay = styled.span`
    text-overflow: ellipsis;
    white-space: nowrap;
    overflow: hidden;
`