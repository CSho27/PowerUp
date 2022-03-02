import { useReducer, useRef } from "react";

export  type ReducerWithActionAndContext<TState, TAction, TContext> = (state: TState, action: TAction, context: TContext) => TState; 

export function useReducerWithContext<TState, TAction, TContext>(reducerWithContext: ReducerWithActionAndContext<TState,TAction,TContext>, initialState: TState, context: TContext) {
  const contextRef = useRef<TContext>(context);
  const reducer = (state: TState, action: TAction) => reducerWithContext(state, action, contextRef.current);
  return useReducer(reducer, initialState);
}