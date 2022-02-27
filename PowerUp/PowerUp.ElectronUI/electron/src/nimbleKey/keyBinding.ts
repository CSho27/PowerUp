import { KeyboardKey, keyEquals } from "./keyboardKey";
import { NimbleKeyboardEvent } from "./nimbleKeyboardEvent";

/** A combination of pressed keys that triggers a callback function */
export interface KeyBinding {
  keys: KeyboardKey[];
  callbackFn: () => void;
}

export function processKeyBindings(bindings: KeyBinding[], state: KeyBindingState, event: NimbleKeyboardEvent) {
  const pressedKeys = updatePressedKeys(state.pressedKeys, event.key);

  bindings.forEach(b => {
    if(shouldExecuteBinding(b.keys, pressedKeys)){
      b.callbackFn();
      event.stopPropogation();
      event.preventDefault();
    }
  });
}

function shouldExecuteBinding(bindingKeys: KeyboardKey[], pressedKeys: KeyboardKey[]): boolean {
  return bindingKeys.every((k, i) => keyEquals(k, pressedKeys[i]));
}

export interface KeyBindingState {
  pressedKeys: KeyboardKey[];
}

export type KeyBindingAction =
| { type: 'updateFromKeyDown', key: KeyboardKey }
| { type: 'updateFromKeyUp', key: KeyboardKey }

export function KeyBindingStateReducer(state: KeyBindingState, action: KeyBindingAction): KeyBindingState {
  switch(action.type) {
    case 'updateFromKeyDown':
      return {
        ...state,
        pressedKeys: updatePressedKeys(state.pressedKeys, action.key)
      }
    case 'updateFromKeyUp':
      return {
        ...state,
        pressedKeys: state.pressedKeys.filter(k => k !== action.key)
      }
  }
}

export function updatePressedKeys(pressedKeys: KeyboardKey[], newKey: KeyboardKey): KeyboardKey[] {
  return pressedKeys.some(k => keyEquals(k, newKey))
  ? pressedKeys
  : [...pressedKeys, newKey];
}

export const initialKeyBindingState: KeyBindingState = { pressedKeys: [] }; 