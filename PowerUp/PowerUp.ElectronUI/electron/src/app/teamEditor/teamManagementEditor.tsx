import { Dispatch } from "react";
import { AppContext } from "../app";
import { TeamManagementEditorAction, TeamManagementEditorState } from "./teamManagementEditorState"
import { TeamManagementGrid } from "./teamManagementGrid";

export interface TeamManagementEditorProps {
  appContext: AppContext;
  teamId: number;
  state: TeamManagementEditorState;
  disabled: boolean;
  update: Dispatch<TeamManagementEditorAction>;   
}

export function TeamManagementEditor(props: TeamManagementEditorProps) {
  const { appContext, teamId, state, disabled, update } = props;
  
  return <>
    <TeamManagementGrid 
      appContext={appContext}
      isAAA={false}
      players={state.mlbPlayers}
      startingNumber={1}
      canManageRoster={!disabled}
      canEditRoles={!disabled}
      canSendUpOrDown={true}
      updatePlayer={(id, action) => update({ type: 'updateMLBPlayer', playerId: id, roleAction: action }) } 
      sendUpOrDown={id => update({ type: 'sendDown', playerId: id }) } />
    <TeamManagementGrid 
      appContext={appContext}
      isAAA={true} 
      players={state.aaaPlayers}
      startingNumber={state.mlbPlayers.length+1}
      canManageRoster={!disabled}
      canEditRoles={false}
      canSendUpOrDown={state.mlbPlayers.length < 25}
      updatePlayer={(id, action) => update({ type: 'updateAAAPlayer', playerId: id, roleAction: action }) } 
      sendUpOrDown={id => update({ type: 'sendUp', playerId: id })} />
  </>

  
}