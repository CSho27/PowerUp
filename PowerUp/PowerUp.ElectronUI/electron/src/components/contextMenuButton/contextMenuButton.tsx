import { Menu, MenuButton, MenuItem, MenuList } from "@reach/menu-button";
import { PropsWithChildren, ReactElement } from "react";
import styled from "styled-components";
import { COLORS } from "../../style/constants";
import { ButtonContent, ButtonContentProps,  } from "../button/button";
import { Icon, IconType } from "../icon/icon";
import './contextMenuButton.css';

export interface ContextMenuProps extends ButtonContentProps {
  menuItems: ReactElement<ContextMenuItemProps> | ReactElement<ContextMenuItemProps>[];
}

export function ContextMenuButton(props: ContextMenuProps) {
  const { menuItems, children, disabled, title } = props;

  return <Menu>
    {({ isExpanded }) => <>
    <MenuButton 
      disabled={disabled}
      title={title}
      style={{
        padding: 0,
        border: 'none',
        backgroundColor: 'inherit'
      }}>
        <ButtonContent {...props}>
          {children}
        </ButtonContent>
    </MenuButton>
    {isExpanded &&
    <MenuList style={{ 
      backgroundColor: COLORS.jet.superlight_90,
      boxShadow: '0px 3px 16px -8px', 
      border: `1px solid ${COLORS.jet.light_39}`
    }}>
      {menuItems}
    </MenuList>}
    </>}
  </Menu>
}

export interface ContextMenuItemProps {
  icon?: IconType;
  disabled?: boolean;
  noPadding?: boolean;
  onClick: () => void;
}

export function ContextMenuItem(props: PropsWithChildren<ContextMenuItemProps>) {
  const { icon, disabled, noPadding, onClick, children } = props;
  
  return <MenuItem onSelect={onClick} disabled={disabled}>
    <ItemWrapper noPadding={!!noPadding}>
    {icon && <IconGutter>
      <Icon icon={icon!} />
    </IconGutter>}
    <ContentWrapper>
      {children}
    </ContentWrapper>
    </ItemWrapper>
  </MenuItem>
}

const ItemWrapper = styled.div<{ noPadding: boolean }>`
  display: flex;
  align-items: center;
  justify-content: space-evenly;
  gap: 4px;
  padding: ${p => p.noPadding ? 'undefined' : '4px 8px' };
  color: ${COLORS.primaryBlue.dark_23};
`

const IconGutter = styled.div`
  flex: 0 0 16px;
  display: flex;
  align-items: center;
  justify-content: center;
`

const ContentWrapper = styled.div`
  flex: 1 0 auto;
`