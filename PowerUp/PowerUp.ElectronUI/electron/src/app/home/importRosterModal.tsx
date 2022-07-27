import { useRef, useState } from "react";
import { Button } from "../../components/button/button";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { Modal } from "../../components/modal/modal";
import { RadioButton } from "../../components/radioButton/radioButton";
import { TextField } from "../../components/textField/textField";
import { AppContext } from "../app";
import { GameSaveFormat, ImportRosterApiClient } from "../rosterEditor/importRosterApiClient";

export interface ImportRosterModalProps {
  appContext: AppContext;
  closeDialog: () => void;
}

interface State {
  rosterName: string | undefined;
  selectedFile: File | undefined;
  gameSaveFormat: GameSaveFormat;
}

export function ImportRosterModal(props: ImportRosterModalProps) {
  const { appContext, closeDialog } = props;

  const importApiClientRef = useRef(new ImportRosterApiClient(appContext.performWithSpinner));

  const [state, setState] = useState<State>({
    rosterName: undefined,
    selectedFile: undefined,
    gameSaveFormat: 'Wii'
  });

  const isReadyToSubmit = state.rosterName
    && state.rosterName.length > 0
    && !!state.selectedFile

  return <Modal ariaLabel='Import Roster'>
    <div style={{ paddingBottom: '16px' }}>
      <FieldLabel htmlFor='gameSaveFormat'>GameSave Format</FieldLabel>
      <div style={{ display: 'flex', gap: '16px' }}>
        <div>
          <FieldLabel htmlFor='gameSaveFormat_wii'>Wii</FieldLabel>
          <RadioButton 
            groupName='gameSaveFormat_wii' 
            value='Wii'
            checked={state.gameSaveFormat === 'Wii'} 
            onSelect={() => setState(p => ({ ...p, gameSaveFormat: 'Wii' }))}
          />
        </div>
        <div>
          <FieldLabel htmlFor='gameSaveFormat_Ps2'>Ps2</FieldLabel>
          <RadioButton 
            groupName='gameSaveFormat_Ps2'
            value='Ps2'
            checked={state.gameSaveFormat === 'Ps2'}
            onSelect={() => setState(p => ({ ...p, gameSaveFormat: 'Ps2' }))}
          />
        </div>
      </div>
    </div>
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
        accept={state.gameSaveFormat === 'Wii'
          ? '.dat'
          : ''}
        onChange={e => setState(p => ({...p, selectedFile: e.target.files![0]}))} 
      />
    </div>
    <div style={{ display: 'flex', gap: '4px' }}>
      <Button variant='Outline' size='Small' onClick={closeDialog}>Cancel</Button>
      <Button variant='Fill' size='Small' disabled={!isReadyToSubmit} onClick={importRoster}>Open</Button>
    </div>
  </Modal>

  async function importRoster() {
    const response = await importApiClientRef.current.execute({ 
      file: state.selectedFile!, 
      importSource: state.rosterName!,
      gameSaveFormat: state.gameSaveFormat
    })
    appContext.setPage({ page: 'RosterEditorPage', rosterId: response.rosterId });
  }
}