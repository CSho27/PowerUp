import { useReducer, useState } from "react";
import { initialKeyBindingState, KeyBinding, KeyBindingStateReducer, processKeyBindings } from "./keyBinding";
import { isPrintableKey, KeyboardKey, keyEquals } from "./keyboardKey";
import { handleKeyboardEvent, NimbleKeyboardEvent } from "./nimbleKeyboardEvent";

export interface KeyBindingContextProps {
  keyBindings: KeyBinding[];
  children(provided: KeyBindingProvided): React.ReactNode;
}

export interface KeyBindingProvided {
  onKeyDown: React.KeyboardEventHandler<HTMLElement>;
  onKeyUp: React.KeyboardEventHandler<HTMLElement>;
}

/** React context, inside of which key bindings take effect */
export function KeybindingContext(props: KeyBindingContextProps) {
  const { keyBindings, children } = props;
  const [state, update] = useReducer(KeyBindingStateReducer, initialKeyBindingState);

  const provided: KeyBindingProvided = {
    onKeyDown: handleKeyboardEvent(handleKeyDown),
    onKeyUp: handleKeyboardEvent(handleKeyUp)
  }

  return <>
    {children(provided)}
  </>

  function handleKeyDown(event: NimbleKeyboardEvent) {
    processKeyBindings(keyBindings, state, event);
    update({ type: 'updateFromKeyDown', key: event.key});
  }

  function handleKeyUp(event: NimbleKeyboardEvent) {
    update({ type: 'updateFromKeyUp', key: event.key });
  }
}