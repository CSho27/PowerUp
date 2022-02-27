
export type Position = 
| 'Pitcher'
| 'Catcher'
| 'FirstBase'
| 'SecondBase'
| 'ThirdBase'
| 'Shortstop'
| 'LeftField'
| 'CenterField'
| 'RightField'
| 'DesignatedHitter'

export type PositionType = 
| 'Catcher' 
| 'Infielder' 
| 'Outfielder' 
| 'Pitcher'

export interface PositionCode {
  key: Position;
  name: string;
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