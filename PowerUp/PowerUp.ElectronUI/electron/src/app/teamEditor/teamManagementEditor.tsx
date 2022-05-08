import { Dispatch } from "react";
import { AppContext } from "../app";
import { TeamEditorDetailsAction } from "./teamEditorState";
import { PlayerRoleState } from "./playerRoleState";
import { TeamManagementGrid } from "./teamManagementGrid";

export interface TeamManagementEditorProps {
  appContext: AppContext;
  mlbPlayers: PlayerRoleState[];
  aaaPlayers: PlayerRoleState[];
  disabled: boolean;
  update: Dispatch<TeamEditorDetailsAction>;
  saveTemp: () => void;
}

export function TeamManagementEditor(props: TeamManagementEditorProps) {
  const { appContext, mlbPlayers, aaaPlayers, disabled, update, saveTemp } = props;
  
  return <>
    <TeamManagementGrid 
      appContext={appContext}
      isAAA={false}
      mlbPlayers={mlbPlayers}
      aaaPlayers={aaaPlayers}
      startingNumber={1}
      canManageRoster={!disabled}
      canEditRoles={!disabled}
      canSendUpOrDown={true}
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
      canManageRoster={!disabled}
      canEditRoles={false}
      canSendUpOrDown={mlbPlayers.length < 25}
      updatePlayer={(id, action) => update({ type: 'updateAAAPlayer', playerId: id, roleAction: action }) } 
      sendUpOrDown={id => update({ type: 'sendUp', playerId: id })} 
      addPlayer={player => update({ type: 'addAAAPlayer', playerDetais: player }) } 
      saveTempTeam={saveTemp} />
  </>
}