import { useMemo, useRef, useState } from "react";
import { Button } from "../../components/button/button";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { Modal } from "../../components/modal/modal";
import { TextField } from "../../components/textField/textField";
import { AppContext } from "../appContext";
import { ImportRosterApiClient, ImportRosterRequest } from "../rosterEditor/importRosterApiClient";
import styled from "styled-components";
import { RadioButton } from "../../components/radioButton/radioButton";

export interface ImportRosterModalProps {
  appContext: AppContext;
  closeDialog: () => void;
}

interface State {
  rosterName: string | undefined;
  importType: 'game-save' | 'csv'
  selectedFile: File | undefined;
}

export function ImportRosterModal(props: ImportRosterModalProps) {
  const { appContext, closeDialog } = props;

  const importApiClient = useMemo(() => new ImportRosterApiClient(
    appContext.commandFetcher,
    appContext.performWithSpinner
  ), []);

  const [state, setState] = useState<State>({
    rosterName: undefined,
    importType: 'game-save',
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
    <div>
      <FieldLabel htmlFor='importType'>Import Type</FieldLabel>
      <RadioButtons>
        <RadioButtonWrapper>
          <span>Game Save</span>
          <RadioButton 
            groupName='importType'
            value='game-save' 
            checked={state.importType === 'game-save'} 
            onSelect={() => setState(p => ({ ...p, importType: 'game-save' }))} 
          />
        </RadioButtonWrapper>
        <RadioButtonWrapper>
          <span>CSV</span>
          <RadioButton 
            groupName='importType'
            value='csv' 
            checked={state.importType === 'csv'} 
            onSelect={() => setState(p => ({ ...p, importType: 'csv' }))} 
          />
        </RadioButtonWrapper>
      </RadioButtons>
    </div>
    <div style={{ paddingBottom: '16px' }}>
      <FieldLabel htmlFor='gameSaveFileSelector'>File</FieldLabel>
      <input 
        id='gameSaveFileSelector'
        type='file' 
        accept={state.importType === 'game-save' ? '.dat' : '.csv'} 
        onChange={e => setState(p => ({...p, selectedFile: e.target.files![0]}))} 
      />
    </div>
    <div style={{ display: 'flex', gap: '4px' }}>
      <Button variant='Outline' size='Small' onClick={closeDialog}>Cancel</Button>
      <Button variant='Fill' size='Small' disabled={!isReadyToSubmit} onClick={importRoster}>Open</Button>
    </div>
  </Modal>

  async function importRoster() {
    const request: ImportRosterRequest = { importSource: state.rosterName! };
    const response = state.importType === 'game-save'
      ? await importApiClient.execute(request, state.selectedFile!)
      : await importApiClient.executeCsv(request, state.selectedFile!)
    appContext.setPage({ page: 'RosterEditorPage', rosterId: response.rosterId });
  }
}

const RadioButtons = styled.div`
  display: flex;
  align-items: center;
  gap: 1.5rem;
  padding-bottom: .75rem;
`

const RadioButtonWrapper = styled.div`
  display: flex;
  align-items: center;
  gap: .5rem;
`