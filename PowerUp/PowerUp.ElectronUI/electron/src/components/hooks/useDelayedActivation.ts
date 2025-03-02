import { useState } from "react";
import { useDebounceEffect } from "./useDebounceEffect";

export function useDelayedActivation(active: boolean, delay: number): boolean {
  const [delayedActive, setDelayedActive] = useState(active);
  useDebounceEffect(() => setDelayedActive(active), delay, [active, delay]);
  return active && delayedActive;
}