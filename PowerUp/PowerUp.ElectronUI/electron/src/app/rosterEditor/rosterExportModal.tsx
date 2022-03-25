import { useRef, useState } from "react";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { Modal } from "../../components/modal/modal";
import { AppContext } from "../app";
import { FileSystemSelectionApiClient } from "../../components/fileSystemSelector/fileSystemSelectionApiClient";
import { ExportRosterApiClient } from "./exportRosterApiClient";
import { FileSystemSelector } from "../../components/fileSystemSelector/fileSystemSelector";
import { FlexRow } from "../../components/flexRow/flexRow";
import { CheckboxField } from "../../components/checkboxField/checkboxField";
import { FONT_SIZES } from "../../style/constants";

export interface RosterExportModalProps {
  appContext: AppContext;
  rosterId: number;
  closeDialog: () => void;
}

interface State {
  useBaseGameSave: boolean;
  selectedGameSaveFile: string | undefined; 
  selectedDirectory: string | undefined;
}

export function RosterExportModal(props: RosterExportModalProps) {
  const { appContext, rosterId, closeDialog } = props;
  
  const exportApiClientRef = useRef(new ExportRosterApiClient(appContext.commandFetcher));
  const [state, setState] = useState<State>({
    useBaseGameSave: true,
    selectedGameSaveFile: undefined,
    selectedDirectory: undefined
  });

  return <Modal ariaLabel='Export Roster'>
    <div style={{ paddingBottom: '16px' }}>
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
      <FileSystemSelector
        id='gameSaveToCopyFromSelector'
        type='File'
        selectedPath={state.useBaseGameSave ? undefined : state.selectedGameSaveFile}
        fileFilter={{ name: 'PowerPros Game Save', allowedExtensions: ['dat'] }}
        disabled={state.useBaseGameSave}
        onSelection={file => setState(p => ({ ...p, selectedGameSaveFile: file }))}
      />
    </div>
    <div style={{ paddingBottom: '16px' }}>
      <FieldLabel htmlFor='gameSaveExportLocationSelector'>Export Location</FieldLabel>
      <FileSystemSelector
        id='gameSaveExportLocationSelector'
        type='Directory'
        selectedPath={state.selectedDirectory}
        onSelection={dir => setState(p => ({ ...p, selectedDirectory: dir }))}
      />
    </div>
    <div style={{ display: 'flex', gap: '4px' }}>
      <Button variant='Outline' size='Small' onClick={closeDialog}>Cancel</Button>
      <Button variant='Fill' size='Small' disabled={!state.selectedDirectory || (state.useBaseGameSave && !state.selectedGameSaveFile)} onClick={exportRoster}>Export</Button>
    </div>
  </Modal>

  async function exportRoster() {
    const response = await exportApiClientRef.current.execute({
      rosterId: rosterId,
      directoryPath: state.selectedDirectory!
    });

    if(response.success)
      closeDialog();
  }
}