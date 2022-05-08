import * as React from 'react';

export type IconType = 
| 'arrow-up'
| 'arrow-down'
| 'arrow-left'
| 'arrow-right'
| 'bars'
| 'box-archive'
| 'chevron-down'
| 'chevron-up'
| 'circle-minus'
| 'circle-notch'
| 'copy'
| 'download'
| 'eye'
| 'file'
| 'floppy-disk'
| 'folder'
| 'folder-open'
| 'lock'
| 'pen-to-square'
| 'person-arrow-down-to-line'
| 'person-arrow-up-from-line'
| 'plus'
| 'right-left'
| 'rotate-left'
| 'square'
| 'upload'
| 'user-pen'
| 'user-plus'
| 'wand-magic-sparkles'

export interface IconProps extends React.DetailedHTMLProps<React.HTMLAttributes<HTMLElement>, HTMLElement> {
  icon: IconType;
}

export function Icon(props: IconProps) {
  return <i {...props} className={`fa-solid fa-${props.icon} ${props.className}`}/>
}