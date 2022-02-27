import React from "react";
import { getKeyType, isAlphanumericKey, isPrintableKey, KeyboardKey, KeyboardKeyType } from "./keyboardKey";
import { KeyboardLocation } from "./keyboardLocation";

export interface NimbleKeyboardEvent {
  nativeEvent: KeyboardEvent;
  target: EventTarget | null;
  key: KeyboardKey;
  keyType: KeyboardKeyType;
  isAlphanumeric: boolean;
  isPrintable: boolean;
  location: KeyboardLocation;
  locale: string | undefined;
  altKey: boolean;
  ctrlKey: boolean;
  shiftKey: boolean;
  metaKey: boolean;
  capsLock: boolean;
  numLock: boolean;
  symbolLock: boolean;
  repeat: boolean;
  preventDefault: () => void;
  stopPropogation: () => void;
}
export type NimbleKeyboardEventHandler= (event: NimbleKeyboardEvent) => void;

export function handleKeyboardEvent<T = Element>(handler: NimbleKeyboardEventHandler): React.KeyboardEventHandler<T> {
  return (event: React.KeyboardEvent<T>) => handler(toNimbleKeyboardEvent(event));
}

export function handleNativeKeyboardEvent(handler: NimbleKeyboardEventHandler): (event: KeyboardEvent) => void {
  return (event: KeyboardEvent) => handler(toNimbleKeyboardEventFromNative(event));
}

export function toNimbleKeyboardEvent<T = Element>(event: React.KeyboardEvent<T>): NimbleKeyboardEvent {
  return toNimbleKeyboardEventFromNative(event.nativeEvent);
}

export function toNimbleKeyboardEventFromNative(event: KeyboardEvent): NimbleKeyboardEvent {
  return {
    nativeEvent: event,
    target: event.target,
    key: event.key as KeyboardKey,
    keyType: getKeyType(event.key),
    isAlphanumeric: isAlphanumericKey(event.key),
    isPrintable: isPrintableKey(event.key),
    location: event.location,
    locale: (event as any).locale,
    altKey: event.altKey,
    ctrlKey: event.ctrlKey,
    shiftKey: event.shiftKey,
    metaKey: event.metaKey,
    capsLock: event.getModifierState('CapsLock'),
    numLock: event.getModifierState('NumLock'),
    symbolLock: event.getModifierState('SymbolLock'),
    repeat: event.repeat,
    preventDefault: () => event.preventDefault(),
    stopPropogation: () => event.stopPropagation()
  }
}
