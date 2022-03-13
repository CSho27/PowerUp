import React from "react";
import styled from "styled-components";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { Icon, IconType } from "../icon/icon";

export interface ButtonProps {
  variant: ButtonVariant;
  size: ButtonSize;
  icon?: IconType;
  textAlign?: ButtonTextAlignment;
  squarePadding?: boolean;
  children?: React.ReactNode;
  onClick: () => void;
}

export type ButtonVariant = 'Fill' | 'Outline' | 'Ghost';
export type ButtonSize = 'Small' | 'Medium' | 'Large';
export type ButtonTextAlignment = 'left' | 'center' | 'right';

export function Button(props: ButtonProps) {
  const { variant, size, icon, textAlign, squarePadding, children, onClick } = props;

  const buttonContent = !icon 
    ? children
    : <IconAndTextContainer textAlign={textAlign ?? 'center' }> 
        <Icon icon={icon}/>
        {children && <span>{children}</span>}
      </IconAndTextContainer>

  switch (variant) {
    case "Fill":
      return <FillButton 
        onClick={onClick} 
        size={size} 
        textAlign={textAlign}
        squarePadding={squarePadding}
      >{buttonContent}</FillButton>;
    case "Outline":
      return <OutlineButton 
        onClick={onClick} 
        size={size} 
        textAlign={textAlign}
        squarePadding={squarePadding}
      >{buttonContent}</OutlineButton>;
    case "Ghost":
      return <GhostButton 
        onClick={onClick} 
        size={size} 
        textAlign={textAlign}
        squarePadding={squarePadding}
      >{buttonContent}</GhostButton>;
  }
};

const sizingStyles = {
  Small: `
    --horizontal-padding: 12px;
    --vertical-padding: 2px;
    --border-radius: 2px;
    --font-size: ${FONT_SIZES.default_16};
  `,
  Medium: `
    --horizontal-padding: 16px;
    --vertical-padding: 8px;
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
const BaseButton = styled.button<{ size: ButtonSize, textAlign: string | undefined, squarePadding: boolean | undefined }>`
  ${p => sizingStyles[p.size]}
  padding: var(--vertical-padding) ${p => p.squarePadding ? '' : 'var(--horizontal-padding)'};
  border: solid 1px var(--background-color);
  border-radius: var(--border-radius);
  font-weight: 500;
  font-size: var(--font-size);
  text-align: ${p => p.textAlign ?? 'undefined'};
  letter-spacing: -0.02em;
  color: var(--text-color);
  background-color: var(--background-color);
  cursor: pointer;

  &:hover {
    background-color: var(--hover-color);
  }
`;

const FillButton = styled(BaseButton)`
  --text-color: ${COLORS.white.regular_100};
  --background-color: ${COLORS.jet.regular_18};
  --hover-color: ${COLORS.jet.regular_25};
`;

const OutlineButton = styled(BaseButton)`
  --text-color: ${COLORS.primaryBlue.regular_45};
  --background-color: ${COLORS.white};
  --hover-color: ${COLORS.white.offwhite_97};
  border: solid 2px ${COLORS.primaryBlue.regular_45};
`;
const GhostButton = styled(BaseButton)`
  --text-color: ${COLORS.jet.light_39};
  --hover-color: ${COLORS.jet.light_39};

  &:hover {
    --text-color: ${COLORS.richBlack};
  }
`;

const IconAndTextContainer = styled.span<{ textAlign: string }>`
  display: flex;
  gap: 16px;
  align-items: center;
  justify-content: ${p => p.textAlign};
`