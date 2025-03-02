import { useMemo, useReducer } from "react";
import { AppContext } from "../appContext";
import { DraftPoolApiClient } from "./draftPoolApiClient";
import { PageLoadFunction, PagePropsLoadFunction } from "../pages";
import { DraftStateReducer, getInitialState, getNextPickingPlayherIndex as getNextPickingPlayerIndex, getDraftingIndex, getRound } from "./draftState";
import { PowerUpLayout } from "../shared/powerUpLayout";
import { Breadcrumbs } from "../../components/breadcrumbs/breadcrumbs";
import { ContentWithHangingHeader } from "../../components/hangingHeader/hangingHeader";
import styled from "styled-components";
import { Button } from "../../components/button/button";
import { NumberField } from "../../components/numberField/numberField";
import { FieldLabel } from "../../components/fieldLabel/fieldLabel";
import { ConfirmationModal } from "../../components/modal/confirmationModal";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { Spinner } from "../../components/spinner/spinner";
import { PositionBubble } from "../../components/textBubble/positionBubble";
import { getPositionType } from "../shared/positionCode";
import { PlayerNameBubble } from "../../components/textBubble/playerNameBubble";
import { PlayerDetailsResponse } from "../teamEditor/playerDetailsResponse";
import { TextField } from "../../components/textField/textField";
import { SortHelpers } from "../../utils/sortUtils";
import { LoadExistingRosterApiClient } from "../rosterEditor/loadExistingRosterApiClient";
import { SimpleCode } from "../shared/simpleCode";
import { SelectField } from "../../components/SelectField/selectField";
import { fromOptions, toOptions } from "../../components/SelectField/selectFieldHelpers";
import { KeyedCode } from "../shared/keyedCode";
import { ReplaceWithDraftedTeamsApiClient } from "./replaceWithDraftedTeamsApiClient";

interface DraftPageProps {
  appContext: AppContext;
  rosterId: number;
  existingTeams: KeyedCode[];
}

function DraftPage({ appContext, rosterId, existingTeams }: DraftPageProps) {
  const [state, update] = useReducer(
    DraftStateReducer, 
    getInitialState(2), 
  )
  const draftPoolApiClient = useMemo(() => new DraftPoolApiClient(appContext.commandFetcher), [appContext.commandFetcher]);
  const replacerApiClient = useMemo(() => new ReplaceWithDraftedTeamsApiClient(appContext.commandFetcher), [appContext.commandFetcher]);

  const draftPoolSize = (state.numberOfTeams + 2) * 25;
  const generationEstimate = Math.round((1/50) * draftPoolSize); 

  const round = getRound(state.teams);
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
  const undraftedPlayers = state.draftPool.sort(SortHelpers.numericallyByDesc(p => p.overall)).filter(p => !allSelections.some(s => s === p.playerId));
  
  return <PowerUpLayout headerText="Draft Teams">
    <ContentWithHangingHeader header={<Breadcrumbs appContext={appContext}/>} headerHeight="48px">
      <Wrapper>
        <TeamContainer isDrafting={leftTeamIndex === draftingIndex}>
          <PlayerGrid>
            <TeamField>
              <FieldLabel>Team to Replace</FieldLabel>
              <SelectField 
                value={leftTeam.replaceTeam?.key} 
                onChange={value => update({ type: 'updateReplaceTeam', teamIndex: leftTeamIndex, team: fromOptions(existingTeams, value) })} 
                >
                {toOptions(existingTeams, true)}
              </SelectField>
            </TeamField>
            <TeamField>
              <FieldLabel>Team Name</FieldLabel>
              <TextField 
                placeholder="Enter team name"
                value={leftTeam.name}
                onChange={name => update({ type: 'updateTeamName', teamIndex: leftTeamIndex, name: name })}
                />
            </TeamField>
            {leftTeamPlayers.map(toDraftedPlayer)}
          </PlayerGrid>
        </TeamContainer>
        <DraftPoolContainer>
          <RoundHeader>
            Round: {round}, Pick: {draftingIndex+1}
          </RoundHeader>
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
                disabled={state.draftPool.length > 0}
                onChange={handlePlayersDraftingChange} 
              />
            </div>
            {!state.isGenerating && allSelections.length === 0 &&
            <Button onClick={generateDraftPool} size='Small' variant="Fill">
                {state.draftPool.length > 0 ? 'Re-generate' : 'Generate'} Draft Pool
            </Button>}
            {allSelections.length > 0 &&
            <Button onClick={handleStartOver} size='Small' variant="Fill">
                Start Over
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
              <div>Generating draft pool. This should take about {generationEstimate} minutes.</div>
            </div>}
          </div>
          <PlayerGrid>
            {undraftedPlayers.map(toUndradftedPlayer)}
          </PlayerGrid>
        </DraftPoolContainer>
        <TeamContainer isDrafting={rightTeamIndex === draftingIndex}>
          <PlayerGrid>
            <TeamField>
              <FieldLabel>Team to Replace</FieldLabel>
              <SelectField
                value={rightTeam.replaceTeam?.key} 
                onChange={value => update({ type: 'updateReplaceTeam', teamIndex: rightTeamIndex, team: fromOptions(existingTeams, value) })} 
              >
                {toOptions(existingTeams, true)}
              </SelectField>
            </TeamField>
            <TeamField>
              <FieldLabel>Team Name</FieldLabel>
              <TextField 
                placeholder="Enter team name"
                value={rightTeam.name}
                onChange={name => update({ type: 'updateTeamName', teamIndex: rightTeamIndex, name: name })}
                />
            </TeamField>
            {rightTeamPlayers.map(toDraftedPlayer)}
          </PlayerGrid>
        </TeamContainer>
      </Wrapper>
    </ContentWithHangingHeader>
  </PowerUpLayout>

  function toUndradftedPlayer(p: PlayerDetailsResponse, index: number) {
    return <div 
      key={p.playerId} 
      style={{ display: 'flex', gap: '8px', alignItems: 'center' }}
    >
      <div style={{ width: '3ch' }}>{index+1}.</div>
      <Button
        size='Small'
        variant='Outline'
        title={'Draft Player'}
        icon={'plus'}
        squarePadding
        onClick={() => handleSelection(p.playerId)}
      />
      <div style={{ width: '2ch' }}>{p.overall}</div>
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

  async function handlePlayersDraftingChange(value: number) {
    update({ type: 'updateTeams', teams: value });
  }

  async function generateDraftPool() {
    update({ type: 'startedGenerating' });
    const draftPool = await draftPoolApiClient.execute({ size: draftPoolSize });
    update({ type: 'finishedGenerating', draftPool: draftPool.players })
  }

  async function handleSelection(playerId: number) {
    const isFinalSelection = round === 25 && draftingIndex === state.teams.length-1;

    update({ type: 'makeSelection', playerId: playerId })
    
    if(isFinalSelection)
      confirmAndComplete();  
  }

  async function handleStartOver() {
    const shouldStartOver = await appContext.openModalAsync(closeAndResolve => <ConfirmationModal 
      message="Are you sure you'd like to start over?"
      secondaryMessage='All current selections will be lost.'
      withDeny
      confirmVerb='Start Over'
      closeDialog={closeAndResolve}
    />)
    if(shouldStartOver)
      update({ type: 'resetPicks' });
  }

  async function confirmAndComplete() {
    const shouldComplete = await appContext.openModalAsync(closeAndResolve => <ConfirmationModal 
      message='Draft complete!'
      secondaryMessage='Would you like to save these teams to the current roster?'
      withDeny
      denyVerb="Start Over"
      confirmVerb='Add To Roster'
      closeDialog={closeAndResolve}
    />)
    if(shouldComplete) {
      await replacerApiClient.execute({
        rosterId: rosterId,
        teams: state.teams.map(t => ({
          teamToReplace: t.replaceTeam!.key,
          teamName: t.name,
          playerIds: t.selections
        }))
      })
      appContext.popBreadcrumb(appContext.breadcrumbs[appContext.breadcrumbs.length-2].id);
    } 
    else {
      update({ type: 'resetDraft' });
    }
  }
}

export const loadDraftPageProps: PagePropsLoadFunction<DraftPageProps> = async (appContext, pageDef) => {
  if(pageDef.page !== 'DraftPage') throw '';
  
  const apiClient = new LoadExistingRosterApiClient(appContext.commandFetcher);
  const response = await apiClient.execute({ rosterId: pageDef.rosterId });
  const teamOptions = response.rosterDetails.teams
    .map(t => ({
      key: t.powerProsTeam, 
      name: t.name === t.powerProsName
        ? t.name
        : `${t.name} (${t.powerProsName})`
      }));
  
  return {
    title: 'Draft Teams',
    rosterId: pageDef.rosterId, 
    existingTeams: teamOptions
  }
}

export const loadDraftPage: PageLoadFunction = async (appContext, pageDef) => {
  const props = await loadDraftPageProps(appContext, pageDef);
  return {
    title: props.title,
    renderPage: appContext => <DraftPage 
      appContext={appContext} 
      rosterId={props.rosterId} 
      existingTeams={props.existingTeams}
    />
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

const TeamField = styled.div`
  padding-bottom: 12px;
`

const RoundHeader = styled.div`
  font-weight: 500;
  font-size: ${FONT_SIZES._24};
  padding-bottom: 12px;
`