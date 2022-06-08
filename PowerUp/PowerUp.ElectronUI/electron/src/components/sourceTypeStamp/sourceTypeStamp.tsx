import styled from "styled-components";
import { EntitySourceType } from "../../app/shared/entitySourceType";
import { COLORS, FONT_SIZES } from "../../style/constants";

export type SourceTypeStampTheme = 'Light' | 'Dark';
export type SourceTypeStampSize = 'Small' | 'Medium';

export interface SourceTypeStampProps {
  theme: SourceTypeStampTheme;
  size: SourceTypeStampSize;
  sourceType: EntitySourceType;
}

export function SourceTypeStamp(props: SourceTypeStampProps) {
  const { theme, size, sourceType } = props;
  return <Stamp 
    theme={theme}
    size={size}
    title={getSourceTypeTooltip(sourceType)}>
      {sourceType}
  </Stamp>;
}

const sizingStyles = {
  Small: `
    --border-radius: 2px;
    --font-size: ${FONT_SIZES._18};
  `,
  Medium: `
    --border-radius: 2px;
    --font-size: ${FONT_SIZES._24};
  `,
}

const Stamp = styled.div<{ theme: SourceTypeStampTheme, size: SourceTypeStampSize }>`
  ${p => sizingStyles[p.size]}
  --stampThemeColor: ${p => p.theme === 'Light' ? COLORS.white.regular_100 : COLORS.richBlack.regular_5};
  font-size: var(--font-size);
  line-height: 1;
  color: var(--stampThemeColor);
  border: solid 2px var(--stampThemeColor);
  border-radius: var(--border-radius);
  cursor: default;
`

function getSourceTypeTooltip(sourceType: EntitySourceType): string {
switch(sourceType) {
  case 'Base':
    return 'Included in the original version of the game. Cannot be edited.';
  case 'Imported':
    return 'Imported from a Game Same file. Cannot be edited.';
  case 'Custom':
    return 'Created through the app. Fully editable.';
  case 'Generated':
    return 'Created using Autogeneration. Fully editable.';
}
}
