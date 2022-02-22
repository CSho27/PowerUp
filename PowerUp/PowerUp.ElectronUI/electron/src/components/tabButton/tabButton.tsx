import styled from "styled-components"
import { KeyedCode } from "../../app/home/importBaseRosterApiClient"
import { COLORS, FONT_SIZES } from "../../style/constants"

export interface TabButtonNavProps {
  selectedTab: KeyedCode;
  tabOptions: KeyedCode[];
  onChange: (selectedTab: KeyedCode) => void;
}

export function TabButtonNav(props: TabButtonNavProps) {
  return <nav>
    <TabButtonListWrapper>
      {props.tabOptions.map(o => <TabButton 
        text={o.name} 
        isSelected={props.selectedTab === o} 
        onClick={() => o.key != props.selectedTab.key && props.onChange(o)}
      />)}
    </TabButtonListWrapper>
  </nav>
}

interface TabButtonProps {
  text: string;
  isSelected: boolean;
  onClick: () => void;
}


function TabButton(props: TabButtonProps) {
  return <TabButtonWrapper isSelected={props.isSelected} onClick={props.onClick}>
    {props.text}
  </TabButtonWrapper>
}

const TabButtonListWrapper = styled.ul`
  padding: 0;
  margin: 0;
  list-style-type: none;
  white-space: nowrap;
  display: flex;
  gap: 4px;
  align-items: baseline;
`

const TabButtonWrapper = styled.li<{ isSelected: boolean }>`
  width: 128px;
  padding: 0px 2px;
  font-size: ${FONT_SIZES._18};
  background-color: ${p => p.isSelected ? COLORS.primaryBlue.dark_23 : COLORS.jet.lighter_71};
  color: ${p => p.isSelected ? COLORS.white.regular_100 : COLORS.primaryBlue.dark_23};
  border-radius: 4px;
  text-align: center;
  cursor: pointer;
  user-select: none;
`