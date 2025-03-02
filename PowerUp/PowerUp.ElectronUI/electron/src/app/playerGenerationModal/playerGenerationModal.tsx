import { useRef, useState } from "react";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow";
import { Modal } from "../../components/modal/modal";
import { NumberField } from "../../components/numberField/numberField";
import { TextField } from "../../components/textField/textField";
import { AppContext } from "../appContext";
import { useDebounceEffect } from "../../components/hooks/useDebounceEffect";
import { PlayerGenerationApiClient } from "./playerGenerationApiClient";
import { PlayerInfoApiClient } from "./playerInfoApiClient";
import { PlayerLookupApiClient, PlayerLookupResultDto } from "./playerLookupApiClient";
import { PlayerLookupGrid } from "./playerLookupGrid";

export interface PlayerGenerationModalProps {
  appContext: AppContext;
  closeDialog: (generatedPlayerId: number | undefined) => void;
}

interface PlayerGenerationModalState {
  results: PlayerLookupResultDto[];
  searchText: string | undefined;
  selectedPlayer: PlayerLookupResultDto | undefined;
  minYear: number | undefined;
  maxYear: number | undefined;
  yearToGenerate: number | undefined;
}

export function PlayerGenerationModal(props: PlayerGenerationModalProps) {
  const { appContext, closeDialog } = props;
  const lookupApiClientRef = useRef(new PlayerLookupApiClient(appContext.commandFetcher));
  const playerInfoApiClientRef = useRef(new PlayerInfoApiClient(appContext.commandFetcher));
  const generationApiClientRef = useRef(new PlayerGenerationApiClient(appContext.commandFetcher));
  const [state, setState] = useState<PlayerGenerationModalState>({
    results: [],
    searchText: undefined,
    selectedPlayer: undefined,
    minYear: undefined,
    maxYear: undefined,
    yearToGenerate: undefined
  });
  
  const isSearching = state.searchText && state.searchText.length > 0
  useDebounceEffect(() => { search() }, 500, [state.searchText]);

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
      <PlayerLookupGrid 
        selectedPlayer={state.selectedPlayer}
        players={state.results}
        noDataMessage={isSearching ? 'No players found' : 'Search for player'}
        onPlayerSelected={handleSelectPlayer}
      />
      <FlexRow gap='8px'withBottomPadding>
        <FlexFracItem frac='1/3'>
          <FieldLabel>Current Selected Player</FieldLabel>
          <div>
            {state.selectedPlayer ? state.selectedPlayer.informalDisplayName : 'none'}
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
          disabled={!state.selectedPlayer || !state.yearToGenerate}
          onClick={generateAndClose}>
            Generate Player
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

  async function handleSelectPlayer(player: PlayerLookupResultDto) {
    const response = await playerInfoApiClientRef.current.execute({ lsPlayerId: player.lsPlayerId });
    const maxYear = response.endYear ?? new Date().getFullYear();
    const middleYear = !!response.startYear
      ? (maxYear + response.startYear) / 2
      : undefined;

    setState(p => ({
      ...p, 
      selectedPlayer: player, 
      minYear: response.startYear ?? undefined,
      maxYear: maxYear,
      yearToGenerate: !!middleYear
        ? Math.round(middleYear)
        : undefined
    }));
  }

  async function generateAndClose() {
    const result = await generationApiClientRef.current.execute({ 
      lsPlayerId: state.selectedPlayer!.lsPlayerId,
      year: state.yearToGenerate!
    });

    closeDialog(result.playerId);
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