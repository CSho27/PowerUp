import { useEffect } from "react";
import { initialKeyBindingState, KeyBinding, KeyBindingStateReducer, processKeyBindings } from "./keyBinding";
import { handleNativeKeyboardEvent, NimbleKeyboardEvent } from "./nimbleKeyboardEvent";

let state = initialKeyBindingState;
let bindings: KeyBinding[] = [];
export function useGlobalBindings(...bindings: KeyBinding[]) {
  useEffect(() => {
    addGlobalBindings(bindings);
  }, [])
}

function addGlobalBindings(newBindings: KeyBinding[]) {
  if(bindings.length === 0) {
    window.addEventListener('keydown', handleNativeKeyboardEvent(handleKeyDown));
    window.addEventListener('keyup', handleNativeKeyboardEvent(handleKeyUp));
  }

  bindings = [...bindings, ...newBindings];
}

function handleKeyDown(event: NimbleKeyboardEvent) {
  processKeyBindings(bindings, state, event);
  state = KeyBindingStateReducer(state, { type: 'updateFromKeyDown', key: event.key });
}

function handleKeyUp(event: NimbleKeyboardEvent) {
  state = KeyBindingStateReducer(state, { type: 'updateFromKeyUp', key: event.key });
}