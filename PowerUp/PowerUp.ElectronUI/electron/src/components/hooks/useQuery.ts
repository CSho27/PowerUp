import { DependencyList, useReducer } from "react";
import { useAsyncDebounceEffect } from "./useAsyncDebounceEffect";

export interface UseQueryParams<TData> {
  queryFn: () => Promise<TData>;
  initialData?: TData;
  debounce?: number;
}

export interface UseQueryReturn<TData> {
  isLoading: boolean;
  isFetching: boolean;
  data: TData | null;
  refetch: () => Promise<TData | null>;
}

interface QueryState<TData> {
  status: 'loading' | 'fetching' | 'idle'
  data: TData | null;
}

type QueryAction<TData> =
| { type: 'fetched', data: TData }
| { type: 'failed' }
| { type: 'refetch' }

function QueryReducer<TData>(state: QueryState<TData>, action: QueryAction<TData>): QueryState<TData> {
  switch(action.type) {
    case 'fetched':
      return {
        ...state,
        status: 'idle',
        data: action.data,
      }
    case 'failed':
      return {
        ...state,
        status: 'idle',
        data: null
      }
    case 'refetch': 
      return {
        ...state,
        status: 'fetching'
      }
  }
}

export function useQuery<TData>({ queryFn, initialData }: UseQueryParams<TData>, deps: DependencyList): UseQueryReturn<TData> {
  const initialState: QueryState<TData> = {
    data: initialData ?? null,
    status: 'loading'
  };
  const [state, update] = useReducer(QueryReducer<TData>, initialState);

  useAsyncDebounceEffect(async abort => {
    performQuery(abort);
  }, 0, deps)

  return {
    data: state.data,
    isLoading: state.status === 'loading',
    isFetching: state.status === 'loading' || state.status === 'fetching',
    refetch: async () => performQuery(new AbortSignal())
  }

  function performQuery(abort: AbortSignal): Promise<TData | null> {
    return queryFn()
      .then(data => {
        if(abort.aborted) return null;
        update({ type: 'fetched', data: data });
        return data;
      })
      .catch(error => {
        console.error(error)
        if(abort.aborted) return null;
        update({ type: 'failed' });
        return null;
      });
  }
}