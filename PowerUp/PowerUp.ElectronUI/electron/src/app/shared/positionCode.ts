

const positions = [
  'Pitcher',
  'Catcher',
  'FirstBase',
  'SecondBase',
  'ThirdBase',
  'Shortstop',
  'LeftField',
  'CenterField',
  'RightField',
  'DesignatedHitter'
] as const
export type Position = typeof positions[number];
export function isPosition(val: string): val is Position {
  return positions.some(p => p === val);
}

export type PositionType = 
| 'Catcher' 
| 'Infielder' 
| 'Outfielder' 
| 'Pitcher'

export interface PositionCode {
  key: Position;
  name: string;
}

export function getPositionAbbreviation(position: Position): string {
  switch(position) {
    case 'Pitcher':           return 'P';
    case 'Catcher':           return 'C';
    case 'FirstBase':         return '1B';
    case 'SecondBase':        return '2B';
    case 'ThirdBase':         return '3B';
    case 'Shortstop':         return 'SS';
    case 'LeftField':         return 'LF';
    case 'CenterField':       return 'CF';
    case 'RightField':        return 'RF';
    case 'DesignatedHitter':  return 'DH';
  }
}

export function getPositionType(position: Position): PositionType {
  switch(position) {
    case 'Catcher': 
      return 'Catcher';
    case 'Pitcher':
      return 'Pitcher';
    case 'FirstBase':
    case 'SecondBase':
    case 'ThirdBase':
    case 'Shortstop':
      return 'Infielder';
    case 'LeftField':
    case 'CenterField':      
    case 'RightField':
    case 'DesignatedHitter':
      return 'Outfielder';
  }
}

export function getPositionNumber(position: Position): number {
  switch(position) {
    case 'Pitcher':           return 1;
    case 'Catcher':           return 2;
    case 'FirstBase':         return 3;
    case 'SecondBase':        return 4;
    case 'ThirdBase':         return 5;
    case 'Shortstop':         return 6;
    case 'LeftField':         return 7;
    case 'CenterField':       return 8;
    case 'RightField':        return 9;
    case 'DesignatedHitter':  return 10;
  }
}

export function positionCompare(position1: Position, position2: Position): number {
  if(position1 === 'Pitcher' && position2 === 'Pitcher')
    return 0;
  else if(position1 === 'Pitcher')
    return 1;
  else if(position2 === 'Pitcher')
    return -1
  else
    return getPositionNumber(position1) - getPositionNumber(position2);
}