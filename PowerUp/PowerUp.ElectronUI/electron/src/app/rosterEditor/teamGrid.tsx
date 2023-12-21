import { useRef } from "react";
import styled from "styled-components"
import { Button } from "../../components/button/button";
import { ContextMenuButton, ContextMenuItem } from "../../components/contextMenuButton/contextMenuButton";
import { SourceTypeStamp } from "../../components/sourceTypeStamp/sourceTypeStamp";
import { COLORS, FONT_SIZES } from "../../style/constants"
import { DisabledCriteria, toDisabledProps } from "../../utils/disabledProps";
import { toIdentifier } from "../../utils/getIdentifier";
import { AppContext } from "../app"
import { TeamGenerationModal } from "../teamGenerationModal/teamGenerationModal";
import { TeamSelectionModal } from "../teamSelectionModal/teamSelectionModal";
import { PlayerGrid } from "./playerGrid";
import { ReplaceTeamWithCopyApiClient } from "./replaceTeamWithCopyApiClient";
import { ReplaceTeamWithExistingApiClient } from "./replaceTeamWithExistingApiClient";
import { ReplaceTeamWithNewTeamApiClient } from "./replaceTeamWithNewTeamApiClient";
import { ReplaceWithExistingPlayerApiClient } from "./replaceWithExistingPlayerApiClient";
import { TeamDetails } from "./rosterEditorDTOs";

interface TeamGridProps {
  appContext: AppContext;
  rosterId: number;
  disableRosterEdit: DisabledCriteria;
  team: TeamDetails
}

export function TeamGrid(props: TeamGridProps) {
  const { appContext, rosterId, disableRosterEdit, team } = props;
  const { name, powerProsName, hitters, pitchers } = team;

  const replaceTeamWithCopyApiClientRef = useRef(new ReplaceTeamWithCopyApiClient(appContext.commandFetcher));
  const replaceTeamWithExistingApiClientRef = useRef(new ReplaceTeamWithExistingApiClient(appContext.commandFetcher));
  const replaceTeamWithNewApiClientRef = useRef(new ReplaceTeamWithNewTeamApiClient(appContext.commandFetcher));
  const replacePlayerWithExistingApiClientRef = useRef(new ReplaceWithExistingPlayerApiClient(appContext.commandFetcher));

  const teamIdentifier = toIdentifier('Team', team.teamId);
  const teamDisplayName = name === powerProsName
      ? `${name} - ${teamIdentifier}`
      : `${name} - ${teamIdentifier} (${powerProsName})` 

  const disableManageTeam: DisabledCriteria = [
    { isDisabled: !team.canEdit, tooltipIfDisabled: 'Teams of this type cannot be edited' }
  ]

  return <TeamGridTable>
    <TeamGridCaption>
      <div style={{ display: 'flex', alignItems: 'center', justifyContent: 'flex-end', gap: '8px' }}>
        <div style={{ flex: 'auto', display: 'flex', gap: '16px', alignItems: 'center' }}>
          <TeamHeader>
            {teamDisplayName}
          </TeamHeader>
          <SourceTypeStamp 
            theme='Light'
            size='Medium'
            sourceType={team.sourceType}
          />
          <TeamStatGrid>
            <TeamStatRow>
              <TeamStatLabel>Hitting</TeamStatLabel>
              <TeamStatValue>{team.hittingRating}</TeamStatValue>
            </TeamStatRow>
            <TeamStatRow>
              <TeamStatLabel>Pitching</TeamStatLabel>
              <TeamStatValue>{team.pitchingRating}</TeamStatValue>
            </TeamStatRow>
            <TeamStatRow>
              <TeamStatLabel>Overall</TeamStatLabel>
              <TeamStatValue>{team.overallRating}</TeamStatValue>
            </TeamStatRow>            
          </TeamStatGrid>
        </div>
        <Button 
          size='Medium'
          variant='Outline'
          squarePadding
          onClick={editTeam}
          title={team.canEdit
            ? 'Edit team'
            : 'View team'}
          icon={team.canEdit
            ? 'pen-to-square'
            : 'eye'}/>
        <ContextMenuButton
          size='Medium'
          variant='Outline'
          squarePadding
          icon='right-left'
          {...toDisabledProps('Replace team', ...disableRosterEdit)}
          menuItems={<>
            <ContextMenuItem 
              icon='copy'
              onClick={replaceTeamWithCopy}>
                Replace with copy
            </ContextMenuItem>
            <ContextMenuItem 
              icon='box-archive'
              onClick={replaceTeamWithExisting}>
                Replace with existing
            </ContextMenuItem>
            <ContextMenuItem 
              icon='wand-magic-sparkles'
              onClick={replaceWithGeneratedTeam}>
                Replace with generated
            </ContextMenuItem>
            <ContextMenuItem 
              icon='circle-plus'
              onClick={replaceWithNewTeam}>
                Replace with new
            </ContextMenuItem>
          </>}/>
      </div>
    </TeamGridCaption>
    <PlayerGrid 
      appContext={appContext}
      hitters={team.hitters}
      pitchers={team.pitchers}
      disableManagement={disableManageTeam}
      replacePlayer={replacePlayer}
      hasTeamHeader
    />
  </TeamGridTable>;

  async function editTeam() {
    appContext.setPage({ page: 'TeamEditorPage', teamId: team.teamId });
  }

  async function replaceTeamWithCopy() {
    const response = await replaceTeamWithCopyApiClientRef.current.execute({ rosterId: rosterId, mlbPPTeam: team.powerProsTeam });
    if(response.success)
      appContext.reloadCurrentPage();
  }

  async function replaceWithNewTeam() {
    const response = await replaceTeamWithNewApiClientRef.current.execute({ rosterId: rosterId, mlbPPTeam: team.powerProsTeam });
    if(response.success)
      appContext.reloadCurrentPage();
  }

  function replaceTeamWithExisting(): void {
    appContext.openModal(closeDialog => <TeamSelectionModal 
      appContext={appContext} 
      closeDialog={teamToInsertId => { 
        closeDialog(); 
        if(!!teamToInsertId)
          executeReplaceTeam(teamToInsertId);
      }} 
    />)
  }

  function replaceWithGeneratedTeam(): void {
    appContext.openModal(closeDialog => <TeamGenerationModal 
      appContext={appContext} 
      closeDialog={teamToInsertId => { 
        closeDialog(); 
        if(!!teamToInsertId)
          executeReplaceTeam(teamToInsertId);
      }} 
    />)
  }

  async function replacePlayer(playerToReplaceId: number, playerToInsertId: number) {
    const response = await replacePlayerWithExistingApiClientRef.current.execute({ 
      teamId: team.teamId, 
      playerToReplaceId: playerToReplaceId,
      playerToInsertId: playerToInsertId
    });
    return response.success;
  }

  async function executeReplaceTeam(teamToInsertId: number) {
    const response = await replaceTeamWithExistingApiClientRef.current.execute({ 
      rosterId: rosterId,
      mlbPPTeamToReplace: team.powerProsTeam,
      teamToInsertId: teamToInsertId
    });
    if(response.success)
      appContext.reloadCurrentPage();
  }
}

const TeamGridTable = styled.table`
  width: 100%;
  border-collapse: collapse;
  isolation: isolate;
`

const TeamGridCaption = styled.caption`
  background-color: ${COLORS.primaryBlue.regular_45};
  color: ${COLORS.white.regular_100};
  position: sticky;
  top: 0;
  height: 64px;
  z-index: 1;
  padding: 8px 16px;
`

const TeamHeader = styled.h1`
  font-size: ${FONT_SIZES._32};
  font-weight: 600;
  font-style: italic;
  text-align: left;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
`

const TeamStatGrid = styled.div`
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 0px 16px;
`

const TeamStatRow = styled.div`
  display: grid;
  grid-template-columns: auto auto;
  gap: 8px;
`

const TeamStatLabel = styled.div`
  font-weight: 600;
  font-style: italic;
  text-align: left;
`

const TeamStatValue = styled.div`
  font-style: italic;
  text-align: right;
`
