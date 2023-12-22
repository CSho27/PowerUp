import { useMemo, useReducer } from "react";
import { AppContext } from "../app";
import { DraftPoolApiClient } from "../shared/draftPoolApiClient";
import { PageLoadFunction } from "../pages";
import { useReducerWithContext } from "../../utils/reducerWithContext";
import { DraftStateReducer, getInitialState, getLastPickingPlayerIndex, getNextPickingPlayherIndex as getNextPickingPlayerIndex, getDraftingIndex } from "./draftState";
import { PowerUpLayout } from "../shared/powerUpLayout";
import { Breadcrumbs } from "../../components/breadcrumbs/breadcrumbs";
import { ContentWithHangingHeader } from "../../components/hangingHeader/hangingHeader";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { NumberField } from "../../components/numberField/numberField";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { ConfirmationModal } from "../../components/modal/confirmationModal";
import { COLORS } from "../../style/constants";
import { Spinner } from "../../components/spinner/spinner";
import { PositionBubble } from "../../components/textBubble/positionBubble";
import { getPositionType } from "../shared/positionCode";
import { PlayerNameBubble } from "../../components/textBubble/playerNameBubble";
import { PlayerDetailsResponse } from "../teamEditor/playerDetailsResponse";
import { TextField } from "../../components/textField/textField";

interface DraftPageProps {
  appContext: AppContext;
  rosterId: number;
}

function DraftPage({ appContext, rosterId }: DraftPageProps) {
  const [state, update] = useReducer(
    DraftStateReducer, 
    getInitialState(2), 
  )
  const draftPoolApiClient = useMemo(() => new DraftPoolApiClient(appContext.commandFetcher), [appContext.commandFetcher]);

  const draftingIndex = getDraftingIndex(state.teams);
  const nextDraftingIndex = getNextPickingPlayerIndex(state.teams);
  const allSelections = state.teams.flatMap(t => t.selections);

  const leftTeamIndex = state.numberOfTeams === 2 && draftingIndex === 0
    ? draftingIndex
    : nextDraftingIndex;
  const rightTeamIndex = state.numberOfTeams === 2 && draftingIndex === 0
    ? nextDraftingIndex
    : draftingIndex;
  const leftTeam = state.teams[leftTeamIndex];
  const rightTeam = state.teams[rightTeamIndex];

  const leftTeamPlayers = leftTeam.selections.map(s => state.draftPool.find(p => p.playerId === s)!);
  const rightTeamPlayers = rightTeam.selections.map(s => state.draftPool.find(p => p.playerId === s)!);
  const undraftedPlayers = state.draftPool.filter(p => !allSelections.some(s => s === p.playerId));
  
  const header = <>
    <Breadcrumbs appContext={appContext}/>
  </>

  return <PowerUpLayout appContext={appContext} headerText="Draft Teams">
    <ContentWithHangingHeader header={header} headerHeight="48px">
      <Wrapper>
        <TeamContainer isDrafting={leftTeamIndex === draftingIndex}>
          <PlayerGrid>
            <TextField 
              placeholder="Enter team name"
              value={leftTeam.name}
              onChange={name => update({ type: 'updateTeamName', teamIndex: leftTeamIndex, name: name })}
            />
            {leftTeamPlayers.map(toDraftedPlayer)}
          </PlayerGrid>
        </TeamContainer>
        <DraftPoolContainer>
          <div 
            style={{ 
              display: 'flex',
              justifyContent: 'center',
              alignItems: 'flex-end', 
              gap: '16px'
            }}
          >
            <div style={{ width: '96px' }}>
              <FieldLabel htmlFor='teams'>Teams</FieldLabel>
              <NumberField 
                id='teams' 
                type='Defined' 
                min={2}
                value={state.numberOfTeams}
                onChange={handlePlayersDraftingChange} 
              />
            </div>
            {!state.isGenerating && 
            <Button onClick={generateDraftPool} size='Small' variant="Fill">
                Generate Draft Pool
            </Button>}
            {allSelections.length > 0 && <Button
              size='Small'
              variant='Ghost'
              title='Undo Last Pick'
              icon='rotate-left'
              squarePadding
              onClick={() => update({ type: 'undoSelection' })}
            />}
            {state.isGenerating && <div style={{ display: 'flex', gap: '16px', alignItems: 'center' }}>
              <Spinner />
              <div>Generating draft pool. This generally takes about 2 minutes.</div>
            </div>}
          </div>
          <PlayerGrid>
            {undraftedPlayers.map(toUndradftedPlayer)}
          </PlayerGrid>
        </DraftPoolContainer>
        <TeamContainer isDrafting={rightTeamIndex === draftingIndex}>
          <PlayerGrid>
            <TextField 
              placeholder="Enter team name"
              value={rightTeam.name}
              onChange={name => update({ type: 'updateTeamName', teamIndex: rightTeamIndex, name: name })}
            />
            {rightTeamPlayers.map(toDraftedPlayer)}
          </PlayerGrid>
        </TeamContainer>
      </Wrapper>
    </ContentWithHangingHeader>
  </PowerUpLayout>

  function toUndradftedPlayer(p: PlayerDetailsResponse) {
    return <div 
      key={p.playerId} 
      style={{ display: 'flex', gap: '8px', alignItems: 'center' }}
    >
      <Button
        size='Small'
        variant='Outline'
        title={'Draft Player'}
        icon={'plus'}
        squarePadding
        onClick={() => update({ type: 'makeSelection', playerId: p.playerId })}
      />
      <div>{p.overall}</div>
      <PositionBubble
        positionType={getPositionType(p.position)}
        size='Medium'
        squarePadding
      >
        {p.positionAbbreviation}
      </PositionBubble>
      <div style={{ flex: 'auto' }}>
        <PlayerNameBubble
          appContext={appContext}
          sourceType={p.sourceType}
          playerId={p.playerId}
          positionType={getPositionType(p.position)}
          size='Medium'
          fullWidth
          withoutPID
          withoutSourceType
        >
          {p.savedName}
        </PlayerNameBubble>
      </div>
    </div>
  }

  function toDraftedPlayer(p: PlayerDetailsResponse) {
    return <div 
      key={p.playerId} 
      style={{ display: 'flex', gap: '8px', alignItems: 'center' }}
    >
      <div>{p.overall}</div>
      <PositionBubble
        positionType={getPositionType(p.position)}
        size='Medium'
        squarePadding
      >
        {p.positionAbbreviation}
      </PositionBubble>
      <div style={{ flex: 'auto' }}>
        <PlayerNameBubble
          appContext={appContext}
          sourceType={p.sourceType}
          playerId={p.playerId}
          positionType={getPositionType(p.position)}
          size='Medium'
          fullWidth
          withoutPID
          withoutSourceType
        >
          {p.savedName}
        </PlayerNameBubble>
      </div>
    </div>
  }

  function handlePlayersDraftingChange(value: number) {
    const shouldEdit = state.draftPool.length > 0
      ? appContext.openModalAsync(closeAndResolve => <ConfirmationModal 
          message='Are you sure you want to change the number of players drafting?'
          secondaryMessage='This will clear both the draft pool and any picks made'
          withDeny
          confirmVerb='Reset Draft Pool'
          closeDialog={closeAndResolve}
        />)
      : true;
    if(shouldEdit)
        update({ type: 'updateTeams', teams: value });
  }

  async function generateDraftPool() {
    update({ type: 'startedGenerating' });
    const draftPool = await draftPoolApiClient.execute();
    update({ type: 'finishedGenerating', draftPool: draftPool.players })
  }
}

export const loadDraftPage: PageLoadFunction = async (_, pageDef) => {
  if(pageDef.page !== 'DraftPage') throw '';

  return {
    title: 'Draft Teams',
    renderPage: appContext => <DraftPage appContext={appContext} rosterId={pageDef.rosterId} />
  }
}

const Wrapper = styled.div`
  display: flex;
  width: 100%;
  height: 100%;
`

const DraftPoolContainer = styled.div`
  flex: 3;
  min-width: 375px;
  padding: 12px 8px;
  height: 100%;
  overflow: auto;
  background-color: ${COLORS.jet.lighter_71};
`

const PlayerGrid = styled.div`
  padding: 16px 8px;
  display: flex;
  flex-direction: column;
  gap: 4px;
`

const TeamContainer = styled.div<{ isDrafting: boolean }>`
  flex: 2;
  min-width: 0;
  background-color: ${p => p.isDrafting 
    ? COLORS.primaryBlue.lighter_69_t40 
    : undefined};
  border: 3px solid ${p => p.isDrafting 
    ? COLORS.primaryBlue.regular_45 
    : 'transparent'}; 
  padding: 0 8px;
  height: 100%;
  overflow: auto;
`