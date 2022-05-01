import { useEffect, useRef, useState } from "react";
import { Button } from "../../components/button/button";
import { Modal } from "../../components/modal/modal";
import { TextField } from "../../components/textField/textField";
import { AppContext } from "../app";
import { EntitySourceType } from "../shared/entitySourceType";
import { PlayerSearchApiClient, PlayerSearchResultDto } from "./playerSearchApiClient";

export interface PlayerSelectionModalProps {
  appContext: AppContext;
  closeDialog: (selectedPlayerId: number | undefined) => void;
}

interface PlayerSelectionModalState {
  results: PlayerSearchResultDto[];
  searchText: string | undefined;
  selectedPlayerId: number | undefined;
}

export function PlayerSelectionModal(props: PlayerSelectionModalProps) {
  const { appContext, closeDialog } = props;

  const apiClientRef = useRef(new PlayerSearchApiClient(appContext.commandFetcher))

  const [state, setState] = useState<PlayerSelectionModalState>({
    results: [],
    searchText: undefined,
    selectedPlayerId: undefined
  });

  return <Modal ariaLabel='Select Player'>
    <TextField 
      value={state.searchText}
      onChange={text => setState(p => ({...p, searchText: text}))}
    />
    <Button 
      variant='Fill'
      size='Medium'
      onClick={search}
      disabled={!state.searchText || state.searchText.length === 0}>
      Search
    </Button>
    {state.results.map(r => <p key={r.playerId}>{r.name}</p>)}
  </Modal>

  async function search() {
    const response = await apiClientRef.current.execute({ searchText: state.searchText! });
    setState(p => ({...p, results: response.results}));
  } 
}