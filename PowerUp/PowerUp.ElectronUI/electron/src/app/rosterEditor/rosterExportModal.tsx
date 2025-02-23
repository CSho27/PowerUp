import { useRef, useState } from "react";
import { Button } from "../../components/button/button";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { Modal } from "../../components/modal/modal";
import { AppContext } from "../appContext";
import { ExportRosterApiClient } from "./exportRosterApiClient";
import { FlexRow } from "../../components/flexRow/flexRow";
import { CheckboxField } from "../../components/checkboxField/checkboxField";
import { FONT_SIZES } from "../../style/constants";
import { RadioButton } from "../../components/radioButton/radioButton";
import styled from "styled-components";
import { downloadFile } from "../../utils/downloadFile";
import { FileSelectionInput } from "../shared/fileSelectionInput";

export async function openRosterExportModal(appContext: AppContext, rosterId: number) {
  appContext.openModal(closeDialog => <RosterExportModal
    appContext={appContext}
    rosterId={rosterId}
    closeDialog={closeDialog}
  />);
}

interface RosterExportModalProps {
  appContext: AppContext;
  rosterId: number;
  closeDialog: () => void;
}

interface State {
  exportType: ExportType;
  useBaseGameSave: boolean;
  selectedGameSaveFile: File | null; 
}

type ExportType = 'game-save' | 'csv';

function RosterExportModal(props: RosterExportModalProps) {
  const { appContext, rosterId, closeDialog } = props;
  
  const exportApiClientRef = useRef(
    new ExportRosterApiClient(
      appContext.commandFetcher, 
      appContext.performWithSpinner
    )
  );
  const [state, setState] = useState<State>({
    exportType: 'game-save',
    useBaseGameSave: true,
    selectedGameSaveFile: null,
  });

  return <Modal ariaLabel='Export Roster'>
    <div style={{ paddingBottom: '16px' }}>
      <div>
        <FieldLabel htmlFor='exportType'>Export Type</FieldLabel>
        <RadioButtons>
          <RadioButtonWrapper>
            <span>Game Save</span>
            <RadioButton 
              groupName='exportType'
              value='game-save' 
              checked={state.exportType === 'game-save'} 
              onSelect={() => setState(p => ({ ...p, exportType: 'game-save' }))} 
            />
          </RadioButtonWrapper>
          <RadioButtonWrapper>
            <span>CSV</span>
            <RadioButton 
              groupName='exportType'
              value='csv' 
              checked={state.exportType === 'csv'} 
              onSelect={() => setState(p => ({ ...p, exportType: 'csv' }))} 
            />
          </RadioButtonWrapper>
        </RadioButtons>
      </div>
      {state.exportType === 'game-save' && <>
      <FlexRow gap='16px' vAlignCenter>
        <FieldLabel htmlFor='gameSaveToCopyFromSelector'>Game Save to Copy From</FieldLabel>
        <FlexRow gap='4px' vAlignCenter style={{ flex: 'auto' }}>
          <CheckboxField 
            id='useBaseGameSave'
            checked={state.useBaseGameSave}
            size='Small'
            onToggle={() => setState(p => ({...p, useBaseGameSave: !state.useBaseGameSave }))}
            />
          <label htmlFor='useBaseGameSave' style={{ fontSize: FONT_SIZES._14 }}>Use Base Game Save</label>
        </FlexRow>
      </FlexRow>
      <FileSelectionInput
        id='gameSaveToCopyFromSelector'
        file={state.useBaseGameSave ? null : state.selectedGameSaveFile}
        fileFilter={{ name: 'PowerPros Game Save', allowedExtensions: ['dat'] }}
        disabled={state.useBaseGameSave}
        onSelection={file => setState(p => ({ ...p, selectedGameSaveFile: file }))}
        />
      </>}
    </div>
    <div style={{ display: 'flex', gap: '4px' }}>
      <Button variant='Outline' size='Small' onClick={closeDialog}>Cancel</Button>
      <Button variant='Fill' size='Small' disabled={!state.useBaseGameSave && !state.selectedGameSaveFile} onClick={exportRoster}>Export</Button>
    </div>
  </Modal>

  async function exportRoster() {
    if(state.exportType === 'game-save')
      exportRosterAsGameSave();
    else
      exportRosterAsCsv();
  }
  
  async function exportRosterAsGameSave() {
    const response = await exportApiClientRef.current.execute({
      rosterId: rosterId,
    }, state.selectedGameSaveFile);
    if(!response) return;

    downloadFile(response);
    closeDialog();
  }

  async function exportRosterAsCsv() {
    const csvFile = await exportApiClientRef.current.executeCsv(rosterId);
    downloadFile(csvFile);
    closeDialog();
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