

export function remove<T>(items: T[], callback: (item: T) => boolean): T[] {
  const itemToRemoveIndex = items.findIndex(callback);
  if(itemToRemoveIndex === -1)
    return items;
    
  return [
    ...getItemsBeforeIndex(items, itemToRemoveIndex),
    ...getItemsAfterIndex(items, itemToRemoveIndex)
  ];
}

export function replace<T>(items: T[], callback: (item: T) => boolean, getReplacement: (item: T) => T): T[] {
  const itemToReplaceIndex = items.findIndex(callback);
  if(itemToReplaceIndex === -1)
    return items;

  const itemToReplace = items[itemToReplaceIndex];
  return [
    ...getItemsBeforeIndex(items, itemToReplaceIndex),
    getReplacement(itemToReplace), 
    ...getItemsAfterIndex(items, itemToReplaceIndex)
  ];
}

export function insert<T>(items: T[], item: T, index: number, insertAfter?: boolean): T[] {
  const itemPreviouslyAtIndex = items[index];
  return [
    ...getItemsBeforeIndex(items, index),
    ...insertIf(item, !insertAfter),
    ...insertIf(itemPreviouslyAtIndex, !!itemPreviouslyAtIndex),
    ...insertIf(item, !!insertAfter),
    ...getItemsAfterIndex(items, index)
  ]  
}

export function insertIf<T>(item: T, condition: boolean) {
  return condition
    ? [item]
    : [];
}

function getItemsBeforeIndex<T>(items: T[], index: number): T[] {
  return items.slice(0, index);
}

function getItemsAfterIndex<T>(items: T[], index: number): T[] {
  return items.slice(index + 1);
}

export function distinctBy<T, V>(items: T[], callback: (item: T) => V) {
  return items.filter((value, index) => items.findIndex(i => callback(i) === callback(value)) === index)
}