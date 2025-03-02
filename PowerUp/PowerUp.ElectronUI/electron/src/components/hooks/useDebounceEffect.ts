import { DependencyList, EffectCallback, useEffect } from "react";

export function useDebounceEffect(callback: EffectCallback, timeout: number, dependencyList?: DependencyList) {
  useEffect(() => {
    let destructor: void | (() => void);
    const timeoutId = setTimeout(() => {
      destructor = callback();
    }, timeout);

    return () => {
      if(!!destructor)
        destructor();
      clearTimeout(timeoutId)
    }
  }, dependencyList);
}