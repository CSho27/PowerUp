import * as React from 'react';

export type IconType = 
| 'arrow-up'
| 'box-archive'
| 'circle-notch'
| 'folder-open'
| 'pen-to-square'
| 'upload'
| 'wand-magic-sparkles'

export interface IconProps extends React.DetailedHTMLProps<React.HTMLAttributes<HTMLElement>, HTMLElement> {
  icon: IconType;
}

export function Icon(props: IconProps) {
  return <i {...props} className={`fa-solid fa-${props.icon} ${props.className}`}/>
}