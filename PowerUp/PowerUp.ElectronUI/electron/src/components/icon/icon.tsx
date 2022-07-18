import * as React from 'react';
import styled from 'styled-components';

export type IconType = 
| 'angle-right'
| 'angle-left'
| 'arrow-up'
| 'arrow-down'
| 'arrow-left'
| 'arrow-right'
| 'bars'
| 'baseball'
| 'box-archive'
| 'chevron-down'
| 'chevron-up'
| 'circle-arrow-left'
| 'circle-info'
| 'circle-minus'
| 'circle-notch'
| 'circle-plus'
| 'circle-question'
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
| 'up-right-from-square'
| 'user-pen'
| 'user-plus'
| 'wand-magic-sparkles'

export interface IconProps extends React.DetailedHTMLProps<React.HTMLAttributes<HTMLElement>, HTMLElement> {
  icon: IconType;
}

export function Icon(props: IconProps) {
  return <i {...props} className={`fa-solid fa-${props.icon} ${props.className}`}/>
}