import { useMemo, useReducer } from "react";
import { AppContext } from "../app";
import { DraftPoolApiClient } from "../shared/draftPoolApiClient";
import { PageLoadFunction } from "../pages";
import { useReducerWithContext } from "../../utils/reducerWithContext";
import { DraftStateReducer, getInitialState, getLastPickingPlayerIndex, getNextPickingPlayherIndex as getNextPickingPlayerIndex, getPickingPlayerIndex } from "./draftState";
import { PowerUpLayout } from "../shared/powerUpLayout";
import { Breadcrumbs } from "../../components/breadcrumbs/breadcrumbs";
import { ContentWithHangingHeader } from "../../components/hangingHeader/hangingHeader";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { NumberField } from "../../components/numberField/numberField";
import { FlexFracItem, FlexRow } from "../../components/flexRow/flexRow";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { ConfirmationModal } from "../../components/modal/confirmationModal";
import { COLORS } from "../../style/constants";
import { Spinner } from "../../components/spinner/spinner";
import { PositionBubble } from "../../components/textBubble/positionBubble";
import { getPositionType } from "../shared/positionCode";
import { PlayerNameBubble } from "../../components/textBubble/playerNameBubble";
import { PlayerDetailsResponse } from "../teamEditor/playerDetailsResponse";

interface DraftPageProps {
  appContext: AppContext;
  rosterId: number;
}

const teams = 2;

function DraftPage({ appContext, rosterId }: DraftPageProps) {
  const [state, update] = useReducer(
    DraftStateReducer, 
    getInitialState(teams), 
  )
  const draftPoolApiClient = useMemo(() => new DraftPoolApiClient(appContext.commandFetcher), [appContext.commandFetcher]);

  const draftingIndex = getPickingPlayerIndex(state.selections);
  const nextDraftingIndex = getNextPickingPlayerIndex(state.selections);
  const allSelections = state.selections.flat();
  const draftingTeamPlayers = state.selections[draftingIndex].map(s => state.draftPool.find(p => p.playerId === s)!);
  const nextUpPlayers = state.selections[nextDraftingIndex].map(s => state.draftPool.find(p => p.playerId === s)!);
  const undraftedPlayers = state.draftPool.filter(p => !allSelections.some(s => s === p.playerId));
  
  const header = <>
    <Breadcrumbs appContext={appContext}/>
  </>

  return <PowerUpLayout appContext={appContext} headerText="Draft Teams">
    <ContentWithHangingHeader header={header} headerHeight="48px">
      <Wrapper>
        <TeamContainer>
          <PlayerGrid>
            {state.teams === 2 && draftingIndex === 0
              ? draftingTeamPlayers.map(toDraftedPlayer)
              : nextUpPlayers.map(toDraftedPlayer)}
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
                value={state.teams}
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
        <TeamContainer>
          <PlayerGrid>
            {state.teams === 2 && draftingIndex === 0
              ? nextUpPlayers.map(toDraftedPlayer)
              : draftingTeamPlayers.map(toDraftedPlayer)}
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
`

const PlayerGrid = styled.div`
  padding: 16px 8px;
  display: flex;
  flex-direction: column;
  gap: 4px;
`

const TeamContainer = styled.div`
  flex: 2;
  min-width: 0;
  background-color: ${COLORS.jet.lighter_71};
  padding: 0 8px;
  height: 100%;
  overflow: auto;
`