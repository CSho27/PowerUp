export type ValueGetter<TObject, TValue> = (a: TObject) => TValue;
export type SortComparator<T> = (a: T, b: T) => number;

export class SortHelpers {
  private static getComparator = <TObject, TValue>(
    fn: ValueGetter<TObject, TValue>,
    compare: SortComparator<TValue>
  ): SortComparator<TObject> => {
    return (a, b) => compare(fn(a), fn(b));
  };

  public static alphabetically = (
    a: string | null | undefined,
    b: string | null | undefined
  ): number => {
    return (a ?? '').localeCompare(b ?? '');
  };

  public static alphabeticallyDesc = (
    a: string | null | undefined,
    b: string | null | undefined
  ): number => {
    return this.alphabetically(a, b) * -1;
  };

  public static alphabeticallyBy = <T>(
    fn: ValueGetter<T, string | null | undefined>
  ): SortComparator<T> => {
    return this.getComparator(fn, this.alphabetically);
  };

  public static alphabeticallyByDesc = <T>(
    fn: ValueGetter<T, string | null | undefined>
  ): SortComparator<T> => {
    return this.getComparator(fn, this.alphabeticallyDesc);
  };

  public static numerically = (
    a: number | null | undefined,
    b: number | null | undefined
  ): number => {
    return (a ?? Number.NEGATIVE_INFINITY) - (b ?? Number.NEGATIVE_INFINITY);
  };

  public static numericallyDesc = (
    a: number | null | undefined,
    b: number | null | undefined
  ): number => {
    return this.numerically(a, b) * -1;
  };

  public static numericallyBy = <T>(
    fn: ValueGetter<T, number | null | undefined>
  ): SortComparator<T> => {
    return this.getComparator(fn, this.numerically);
  };

  public static numericallyByDesc = <T>(
    fn: ValueGetter<T, number | null | undefined>
  ): SortComparator<T> => {
    return this.getComparator(fn, this.numericallyDesc);
  };

  public static chronologically = (
    a: Date | null | undefined,
    b: Date | null | undefined
  ) => {
    return this.numerically(a?.getTime(), b?.getTime());
  };

  public static chronologicallyDesc = (
    a: Date | null | undefined,
    b: Date | null | undefined
  ) => {
    return this.numericallyDesc(a?.getTime(), b?.getTime());
  };

  public static chronologicallyBy = <T>(
    fn: ValueGetter<T, Date | null | undefined>
  ): SortComparator<T> => {
    return this.getComparator(fn, this.chronologically);
  };

  public static chronologicallyByDesc = <T>(
    fn: ValueGetter<T, Date | null | undefined>
  ): SortComparator<T> => {
    return this.getComparator(fn, this.chronologicallyDesc);
  };

  public static boolean = (a: boolean, b: boolean): number => {
    return this.numerically(+a, +b);
  };

  public static booleanDesc = (a: boolean, b: boolean): number => {
    return this.boolean(a, b) * -1;
  };

  public static booleanBy = <T>(
    fn: ValueGetter<T, boolean>
  ): SortComparator<T> => {
    return this.getComparator(fn, this.boolean);
  };

  public static booleanByDesc = <T>(
    fn: ValueGetter<T, boolean>
  ): SortComparator<T> => {
    return this.getComparator(fn, this.booleanDesc);
  };

  public static cascade = <T>(
    ...comparators: SortComparator<T>[]
  ): SortComparator<T> => {
    return (a, b) => {
      for (const comparator of comparators) {
        const result = comparator(a, b);
        if (result !== 0) return result;
      }
      return 0;
    };
  };
}
