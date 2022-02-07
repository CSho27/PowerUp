
export interface PlayerEditorState {
  firstName: string;
  lastName: string;
  savedName: string;
  playerNumber: string;
}

export type PlayerEditorAction =
| { type: 'updateFirstName', firstName: string }
| { type: 'updateLastName', lastName: string }
| { type: 'updateSavedName', savedName: string }

export function PlayerEditorStateReducer(state: PlayerEditorState, action: PlayerEditorAction): PlayerEditorState {
  switch(action.type) {
    case 'updateFirstName':
      return {
        ...state,
        firstName: action.firstName
      }
    case 'updateLastName':
      return {
        ...state,
        lastName: action.lastName
      }
    case 'updateSavedName':
      return {
        ...state,
        savedName: action.savedName
      }
  }
}