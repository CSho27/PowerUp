import { useRef, useState } from "react";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow";
import { Modal } from "../../components/modal/modal";
import { NumberField } from "../../components/numberField/numberField";
import { TextField } from "../../components/textField/textField";
import { AppContext } from "../app";
import { useDebounceEffect } from "../shared/useDebounceEffect";
import { FranchiseLookupApiClient, FranchiseLookupResultDto } from "./franchiseLookupApiClient";
import { FranchiseLookupGrid } from "./franchiseLookupGrid";
import { TeamGenerationApiClient } from "./teamGenerationApiClient";

export interface TeamGenerationModalProps {
  appContext: AppContext;
  closeDialog: (generatedPlayerId: number | undefined) => void;
}

interface TeamGenerationModalState {
  results: FranchiseLookupResultDto[];
  searchText: string | undefined;
  selectedTeam: FranchiseLookupResultDto | undefined;
  minYear: number | undefined;
  maxYear: number | undefined;
  yearToGenerate: number | undefined;
}

export function TeamGenerationModal(props: TeamGenerationModalProps) {
  const { appContext, closeDialog } = props;
  const lookupApiClientRef = useRef(new FranchiseLookupApiClient(appContext.commandFetcher));
  const generationApiClientRef = useRef(new TeamGenerationApiClient(appContext.commandFetcher));
  const [state, setState] = useState<TeamGenerationModalState>({
    results: [],
    searchText: undefined,
    selectedTeam: undefined,
    minYear: undefined,
    maxYear: undefined,
    yearToGenerate: undefined
  });
  
  const isSearching = state.searchText && state.searchText.length > 0
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
      <FranchiseLookupGrid 
        selectedTeam={state.selectedTeam}
        teams={state.results}
        noDataMessage={isSearching ? 'No teams found' : 'Search for team'}
        onTeamSelected={handleSelectTeam}
      />
      <FlexRow gap='8px'withBottomPadding>
        <FlexFracItem frac='1/3'>
          <FieldLabel>Current Selected Team</FieldLabel>
          <div>
            {state.selectedTeam ? state.selectedTeam.name : 'none'}
          </div>
        </FlexFracItem>
        <FlexFracItem frac='1/3'>
          <FieldLabel htmlFor='year-to-generate'>Year to Generate</FieldLabel>
          <NumberField 
            id='year-to-generate'
            type='PossiblyUndefined'
            min={state.minYear}
            max={state.maxYear}
            value={state.yearToGenerate} 
            onChange={year => setState(p => ({...p, yearToGenerate: year}))}
          />
        </FlexFracItem>
      </FlexRow>
      <div style={{ display: 'flex', gap: '4px', justifyContent: 'flex-end' }}>
        <Button
          size='Small'
          variant='Outline'
          onClick={() => closeDialog(undefined)}>
            Cancel
        </Button>
        <Button
          size='Small'
          variant='Fill'
          disabled={!state.selectedTeam || !state.yearToGenerate}
          onClick={generateAndClose}>
            Generate Team
        </Button>
      </div>
    </Wrapper>
  </Modal>

  async function search() {
    if(!isSearching)
      return;

    const response = await lookupApiClientRef.current.execute({ searchText: state.searchText! });
    setState(p => ({ ...p, results: response.results }));
  }

  async function handleSelectTeam(team: FranchiseLookupResultDto) {
    const maxYear = team.endYear ?? new Date().getFullYear();

    setState(p => ({
      ...p, 
      selectedTeam: team, 
      minYear: team.beginYear,
      maxYear: maxYear,
      yearToGenerate: maxYear
    }));
  }

  async function generateAndClose() {
    const result = await generationApiClientRef.current.execute({ 
      lsTeamId: state.selectedTeam!.lsTeamId,
      year: state.yearToGenerate!,
      teamName: state.selectedTeam!.name
    });
    closeDialog(result.teamId);
  }
}

const Wrapper = styled.div`
  display: grid;
  grid-template-rows: min-content auto min-content min-content;
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