import { useState } from "react";
import { Button } from "../../components/button/button";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { Modal } from "../../components/modal/modal";
import { SelectField } from "../../components/SelectField/selectField";
import { toOptions, tryFromOptions } from "../../components/SelectField/selectFieldHelpers";
import { AppContext } from "../appContext";
import { SimpleCode } from "../shared/simpleCode";

export interface ExistingRostersModalProps {
  appContext: AppContext;
  options: SimpleCode[];
  okLabel: string;
  closeDialog: () => void;
  onRosterSelected: (rosterId: number) => void;
}

export function ExistingRostersModal(props: ExistingRostersModalProps) {
  const { appContext, options, okLabel, closeDialog, onRosterSelected } = props;
  const [selectedRoster, setSelectedRoster] = useState<SimpleCode | undefined>(undefined)

  return <Modal ariaLabel='Load Roster'>
    <div style={{ paddingBottom: '16px' }}>
      <FieldLabel htmlFor='rosterSelector'>Roster</FieldLabel>
      <SelectField id='rosterSelector' value={selectedRoster?.id} onChange={roster => setSelectedRoster(tryFromOptions(options, roster))}>
        {toOptions(options, true)}
      </SelectField>
    </div>
    <div style={{ display: 'flex', gap: '4px' }}>
      <Button variant='Outline' size='Small' onClick={closeDialog}>Cancel</Button>
      <Button variant='Fill' size='Small' disabled={!selectedRoster} onClick={() => onRosterSelected(selectedRoster!.id)}>{okLabel}</Button>
    </div>
  </Modal>
}