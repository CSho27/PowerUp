import { useRef, useState } from "react";
import { Button } from "../../components/button/button";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { Modal } from "../../components/modal/modal";
import { TextField } from "../../components/textField/textField";
import { AppContext } from "../app";
import { ImportRosterApiClient } from "../rosterEditor/importRosterApiClient";

export interface ImportRosterModalProps {
  appContext: AppContext;
  importUrl: string;
  closeDialog: () => void;
}

interface State {
  rosterName: string | undefined;
  selectedFile: File | undefined;
}

export function ImportRosterModal(props: ImportRosterModalProps) {
  const { appContext, importUrl, closeDialog } = props;

  const importApiClientRef = useRef(new ImportRosterApiClient(importUrl, appContext.performWithSpinner));

  const [state, setState] = useState<State>({
    rosterName: undefined,
    selectedFile: undefined
  });

  const isReadyToSubmit = state.rosterName
    && state.rosterName.length > 0
    && !!state.selectedFile

  return <Modal ariaLabel='Import Roster'>
    <div style={{ paddingBottom: '16px' }}>
      <FieldLabel htmlFor='importSource'>Roster Name</FieldLabel>
      <TextField 
        id='importSource'
        value={state.rosterName}
        placeholder='Enter name for roster'
        onChange={name => setState(p => ({...p, rosterName: name}))}
      />
    </div>
    <div style={{ paddingBottom: '16px' }}>
      <FieldLabel htmlFor='gameSaveFileSelector'>File</FieldLabel>
      <input 
        id='gameSaveFileSelector'
        type='file' 
        accept='.dat' 
        onChange={e => setState(p => ({...p, selectedFile: e.target.files![0]}))} 
      />
    </div>
    <div style={{ display: 'flex', gap: '4px' }}>
      <Button variant='Outline' size='Small' onClick={closeDialog}>Cancel</Button>
      <Button variant='Fill' size='Small' disabled={!isReadyToSubmit} onClick={importRoster}>Open</Button>
    </div>
  </Modal>

  async function importRoster() {
    const response = await importApiClientRef.current.execute({ file: state.selectedFile!, importSource: state.rosterName! })
    appContext.setPage({ page: 'RosterEditorPage', rosterId: response.rosterId });
  }
}