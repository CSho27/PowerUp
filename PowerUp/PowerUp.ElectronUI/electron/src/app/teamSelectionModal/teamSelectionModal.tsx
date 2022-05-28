import { useRef, useState } from "react";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { Modal } from "../../components/modal/modal";
import { TextField } from "../../components/textField/textField";
import { AppContext } from "../app";
import { useDebounceEffect } from "../shared/useDebounceEffect";
import { TeamSearchApiClient, TeamSearchResultDto } from "./teamSearchApiClient";
import { TeamSelectionGrid } from "./teamSelectionGrid";

export interface TeamSelectionModalProps {
  appContext: AppContext;
  closeDialog: (selectedTeamId: number | undefined) => void;
}

interface TeamSelectionModalState {
  results: TeamSearchResultDto[];
  searchText: string | undefined;
  selectedTeam: TeamSearchResultDto | undefined;
}

export function TeamSelectionModal(props: TeamSelectionModalProps) {
  const { appContext, closeDialog } = props;
  const apiClientRef = useRef(new TeamSearchApiClient(appContext.commandFetcher));
  const [state, setState] = useState<TeamSelectionModalState>({
    results: [],
    searchText: undefined,
    selectedTeam: undefined
  });

  const isSearching = state.searchText && state.searchText.length > 0;
  useDebounceEffect(() => { search() }, 500, [state.searchText]);

  return <Modal ariaLabel='Select Team' fullHeight>
    <Wrapper>
      <SelectionHeader>
        <SelectionHeading>Select Team</SelectionHeading>
        <SearchBoxWrapper>
          <TextField 
            value={state.searchText}
            onChange={text => setState(p => ({...p, searchText: text}))}
          />
        </SearchBoxWrapper>
      </SelectionHeader>
      <TeamSelectionGrid
        selectedTeam={state.selectedTeam}
        teams={state.results}
        noDataMessage={isSearching ? 'No teams found' : 'Search for team'}
        onTeamSelected={team => setState(p => ({...p, selectedTeam: team}))}
      />
      <SelectionButtons>
        <div>
          <span style={{ fontWeight: '600' }}>
            Current Selected Team:
          </span>
          &nbsp;
          <span>
            {state.selectedTeam ? state.selectedTeam.name : 'none'}
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
            disabled={!state.selectedTeam}
            onClick={() => closeDialog(state.selectedTeam?.teamId)}>
              Select Team
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