

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

function getItemsBeforeIndex<T>(items: T[], index: number): T[] {
  return items.slice(0, index);
}

function getItemsAfterIndex<T>(items: T[], index: number): T[] {
  return items.slice(index + 1);
}