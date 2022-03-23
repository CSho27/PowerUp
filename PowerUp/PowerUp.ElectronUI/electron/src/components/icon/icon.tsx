import * as React from 'react';

export type IconType = 
| 'arrow-up'
| 'arrow-down'
| 'arrow-left'
| 'arrow-right'
| 'box-archive'
| 'chevron-down'
| 'chevron-up'
| 'circle-notch'
| 'floppy-disk'
| 'folder-open'
| 'pen-to-square'
| 'user-pen'
| 'upload'
| 'wand-magic-sparkles'

export interface IconProps extends React.DetailedHTMLProps<React.HTMLAttributes<HTMLElement>, HTMLElement> {
  icon: IconType;
}

export function Icon(props: IconProps) {
  return <i {...props} className={`fa-solid fa-${props.icon} ${props.className}`}/>
}