import { useRef, useState } from "react";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { Modal } from "../../components/modal/modal";
import { AppContext } from "../app";
import { FileSystemSelectionApiClient } from "../../components/fileSystemSelector/fileSystemSelectionApiClient";
import { ExportRosterApiClient } from "./exportRosterApiClient";
import { FileSystemSelector } from "../../components/fileSystemSelector/fileSystemSelector";

export interface RosterExportModalProps {
  appContext: AppContext;
  rosterId: number;
  closeDialog: () => void;
}

export function RosterExportModal(props: RosterExportModalProps) {
  const { appContext, rosterId, closeDialog } = props;
  
  const exportApiClientRef = useRef(new ExportRosterApiClient(appContext.commandFetcher));
  const [selectedDirectory, setSelectedDirectory] = useState<string|undefined>(undefined);

  return <Modal ariaLabel='Export Roster'>
    <div style={{ paddingBottom: '16px' }}>
      <FieldLabel htmlFor='gameSaveExportLocationSelector'>Export Location</FieldLabel>
      <FileSystemSelector
        id='gameSaveExportLocationSelector'
        type='Directory'
        selectedPath={selectedDirectory}
        onSelection={setSelectedDirectory}
      />
    </div>
    <div style={{ display: 'flex', gap: '4px' }}>
      <Button variant='Outline' size='Small' onClick={closeDialog}>Cancel</Button>
      <Button variant='Fill' size='Small' disabled={!selectedDirectory} onClick={exportRoster}>Export</Button>
    </div>
  </Modal>

  async function exportRoster() {
    const response = await exportApiClientRef.current.execute({
      rosterId: rosterId,
      directoryPath: selectedDirectory!
    });

    if(response.success)
      closeDialog();
  }
}