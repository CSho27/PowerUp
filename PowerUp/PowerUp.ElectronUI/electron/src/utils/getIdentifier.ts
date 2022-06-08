export type EntityType = 'Roster' | 'Team' | 'Player';

export function toIdentifier(type: EntityType, id: number): string {
  return `${getPrefixForEntityType(type)}${id}`;
}

function getPrefixForEntityType(type: EntityType): string {
  switch(type) {
    case 'Roster':
      return 'R';
    case 'Team':
      return 'T';
    case 'Player':
      return 'P';
  }
}