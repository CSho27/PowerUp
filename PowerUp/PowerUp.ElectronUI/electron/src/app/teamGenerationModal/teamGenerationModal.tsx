import { useRef, useState } from "react";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow";
import { Modal } from "../../components/modal/modal";
import { NumberField } from "../../components/numberField/numberField";
import { ProgressBar } from "../../components/progressBar/progressBar";
import { TextField } from "../../components/textField/textField";
import { AppContext } from "../app";
import { useDebounceEffect } from "../shared/useDebounceEffect";
import { FranchiseLookupApiClient, FranchiseLookupResultDto } from "./franchiseLookupApiClient";
import { FranchiseLookupGrid } from "./franchiseLookupGrid";
import { TeamGenerationApiClient } from "./teamGenerationApiClient";
import { TeamGenerationStatusApiClient } from "./teamGenerationStatusApiClient";

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
  currentGenerationAction: string | undefined;
  generationProgress: number | undefined;
  estimatedTimeRemaining: string | undefined
}

export function TeamGenerationModal(props: TeamGenerationModalProps) {
  const { appContext, closeDialog } = props;
  const lookupApiClientRef = useRef(new FranchiseLookupApiClient(appContext.commandFetcher));
  const generationApiClientRef = useRef(new TeamGenerationApiClient(appContext.commandFetcher));
  const statusApiClientRef = useRef(new TeamGenerationStatusApiClient(appContext.commandFetcher));
  const [state, setState] = useState<TeamGenerationModalState>({
    results: [],
    searchText: undefined,
    selectedTeam: undefined,
    minYear: undefined,
    maxYear: undefined,
    yearToGenerate: undefined,
    currentGenerationAction: undefined,
    generationProgress: undefined,
    estimatedTimeRemaining: undefined
  });
  
  const isSearching = state.searchText && state.searchText.length > 0
  useDebounceEffect(() => { search() }, 500, [state.searchText]);

  return <Modal ariaLabel='Generate Team' fullHeight={!state.currentGenerationAction}>
    {!state.currentGenerationAction && <Wrapper>
      <ModalHeader>
        <ModalHeading>Select Team</ModalHeading>
        <SearchBoxWrapper>
          <TextField 
            value={state.searchText}
            onChange={text => setState(p => ({...p, searchText: text}))}
          />
        </SearchBoxWrapper>
      </ModalHeader>
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
    </Wrapper>}
    {state.currentGenerationAction && <GenerationWrapper>
      <ModalHeading>Generating {state.yearToGenerate} {state.selectedTeam!.name}</ModalHeading>
        <ProgressBar size='Large' progress={state.generationProgress!} />
        <GenerationDetailsContainer>
          <div>{state.generationProgress}%</div>
          <div>{state.currentGenerationAction}</div>
          <div>est. Time Remaining: {state.estimatedTimeRemaining}</div>
        </GenerationDetailsContainer>
    </GenerationWrapper>}
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

    setTimeout(pollProgress, 1000);

    async function pollProgress() {
      const status = await statusApiClientRef.current.execute({ generationStatusId: result.generationStatusId });
      setState(p => ({
        ...p, 
        currentGenerationAction: status.currentAction, 
        generationProgress: status.percentCompletion,
        estimatedTimeRemaining: status.estimatedTimeToCompletion
      }));
      
      if(status.completedTeamId)
        closeDialog(status.completedTeamId);
      else
        pollProgress();
    }
  }
}

const Wrapper = styled.div`
  display: grid;
  grid-template-rows: min-content auto min-content min-content;
  gap: 8px;
  height: 100%;
`

const ModalHeader = styled.div`
  display: flex;
  align-items: flex-end;
  gap: 16px;
`

const ModalHeading = styled.h2`
  font-style: italic;
`

const SearchBoxWrapper = styled.div`
  flex: auto;
`

const GenerationWrapper = styled.div`
  display: grid;
  grid-template-rows: min-content min-content min-content;
  gap: 8px;
  height: 100%;
`

const GenerationDetailsContainer = styled.div`
  display: grid;
  grid-template-columns: min-content auto 275px;
  gap: 8px;
`