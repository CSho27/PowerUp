import { DependencyList } from "react";
import { AsyncEffectCallback } from "./useAsyncEffect";
import { useDebounceEffect } from "./useDebounceEffect";

export function useAsyncDebounceEffect(
  effect: AsyncEffectCallback,
  ms: number,
  deps: DependencyList
) {
  useDebounceEffect(
    () => {
      const abortController = new AbortController();
      const { signal } = abortController;
      effect(signal).catch(error => {
        if (error.name !== 'AbortError') console.error(error);
      });
      return () => abortController.abort();
    },
    ms,
    deps
  );
}