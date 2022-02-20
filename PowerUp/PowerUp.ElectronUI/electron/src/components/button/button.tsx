import React from "react";
import styled from "styled-components";
import { COLORS, FONT_SIZES } from "../../style/constants";

export interface ButtonProps {
  variant: ButtonVariant;
  size: ButtonSize;
  children?: React.ReactNode;
  onClick: () => void;
}

export type ButtonVariant = 'Fill' | 'Outline' | 'Ghost';
export type ButtonSize = 'Small' | 'Medium' | 'Large';

export function Button(props: ButtonProps) {
  const { variant, size, children, onClick } = props;

  switch (variant) {
    case "Fill":
      return <FillButton onClick={onClick} size={size}>{children}</FillButton>;
    case "Outline":
      return <OutlineButton onClick={onClick} size={size}>{children}</OutlineButton>;
    case "Ghost":
      return <GhostButton onClick={onClick} size={size}>{children}</GhostButton>;
    default:
      return <BaseButton onClick={onClick} size={size}>{children}</BaseButton>;
  }
};

const sizingStyles = {
  Small: `
    --horizontal-padding: 12px;
    --vertical-padding: 4px;
    --border-radius: 2px;
    --font-size: ${FONT_SIZES.default_16};
  `,
  Medium: `
    --horizontal-padding: 20px;
    --vertical-padding: 12px;
    --border-radius: 2px;
    --font-size: ${FONT_SIZES._18};
  `,
  Large: `
    --horizontal-padding: 32px;
    --vertical-padding: 16px;
    --border-radius: 4px;
    --font-size: ${FONT_SIZES._36};
  `
}


// Default is fill button
const BaseButton = styled.button<{ size: ButtonSize }>`
  ${p => sizingStyles[p.size]}
  padding: var(--vertical-padding) var(--horizontal-padding);
  border-style: none;
  border-radius: var(--border-radius);
  font-weight: 500;
  font-size: var(--font-size);
  letter-spacing: -0.02em;
  text-transform: uppercase;
  color: var(--text-color);
  background-color: var(--background-color);

  &:focus {
    outline: solid var(--focus-border-color);
    outline-offset: 2px;
  }

  &:hover {
    background-color: var(--hover-color);
  }
`;

const FillButton = styled(BaseButton)`
  --text-color: ${COLORS.white};
  --background-color: ${COLORS.primaryBlue.regular_45};
  --focus-border-color: ${COLORS.primaryBlue.regular_45};
  --hover-color: ${COLORS.primaryBlue.light_50};
`;

const OutlineButton = styled(BaseButton)`
  --text-color: ${COLORS.primaryBlue.regular_45};
  --background-color: ${COLORS.white};
  --focus-border-color: ${COLORS.primaryBlue.regular_45};
  --hover-color: ${COLORS.white.offwhite_97};
  border: solid 2px ${COLORS.primaryBlue.regular_45};
`;
const GhostButton = styled(BaseButton)`
  --text-color: ${COLORS.jet.light_39};
  --focus-border-color: ${COLORS.jet.light_39};
  --hover-color: ${COLORS.jet.light_39};

  &:hover {
    --text-color: ${COLORS.richBlack};
  }
`;