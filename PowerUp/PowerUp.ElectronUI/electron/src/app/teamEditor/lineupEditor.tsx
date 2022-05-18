import styled from "styled-components";
import { Icon } from "../../components/icon/icon";
import { PlayerNameBubble } from "../../components/textBubble/playerNameBubble";
import { PositionBubble } from "../../components/textBubble/positionBubble";
import { TextBubble } from "../../components/textBubble/textBubble";
import { COLORS } from "../../style/constants";
import { textOutline } from "../../style/outlineHelper";
import { insert } from "../../utils/arrayUtils";
import { EntitySourceType } from "../shared/entitySourceType";
import { getPositionAbbreviation, getPositionType, Position } from "../shared/positionCode";

export interface LineupEditorProps {
  players: LineupSlotDefinition[];
  useDh: boolean;
}

export interface LineupSlotDefinition {
  orderInLineup: number | undefined;
  position: Position | undefined;
  details: HitterDetails | undefined;
}

export interface HitterDetails {
  sourceType: EntitySourceType;
  playerId: number;
  savedName: string;
  fullName: string;
  overall: number;
  batsAndThrows: string;
  position: Position;
  positionAbbreviation: string;
}

export function LineupEditor(props: LineupEditorProps) {
  const { players, useDh } = props;

  const sortedPlayers = players.sort(byOrder);
  const playersInLineup = sortedPlayers.filter(p => !!p.orderInLineup);
  const playersOnBench = sortedPlayers.filter(p => !p.orderInLineup);
  const lineup = useDh
    ? playersInLineup
    : lineupWithPitcherSlot(playersInLineup);   
  
  return <EditorWrapper>
    <PlayerGroupWrapper>
      <PlayerGroupHeading>Starting Lineup</PlayerGroupHeading>
      {lineup.map(s => toLineupCard(s, true))}
    </PlayerGroupWrapper>
    <PlayerGroupWrapper>
      <PlayerGroupHeading>Bench</PlayerGroupHeading>
      {playersOnBench.map(s => toLineupCard(s, false))}
    </PlayerGroupWrapper>
  </EditorWrapper>

  function toLineupCard(slot: LineupSlotDefinition, isStarter: boolean) {
    return <PlayerRowWrapper key={slot.position === 'Pitcher' ? 'Pitcher' : slot.details!.playerId}>
      {slot.orderInLineup && <div>{slot.orderInLineup}.</div>}
      <SlotTile 
        details={slot.details} 
        sortable={isStarter} 
        position={isStarter 
          ? slot.position 
          : undefined} />
    </PlayerRowWrapper>
  }

  function byOrder(playerA: LineupSlotDefinition, playerB: LineupSlotDefinition): number {
    if(playerA.orderInLineup && playerB.orderInLineup)
      return playerA.orderInLineup - playerB.orderInLineup;
    if(playerA.orderInLineup && !playerB.orderInLineup)
      return -1;
    if(playerB.orderInLineup && !playerA.orderInLineup)
      return 1;
    else
      return 0;
  }

  function lineupWithPitcherSlot(playersInLineup: LineupSlotDefinition[]): LineupSlotDefinition[] {
    let pitcherIndex = 0;
    playersInLineup.forEach((p, i) => {
      if(p.orderInLineup == i+1)
        pitcherIndex++;
    });

    return insert<LineupSlotDefinition>(playersInLineup, { details: undefined, orderInLineup: pitcherIndex + 1, position: 'Pitcher' }, pitcherIndex);
  }
}

const EditorWrapper = styled.div`
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
`

const PlayerGroupWrapper = styled.div`
  width: fit-content;
  display: flex;
  flex-direction: column;
  align-items: stretch;
  gap: 4px;
  padding: 4px;
  border: 1px solid ${COLORS.richBlack.regular_5};
  border-radius: 16px;
  background-color: ${COLORS.jet.superlight_85};
`

const PlayerGroupHeading = styled.h2`
  text-align: center;
`

const PlayerRowWrapper = styled.div`
  display: flex;
  align-items: center;
  gap: 16px;
`

interface PlayerTileProps {
  details: HitterDetails | undefined;
  position: Position | undefined;
  sortable: boolean;
}

function SlotTile(props: PlayerTileProps) {
  const { details, position, sortable } = props;
  
  return <PlayerTileWrapper>
    <NameContainer>
      {sortable && <Icon icon='bars' />}
      {position && <PositionBubble
        size='Medium'
        positionType={position === 'Pitcher'
          ? 'Pitcher'
          :  getPositionType(position)}>
          {getPositionAbbreviation(position)}
      </PositionBubble>}
      <NameContentContainer>
        {position === 'Pitcher' &&
        <TextBubble size='Medium' positionType='Pitcher' fullWidth>
          <PitcherTitle>Pitcher</PitcherTitle>
        </TextBubble>}
        {position !== 'Pitcher' &&
        <PlayerNameBubble 
          positionType={getPositionType(details!.position)}
          size='Medium'      
          title={details!.fullName}
          fullWidth
          sourceType={details!.sourceType}> 
            {details!.savedName}
        </PlayerNameBubble>}
      </NameContentContainer>
    </NameContainer>
    {position !== 'Pitcher' && <>
    <span>{details!.overall}</span>
    <span>{details!.batsAndThrows}</span>
    </>}
  </PlayerTileWrapper>
}

const PlayerTileWrapper = styled.div`
  display: grid;
  gap: 16px;
  align-items: center;
  grid-template-columns: 
    18rem
    1.75rem
    2rem;
  padding: 2px 4px;
  border: 2px solid ${COLORS.richBlack.regular_5};
  border-radius: 8px;
  background-color: ${COLORS.white.regular_100_t60};
`

const NameContainer = styled.div`
  display: flex;
  gap: 8px;
  align-items: center;
`

const PitcherTitle = styled.div`
  flex: 1 1 auto;
  ${textOutline('1px', COLORS.richBlack.regular_5)}
  text-align: left;
  white-space: nowrap;
  text-overflow: ellipsis;
`

const NameContentContainer = styled.div`
  flex: 1 1 auto;
`