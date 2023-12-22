import { replace } from "../../utils/arrayUtils";
import { PlayerDetailsResponse, toPlayerDetails } from "../teamEditor/playerDetailsResponse";
import { PlayerDetails } from "../teamEditor/playerRoleState";

export interface DraftState {
  numberOfTeams: number;
  isGenerating: boolean;
  draftPool: PlayerDetails[];
  teams: DraftedTeam[]; 
}

export interface DraftedTeam {
  name: string;
  selections: number[];
}

export type DraftStateAction =
| { type: 'updateTeams', teams: number }
| { type: 'startedGenerating' }
| { type: 'finishedGenerating', draftPool: PlayerDetailsResponse[] }
| { type: 'makeSelection', playerId: number }
| { type: 'updateTeamName', teamIndex: number, name: string }
| { type: 'undoSelection' }
| { type: 'reset' }

export function DraftStateReducer(state: DraftState, action: DraftStateAction): DraftState {
  switch(action.type) {
    case 'updateTeams':
      return {
        ...state,
        numberOfTeams: action.teams,
        draftPool: [],
        teams: getInitialSelections(state.numberOfTeams)
      }

    case 'startedGenerating':
      return {
        ...state,
        isGenerating: true,
      }
    case 'finishedGenerating':
      return {
        ...state,
        isGenerating: false,
        draftPool: action.draftPool.map(toPlayerDetails)
      }
    case 'makeSelection':
      const pickIndex = getPickingPlayerIndex(state.teams);
      return {
        ...state,
        teams: replace(
          state.teams, 
          (_, i) => i === pickIndex, 
          s => ({ ...s, selections: [...s.selections, action.playerId] }) 
        ) 
      }
    case 'updateTeamName':
      return {
        ...state,
        teams: replace(
          state.teams, 
          (_, i) => i === action.teamIndex, 
          s => ({ ...s, name: action.name }) 
        ) 
      }
    case 'undoSelection': 
      const lastPickIndex = getLastPickingPlayerIndex(state.teams);
      const allButLastSelection = state.teams[lastPickIndex].selections.slice();
      allButLastSelection.pop();
        
      return {
        ...state,
        teams: replace(
          state.teams, 
          (_, i) => i === lastPickIndex, 
          s => ({ ...s, selections: allButLastSelection })
        ) 
      }
    case 'reset':
      return {
        ...state,
        teams: getInitialSelections(state.numberOfTeams)
      }
  }
}

export function getInitialState(teams: number): DraftState {
  return {
    numberOfTeams: teams,
    isGenerating: false,
    draftPool: [],
    teams: getInitialSelections(teams),
  }
}

function getInitialSelections(teams: number): DraftedTeam[] {
  const initial: DraftedTeam[] = [];
  for(let i=0; i<teams; i++) {
    initial.push({
      name: '',
      selections: []
    });
  }

  return initial;
}

export function getPickingPlayerIndex(teams: DraftedTeam[]): number {
  let firstPlayerPicks = teams[0].selections.length
  for(let i=0; i<teams.length; i++) {
    if(teams[i].selections.length < firstPlayerPicks)
      return i;
  }
  return 0;
}

export function getLastPickingPlayerIndex(teams: DraftedTeam[]): number {
  const currentPickIndex = getPickingPlayerIndex(teams)
  return  currentPickIndex > 0
    ? currentPickIndex - 1
    : teams.length - 1;
}

export function getNextPickingPlayherIndex(teams: DraftedTeam[]): number {
  const currentPickIndex = getPickingPlayerIndex(teams)
  return currentPickIndex < teams.length-1 
    ? currentPickIndex + 1
    : 0;
}

export function getRound(teams: DraftedTeam[]) {
  let firstPlayerPicks = teams[0].selections.length;
  return getPickingPlayerIndex(teams) === 0
    ? firstPlayerPicks + 1
    : firstPlayerPicks;
}