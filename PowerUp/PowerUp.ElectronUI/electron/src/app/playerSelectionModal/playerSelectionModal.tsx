import { useRef, useState } from "react";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { Modal } from "../../components/modal/modal";
import { TextField } from "../../components/textField/textField";
import { AppContext } from "../appContext";
import { DisableResult } from "../shared/disableResult";
import { useDebounceEffect } from "../shared/useDebounceEffect";
import { PlayerSearchApiClient, PlayerSearchResultDto } from "./playerSearchApiClient";
import { PlayerSelectionGrid } from "./playerSelectionGrid";

export interface PlayerSelectionModalProps {
  appContext: AppContext;
  closeDialog: (selectedPlayerId: number | undefined) => void;
  isPlayerDisabled?: (player: PlayerSearchResultDto) => DisableResult;
}

interface PlayerSelectionModalState {
  results: PlayerSearchResultDto[];
  searchText: string | undefined;
  selectedPlayer: PlayerSearchResultDto | undefined;
}

export function PlayerSelectionModal(props: PlayerSelectionModalProps) {
  const { appContext, isPlayerDisabled, closeDialog } = props;

  const apiClientRef = useRef(new PlayerSearchApiClient(appContext.commandFetcher))

  const [state, setState] = useState<PlayerSelectionModalState>({
    results: [],
    searchText: undefined,
    selectedPlayer: undefined
  });

  const isSearching = state.searchText && state.searchText.length > 0;

  useDebounceEffect(() => { search() }, 500, [state.searchText])

  return <Modal ariaLabel='Select Player' fullHeight>
    <Wrapper>
      <SelectionHeader>
        <SelectionHeading>Select Player</SelectionHeading>
        <SearchBoxWrapper>
          <TextField 
            value={state.searchText}
            onChange={text => setState(p => ({...p, searchText: text}))}
          />
        </SearchBoxWrapper>
      </SelectionHeader>
      <PlayerSelectionGrid 
        selectedPlayer={state.selectedPlayer}
        players={isSearching ? state.results : []}
        noDataMessage={isSearching ? 'No players found' : 'Search for player'}
        onPlayerSelected={player => setState(p => ({...p, selectedPlayer: player}))}
        isPlayerDisabled={isPlayerDisabled}
      />
      <SelectionButtons>
        <div>
          <span style={{ fontWeight: '600' }}>
            Current Selected Player:
          </span>
          &nbsp;
          <span>
            {state.selectedPlayer ? state.selectedPlayer.informalDisplayName : 'none'}
          </span>  
        </div>
        <div style={{ display: 'flex', gap: '4px' }}>
          <Button
            size='Small'
            variant='Outline'
            onClick={() => closeDialog(undefined)}>
              Cancel
          </Button>
          <Button
            size='Small'
            variant='Fill'
            disabled={!state.selectedPlayer}
            onClick={() => closeDialog(state.selectedPlayer?.playerId)}>
              Select Player
          </Button>
        </div>
      </SelectionButtons>
    </Wrapper>
  </Modal>

  async function search() {
    if(!isSearching)
      return;
    const response = await apiClientRef.current.execute({ searchText: state.searchText! });
    setState(p => ({...p, results: response.results}));
  } 
}

const Wrapper = styled.div`
  display: grid;
  grid-template-rows: min-content auto min-content;
  gap: 8px;
  height: 100%;
`

const SelectionHeader = styled.div`
  display: flex;
  align-items: flex-end;
  gap: 16px;
`

const SelectionHeading = styled.h2`
  font-style: italic;
`

const SearchBoxWrapper = styled.div`
  flex: auto;
`

const SelectionButtons = styled.div`
  display: flex;
  align-items: center;
  justify-content: space-between;
`