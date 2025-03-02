import { DependencyList, EffectCallback, useEffect } from "react";

export type AsyncEffectCallback = (
  signal: AbortSignal
) => Promise<ReturnType<EffectCallback>>;

export function useAsyncEffect(
  effect: AsyncEffectCallback,
  deps?: DependencyList
) {
  useEffect(() => {
    const abortController = new AbortController();
    const { signal } = abortController;
    effect(signal).catch(error => {
      if (error.name !== 'AbortError') {
        console.error('An unexpected error occurred:', error);
      }
    });
    return () => abortController.abort();
  }, deps);
}