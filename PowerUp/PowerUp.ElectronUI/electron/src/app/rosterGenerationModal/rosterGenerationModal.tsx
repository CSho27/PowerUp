import { useRef, useState } from "react";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow";
import { Modal } from "../../components/modal/modal";
import { NumberField } from "../../components/numberField/numberField";
import { ProgressBar } from "../../components/progressBar/progressBar";
import { AppContext } from "../app";
import { RosterGenerationApiClient } from "./rosterGenerationApiClient";
import { RosterGenerationStatusApiClient } from "./rosterGenerationStatusApiClient";
import { ConfirmationModal } from "../../components/modal/confirmationModal";

interface RosterGenerationModalProps {
  appContext: AppContext;
  closeDialog: (generatedRosterId: number | undefined) => void;
}

const minYear = 1876;

interface RosterGenerationModalState {
  yearToGenerate: number | undefined;
  currentTeamGenerationAction: string | undefined;
  currentPlayerGenerationAction: string | undefined;
  generationProgress: number | undefined;
  estimatedTimeRemaining: string | undefined
}

export function RosterGenerationModal(props: RosterGenerationModalProps) {
  const { appContext, closeDialog } = props;
  const generationApiClientRef = useRef(new RosterGenerationApiClient(appContext.commandFetcher));
  const statusApiClientRef = useRef(new RosterGenerationStatusApiClient(appContext.commandFetcher));
  const currentYear = new Date().getFullYear();
  const [state, setState] = useState<RosterGenerationModalState>({
    yearToGenerate: currentYear,
    currentTeamGenerationAction: undefined,
    currentPlayerGenerationAction: undefined,
    generationProgress: undefined,
    estimatedTimeRemaining: undefined
  });

  return <Modal ariaLabel='Generate Roster' width={!state.currentTeamGenerationAction ? '500px' : undefined}>
    {!state.currentTeamGenerationAction && <>
      <FieldLabel htmlFor='year-to-generate'>Generate MLB Rosters for Year</FieldLabel>
      <FlexRow gap='8px' withBottomPadding>
        <FlexFracItem frac='1/3'>
          <NumberField 
            type='PossiblyUndefined' 
            value={state.yearToGenerate}
            min={minYear}
            max={currentYear}
            onChange={year => setState(p => ({ ...p, yearToGenerate: year }))}
          />
        </FlexFracItem>
      </FlexRow>
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
          disabled={!state.yearToGenerate}
          onClick={generateAndClose}>
            Generate Rosters
        </Button>
      </div>  
    </>}
    {state.currentTeamGenerationAction && <GenerationWrapper>
      <FieldLabel>Generating {state.yearToGenerate} MLB Rosters</FieldLabel>
      <ProgressBar size='Large' progress={state.generationProgress!} />
      <GenerationDetailsContainer>
        <div>
          <div>{state.generationProgress}%</div>
          <div>{state.currentTeamGenerationAction}</div>
          <div>{state.currentPlayerGenerationAction}</div>
        </div>
        <div>Est. Time Remaining: {state.estimatedTimeRemaining}</div>
      </GenerationDetailsContainer>
    </GenerationWrapper>}
  </Modal>

  async function generateAndClose() {
    const result = await generationApiClientRef.current.execute({ year: state.yearToGenerate! });
    pollProgress();

    async function pollProgress() {
      const status = await statusApiClientRef.current.execute({ generationStatusId: result.generationStatusId });
      setState(p => ({
        ...p, 
        currentTeamGenerationAction: status.currentTeamAction,
        currentPlayerGenerationAction: status.currentPlayerAction, 
        generationProgress: status.percentCompletion,
        estimatedTimeRemaining: status.estimatedTimeToCompletion
      }));
      
      if(!!status.completedRosterId) {
        closeDialog(status.completedRosterId);
      }
      else if(status.isFailed) {
        closeDialog(undefined);
        await appContext.openModalAsync(closeAndResolve => <ConfirmationModal 
          message="Failed to generate roster"
          secondaryMessage='Please try again later'
          closeDialog={closeAndResolve}
        />)
      }
      else {
        setTimeout(pollProgress, 20);
      }
    }
  }
}


const GenerationWrapper = styled.div`
  display: grid;
  grid-template-rows: min-content min-content min-content;
  gap: 8px;
  height: 100%;
`

const GenerationDetailsContainer = styled.div`
  display: grid;
  grid-template-columns: auto 275px;
  gap: 8px;
`