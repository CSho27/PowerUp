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
      gridTitle='MLB' 
      players={state.mlbPlayers}
      canManageRoster={!disabled}
      canEditRoles={!disabled}
      updatePlayer={(id, action) => update({ type: 'updateMLBPlayer', playerId: id, roleAction: action }) } />
    <TeamManagementGrid 
      appContext={appContext}
      gridTitle='AAA' 
      players={state.aaaPlayers}
      canManageRoster={!disabled}
      canEditRoles={false}
      updatePlayer={(id, action) => update({ type: 'updateAAAPlayer', playerId: id, roleAction: action }) }/>
  </>

  
}