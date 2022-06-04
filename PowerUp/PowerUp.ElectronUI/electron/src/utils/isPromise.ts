export function isPromise<T>(obj: T | Promise<T>): obj is Promise<T> {
  const p = obj as any;
  return p !== null &&
    p instanceof Object &&
    p.then instanceof Function &&
    p.catch instanceof Function;
}
