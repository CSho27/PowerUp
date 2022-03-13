import { useRef, useState } from "react";
import { Button } from "../../components/button/button";
import { Modal } from "../../components/modal/modal";
import { SelectField } from "../../components/SelectField/selectField";
import { toOptions, toSimpleCode } from "../../components/SelectField/selectFieldHelpers";
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

  return <Modal>
    <SelectField value={selectedRoster?.id} onChange={roster => setSelectedRoster(toSimpleCode(options, roster))}>
      {toOptions(options, true)}
    </SelectField>
    <div style={{ display: 'flex', gap: '4px' }}>
      <Button variant='Outline' size='Small' onClick={closeDialog}>Cancel</Button>
      <Button variant='Fill' size='Small' onClick={() => loadExisting(selectedRoster!.id)}>Open</Button>
    </div>
  </Modal>

  async function loadExisting(rosterId: number) {
    const response = await apiClientRef.current.execute({ rosterId: rosterId });
    appContext.setPage({ page: 'RosterEditorPage', response: response }); 
  }
}