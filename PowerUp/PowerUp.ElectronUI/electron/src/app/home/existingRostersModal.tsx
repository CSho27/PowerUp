import { useRef, useState } from "react";
import { Button } from "../../components/button/button";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { Modal } from "../../components/modal/modal";
import { SelectField } from "../../components/SelectField/selectField";
import { toOptions, tryToSimpleCode } from "../../components/SelectField/selectFieldHelpers";
import { AppContext } from "../app";
import { LoadExistingRosterApiClient } from "../rosterEditor/loadExistingRosterApiClient";
import { SimpleCode } from "../shared/simpleCode";

export interface ExistingRostersModalProps {
  appContext: AppContext;
  options: SimpleCode[];
  closeDialog: () => void;
}

export function ExistingRostersModal(props: ExistingRostersModalProps) {
  const { appContext, options, closeDialog } = props;
  const [selectedRoster, setSelectedRoster] = useState<SimpleCode | undefined>(undefined)
  const apiClientRef = useRef(new LoadExistingRosterApiClient(appContext.commandFetcher));

  return <Modal ariaLabel='Load Roster'>
    <div style={{ paddingBottom: '16px' }}>
      <FieldLabel htmlFor='rosterSelector'>Roster</FieldLabel>
      <SelectField id='rosterSelector' value={selectedRoster?.id} onChange={roster => setSelectedRoster(tryToSimpleCode(options, roster))}>
        {toOptions(options, true)}
      </SelectField>
    </div>
    <div style={{ display: 'flex', gap: '4px' }}>
      <Button variant='Outline' size='Small' onClick={closeDialog}>Cancel</Button>
      <Button variant='Fill' size='Small' disabled={!selectedRoster} onClick={loadExisting}>Open</Button>
    </div>
  </Modal>

  async function loadExisting() {
    const response = await apiClientRef.current.execute({ rosterId: selectedRoster!.id });
    appContext.setPage({ page: 'RosterEditorPage', response: response }); 
  }
}