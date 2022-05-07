import { Dispatch } from "react";
import { AppContext } from "../app";
import { TeamManagementEditorAction, TeamManagementEditorState } from "./teamManagementEditorState"
import { TeamManagementGrid } from "./teamManagementGrid";

export interface TeamManagementEditorProps {
  appContext: AppContext;
  teamId: number;
  state: TeamManagementEditorState;
  update: Dispatch<TeamManagementEditorAction>;   
}

export function TeamManagementEditor(props: TeamManagementEditorProps) {
  const { appContext, teamId, state, update } = props;
  
  return <>
    <TeamManagementGrid 
      appContext={appContext}
      teamId={teamId}
      gridTitle='MLB' 
      players={state.mlbPlayers}
      updatePlayer={(id, action) => update({ type: 'updateMLBPlayer', playerId: id, roleAction: action }) } />
    <TeamManagementGrid 
      appContext={appContext}
      teamId={teamId}
      gridTitle='AAA' 
      players={state.aaaPlayers}
      updatePlayer={(id, action) => update({ type: 'updateAAAPlayer', playerId: id, roleAction: action }) }/>
  </>

  
}