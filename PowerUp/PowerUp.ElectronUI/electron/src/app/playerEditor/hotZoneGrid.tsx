import { Dispatch, ReactNode } from "react";
import styled from "styled-components"
import { COLORS } from "../../style/constants";
import { HotZoneGridDto } from "./hotZoneGridDto";

export type HotZonePreference = 'Neutral' | 'Hot' | 'Cold';
export interface HotZoneGridState {
  upAndIn: HotZonePreference;
  up: HotZonePreference;
  upAndAway: HotZonePreference;
  middleIn: HotZonePreference;
  middle: HotZonePreference;
  middleAway: HotZonePreference;
  downAndIn: HotZonePreference;
  down: HotZonePreference;
  downAndAway: HotZonePreference;
}

export type HotZoneGridAction = 
| { type: 'toggleUpAndIn' }
| { type: 'toggleUp' }
| { type: 'toggleUpAndAway' }
| { type: 'toggleMiddleIn' }
| { type: 'toggleMiddle' }
| { type: 'toggleMiddleAway' }
| { type: 'toggleDownAndIn' }
| { type: 'toggleDown' }
| { type: 'toggleDownAndAway' }

export function HotZoneGridStateReducer(state: HotZoneGridState, action: HotZoneGridAction): HotZoneGridState {
  switch(action.type) {
    case 'toggleUpAndIn':
      return {
        ...state,
        upAndIn: nextPreference(state.upAndIn)
      }
    case 'toggleUp':
      return {
        ...state,
        up: nextPreference(state.up)
      }
    case 'toggleUpAndAway':
      return {
        ...state,
        upAndAway: nextPreference(state.upAndAway)
      }
    case 'toggleMiddleIn':
      return {
        ...state,
        middleIn: nextPreference(state.middleIn)
      }
    case 'toggleMiddle':
      return {
        ...state,
        middle: nextPreference(state.middle)
      }
    case 'toggleMiddleAway':
      return {
        ...state,
        middleAway: nextPreference(state.middleAway)
      }
    case 'toggleDownAndIn':
      return {
        ...state,
        downAndIn: nextPreference(state.downAndIn)
      }
    case 'toggleDown':
      return {
        ...state,
        down: nextPreference(state.down)
      }
    case 'toggleDownAndAway':
      return {
        ...state,
        downAndAway: nextPreference(state.downAndAway)
      }
  }
}

function nextPreference(preference: HotZonePreference): HotZonePreference {
  switch(preference) {
    case 'Neutral':
      return 'Hot';
    case 'Hot':
      return 'Cold';
    case 'Cold':
      return 'Neutral';
  }
}

export interface HotZoneGridProps {
  battingSide: BattingSide;
  grid: HotZoneGridState;
  disabled?: boolean;
  update: Dispatch<HotZoneGridAction>
}


export type BattingSide = 'Right' | 'Left' | 'Switch';

export function HotZoneGrid(props: HotZoneGridProps) {
  const { battingSide, grid, disabled, update } = props;

  const up = [
    <HotZoneGridTile 
      preference={grid.upAndIn} 
      disabled={disabled} 
      onClick={disabled 
        ? () => {} 
        : () => update({ type: 'toggleUpAndIn' })} 
    />,
    <HotZoneGridTile 
      preference={grid.up} 
      disabled={disabled} 
      onClick={disabled 
        ? () => {} 
        : () => update({ type: 'toggleUp' })} 
    />,
    <HotZoneGridTile 
      preference={grid.upAndAway} 
      disabled={disabled} 
      onClick={disabled 
        ? () => {} 
        : () => update({ type: 'toggleUpAndAway' })} 
    />
  ]

  const middle = [
    <HotZoneGridTile 
      preference={grid.middleIn} 
      disabled={disabled} 
      onClick={disabled 
        ? () => {} 
        : () => update({ type: 'toggleMiddleIn' })} 
    />,
    <HotZoneGridTile 
      preference={grid.middle} 
      disabled={disabled} 
      onClick={disabled 
        ? () => {} 
        : () => update({ type: 'toggleMiddle' })} 
    />,
    <HotZoneGridTile 
      preference={grid.middleAway} 
      disabled={disabled} 
      onClick={disabled 
        ? () => {} 
        : () => update({ type: 'toggleMiddleAway' })} 
    />
  ]

  const down = [
    <HotZoneGridTile 
      preference={grid.downAndIn} 
      disabled={disabled} 
      onClick={disabled 
        ? () => {} 
        : () => update({ type: 'toggleDownAndIn' })} 
    />,
    <HotZoneGridTile 
      preference={grid.down} 
      disabled={disabled} 
      onClick={disabled 
        ? () => {} 
        : () => update({ type: 'toggleDown' })} 
    />,
    <HotZoneGridTile 
      preference={grid.downAndAway} 
      disabled={disabled} 
      onClick={disabled 
        ? () => {} 
        : () => update({ type: 'toggleDownAndAway' })} 
    />
  ]

  return <HotZoneGridWrapper>
    <HotZoneGridBatterSlot visible={battingSide !== 'Left'}>Batter</HotZoneGridBatterSlot>
    <table>
      <tbody>
        <tr>
          {battingSide !== 'Left' && up.map(toCell)}
          {battingSide === 'Left' && up.reverse().map(toCell)}
        </tr>
        <tr>
          {battingSide !== 'Left' && middle.map(toCell)}
          {battingSide === 'Left' && middle.reverse().map(toCell)}
        </tr>
        <tr>
          {battingSide !== 'Left' && down.map(toCell)}
          {battingSide === 'Left' && down.reverse().map(toCell)}
        </tr>
      </tbody>
    </table>
    <HotZoneGridBatterSlot visible={battingSide !== 'Right'}>Batter</HotZoneGridBatterSlot>
  </HotZoneGridWrapper>

  function toCell(node: ReactNode,index: number) {
    return <HzTd key={index}>{node}</HzTd>
  }
}

const HotZoneGridWrapper = styled.div`
  display: flex;
  gap: 16px;
`

const HotZoneGridBatterSlot = styled.div<{ visible: boolean }>`
  opacity: ${p => p.visible ? '1' : '0'};
  writing-mode: vertical-lr;
  text-orientation: upright;
  text-align: center;
`

const HzTd = styled.td`
  padding: 0px;
`

const preferenceColors: { [preference in HotZonePreference]: string } = {
  Neutral: COLORS.hotZones.neutral_gray_42,
  Hot: COLORS.hotZones.hot_red_51,
  Cold: COLORS.hotZones.cold_blue_48
}

const HotZoneGridTile = styled.a<{ preference: HotZonePreference, disabled?: boolean }>`
  display: block;
  width: 64px;
  height: 80px;
  opacity: .7;
  background-color: ${p => preferenceColors[p.preference]};
  cursor: ${p => p.disabled ? 'undefined' : 'pointer' };

  &:hover {
    opacity: ${p => p.disabled ? 'undefined' : '.6' };
  }
`