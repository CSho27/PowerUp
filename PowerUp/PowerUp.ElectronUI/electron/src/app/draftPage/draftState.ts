import { replace } from "../../utils/arrayUtils";
import { PlayerDetailsResponse, toPlayerDetails } from "../teamEditor/playerDetailsResponse";
import { PlayerDetails } from "../teamEditor/playerRoleState";

export interface DraftState {
  teams: number;
  isGenerating: boolean;
  draftPool: PlayerDetails[];
  selections: number[][]; 
}

export type DraftStateAction =
| { type: 'updateTeams', teams: number }
| { type: 'startedGenerating' }
| { type: 'finishedGenerating', draftPool: PlayerDetailsResponse[] }
| { type: 'makeSelection', playerId: number }
| { type: 'undoSelection' }
| { type: 'reset' }

export function DraftStateReducer(state: DraftState, action: DraftStateAction): DraftState {
  switch(action.type) {
    case 'updateTeams':
      return {
        ...state,
        teams: action.teams,
        draftPool: [],
        selections: getInitialSelections(state.teams)
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
      const pickIndex = getPickingPlayerIndex(state.selections);
      return {
        ...state,
        selections: replace(
          state.selections, 
          (_, i) => i === pickIndex, 
          s => [...s, action.playerId]
        ) 
      }
    case 'undoSelection': 
      const lastPickIndex = getLastPickingPlayerIndex(state.selections);
      const allButLastSelection = state.selections[lastPickIndex].slice();
      allButLastSelection.pop();
        
      return {
        ...state,
        selections: replace(state.selections, (_, i) => i === lastPickIndex, () => allButLastSelection) 
      }
    case 'reset':
      return {
        ...state,
        selections: getInitialSelections(state.teams)
      }
  }
}

export function getInitialState(teams: number): DraftState {
  return {
    teams: teams,
    isGenerating: false,
    draftPool: [],
    selections: getInitialSelections(teams),
  }
}

function getInitialSelections(teams: number): number[][] {
  const initial: number[][] = [];
  for(let i=0; i<teams; i++)
    initial.push([]);

  return initial;
}

export function getPickingPlayerIndex(selections: number[][]): number {
  let firstPlayerPicks = selections[0].length
  for(let i=0; i<selections.length; i++) {
    if(selections[i].length < firstPlayerPicks)
      return i;
  }
  return 0;
}

export function getLastPickingPlayerIndex(selections: number[][]): number {
  const currentPickIndex = getPickingPlayerIndex(selections)
  return  currentPickIndex > 0
    ? currentPickIndex - 1
    : selections.length - 1;
}

export function getNextPickingPlayherIndex(selections: number[][]): number {
  const currentPickIndex = getPickingPlayerIndex(selections)
  return currentPickIndex < selections.length-1 
    ? currentPickIndex + 1
    : 0;
}

export function getRound(selections: number[][]) {
  let firstPlayerPicks = selections[0].length;
  return getPickingPlayerIndex(selections) === 0
    ? firstPlayerPicks + 1
    : firstPlayerPicks;
}