import { DragDropContext, Draggable, Droppable, DropResult, ResponderProvided } from "react-beautiful-dnd";
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
  updateLineupOrder: (playerId: number | 'Pitcher', currentOrderInLineup: number, newOrderInLineup: number) => void;
}

export interface LineupSlotDefinition {
  orderInLineup: number | undefined;
  position: Position | undefined;
  details: HitterDetails;
}

interface InternalLineupSlotDefinition {
  orderInLineup: number | undefined;
  position: Position | undefined;
  details: HitterDetails | undefined;
}

export interface HitterDetails {
  sourceType: EntitySourceType;
  playerId: number;
  savedName: string;
  fullName: string;
  position: Position;
}

export function LineupEditor(props: LineupEditorProps) {
  const { players, useDh, updateLineupOrder } = props;

  const sortedPlayers = players.sort(byOrder);
  const playersInLineup = sortedPlayers.filter(p => !!p.orderInLineup);
  const playersOnBench = sortedPlayers.filter(p => !p.orderInLineup);
  const lineup = useDh
    ? playersInLineup
    : lineupWithPitcherSlot(playersInLineup);   
  
  return <EditorWrapper>
    <DragDropContext onDragEnd={handleLineupReorderDragEnd}>
      <Droppable droppableId='starting-lineup'>
        {provided => 
        <PlayerGroupWrapper ref={provided.innerRef} {...provided.droppableProps}>
          <PlayerGroupHeading>Starting Lineup</PlayerGroupHeading>
            {lineup.map(toLineupSlot)}
            {provided.placeholder}
        </PlayerGroupWrapper>}
      </Droppable>
      <PlayerGroupWrapper>
        <PlayerGroupHeading>Bench</PlayerGroupHeading>
        {playersOnBench.map(toPlayerTile)}
      </PlayerGroupWrapper>
    </DragDropContext>
  </EditorWrapper>

  function toLineupSlot(slot: InternalLineupSlotDefinition, index: number) {
    return <PlayerRowWrapper key={slot.position === 'Pitcher' ? 'Pitcher' : slot.details!.playerId}>
      {slot.orderInLineup && <div>{slot.orderInLineup}.</div>}
      <SlotTile 
        index={index}
        details={slot.details} 
        position={slot.position!} />
    </PlayerRowWrapper>
  }

  function toPlayerTile(slot: LineupSlotDefinition) {
    return <PlayerTile key={slot.details!.playerId} details={slot.details!} />
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

  function lineupWithPitcherSlot(playersInLineup: LineupSlotDefinition[]): InternalLineupSlotDefinition[] {
    let pitcherIndex = 0;
    playersInLineup.forEach((p, i) => {
      if(p.orderInLineup == i+1)
        pitcherIndex++;
    });

    return insert<InternalLineupSlotDefinition>(playersInLineup, { details: undefined, orderInLineup: pitcherIndex + 1, position: 'Pitcher' }, pitcherIndex);
  }

  function handleLineupReorderDragEnd(result: DropResult, provided: ResponderProvided) {
    if(!result.destination)
      return;
    
    const draggedPlayer: LineupSlotDefinition | undefined = players.find(p => p.details.playerId.toString() === result.draggableId);
    updateLineupOrder(draggedPlayer?.details?.playerId ?? 'Pitcher', result.source.index+1, result.destination.index+1);
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

interface SlotTileProps {
  index: number;
  details: HitterDetails | undefined;
  position: Position;
}

function SlotTile(props: SlotTileProps) {
  const { index, details, position } = props;
  
  return <Draggable index={index} draggableId={details?.playerId?.toString() ?? 'Pitcher'}>
    {provided => 
    <PlayerTileWrapper ref={provided.innerRef} {...provided.draggableProps}>
      <NameContainer>
        <Icon icon='bars' {...provided.dragHandleProps} />
        {position === 'Pitcher' && 
        <PositionBubble
          size='Medium'
          positionType={getPositionType(position)}>
            {getPositionAbbreviation(position)}
        </PositionBubble>}
        {position !== 'Pitcher' && <PositionTile position={position} />}
        <NameContentContainer>
          {position === 'Pitcher' && <PitcherTile />}
          {position !== 'Pitcher' && <PlayerTile details={details!} />}
        </NameContentContainer>
      </NameContainer>
    </PlayerTileWrapper>}
  </Draggable>
}

const PlayerTileWrapper = styled.div`
  width: 18rem;
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

const NameContentContainer = styled.div`
  flex: 1 1 auto;
`

interface PositionTileProps {
  position: Position;
}

function PositionTile(props: PositionTileProps) {
  const { position } = props;

  return <DraggableTile>
    <PositionBubble
        size='Medium'
        positionType={getPositionType(position)}>
          {getPositionAbbreviation(position)}
      </PositionBubble>
  </DraggableTile>
}


interface PlayerTileProps {
  details: HitterDetails;
}

function PlayerTile(props: PlayerTileProps) {
  const { details } = props;
  
  return <DraggableTile>
    <PlayerNameBubble 
      positionType={getPositionType(details.position)}
      size='Medium'      
      title={details.fullName}
      fullWidth
      sourceType={details.sourceType}> 
        {details.savedName}
    </PlayerNameBubble>
  </DraggableTile>
}

function PitcherTile() {
  return <TextBubble size='Medium' positionType='Pitcher' fullWidth>
    <PitcherTitle>Pitcher</PitcherTitle>
  </TextBubble>
}

const PitcherTitle = styled.div`
  flex: 1 1 auto;
  ${textOutline('1px', COLORS.richBlack.regular_5)}
  text-align: left;
  white-space: nowrap;
  text-overflow: ellipsis;
`

const DraggableTile = styled.div`
  border: 2px solid ${COLORS.primaryBlue.regular_45_t40};
  border-radius: 10px;
  user-select: none;
  cursor: grab;

  &:hover {
    border-color: ${COLORS.primaryBlue.regular_45};
  }

  &:active {
    cursor: grabbing;
  }
`