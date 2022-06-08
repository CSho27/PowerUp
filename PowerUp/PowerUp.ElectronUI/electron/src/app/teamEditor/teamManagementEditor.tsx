import { Dispatch } from "react";
import { AppContext } from "../app";
import { TeamEditorDetailsAction } from "./teamEditorState";
import { PlayerRoleState } from "./playerRoleState";
import { TeamManagementGrid } from "./teamManagementGrid";
import { DisabledCriteria, DisabledCriterion } from "../../utils/disabledProps";

export interface TeamManagementEditorProps {
  appContext: AppContext;
  mlbPlayers: PlayerRoleState[];
  aaaPlayers: PlayerRoleState[];
  disabled: DisabledCriteria;
  update: Dispatch<TeamEditorDetailsAction>;
  saveTemp: () => void;
}

export function TeamManagementEditor(props: TeamManagementEditorProps) {
  const { appContext, mlbPlayers, aaaPlayers, disabled, update, saveTemp } = props;
  
  const aaaRolesDisabled: DisabledCriteria = [
    ...disabled,
    { isDisabled: true, tooltipIfDisabled: "AAA players' team roles cannot be edited" }
  ]

  const aaaDisableCallUp: DisabledCriteria = [
    ...disabled,
    { isDisabled: mlbPlayers.length >= 25, tooltipIfDisabled: 'Cannot be called up because there are already 25 players on the MLB roster.'}
  ]

  return <>
    <TeamManagementGrid 
      appContext={appContext}
      isAAA={false}
      mlbPlayers={mlbPlayers}
      aaaPlayers={aaaPlayers}
      startingNumber={1}
      disableManageRoster={disabled}
      disableEditRoles={disabled}
      disableSendUpOrDown={disabled}
      updatePlayer={(id, action) => update({ type: 'updateMLBPlayer', playerId: id, roleAction: action }) } 
      sendUpOrDown={id => update({ type: 'sendDown', playerId: id }) } 
      addPlayer={player => update({ type: 'addMLBPlayer', playerDetais: player }) }
      saveTempTeam={saveTemp} />
    <TeamManagementGrid 
      appContext={appContext}
      isAAA={true} 
      mlbPlayers={mlbPlayers}
      aaaPlayers={aaaPlayers}
      startingNumber={mlbPlayers.length+1}
      disableManageRoster={disabled}
      disableEditRoles={aaaRolesDisabled}
      disableSendUpOrDown={aaaDisableCallUp}
      updatePlayer={(id, action) => update({ type: 'updateAAAPlayer', playerId: id, roleAction: action }) } 
      sendUpOrDown={id => update({ type: 'sendUp', playerId: id })} 
      addPlayer={player => update({ type: 'addAAAPlayer', playerDetais: player }) } 
      saveTempTeam={saveTemp} />
  </>
}