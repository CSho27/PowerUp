import { DragDropContext, Draggable, Droppable, DropResult, ResponderProvided } from "react-beautiful-dnd";
import styled from "styled-components";
import { Icon } from "../../components/icon/icon";
import { PlayerNameBubble } from "../../components/textBubble/playerNameBubble";
import { COLORS } from "../../style/constants";
import { AppContext } from "../app";
import { EntitySourceType } from "../shared/entitySourceType";
import { PitcherRole } from "./playerRoleState";

export interface PitcherRolesEditorProps {
  appContext: AppContext;
  starters: PitcherDetails[];
  swingMen: PitcherDetails[];
  longRelievers: PitcherDetails[];
  middleRelievers: PitcherDetails[];
  situationalLefties: PitcherDetails[];
  mopUpMen: PitcherDetails[];
  setupMen: PitcherDetails[];
  closer: PitcherDetails | undefined;
}

export interface PitcherDetails {
  sourceType: EntitySourceType;
  playerId: number;
  savedName: string;
  fullName: string;
  overall: number;
  throwingArm: string;
  pitcherType: string;
  topSpeed: string;
  control: string;
  stamina: string;
}

export function PitcherRolesEditor(props: PitcherRolesEditorProps) {
  const { 
    appContext,
    starters,  
    swingMen,
    longRelievers,
    middleRelievers,
    situationalLefties,
    mopUpMen,
    setupMen,
    closer
  } = props;
  
  return <Wrapper>
    <DragDropContext onDragEnd={handleDragEnd}>
      <PitcherRoleSection 
        pitcherRole='Starter'
        displayName='Starter'
        lightColor={COLORS.pitcherRoles.starter_orange_light_87}
        darkColor={COLORS.pitcherRoles.starter_orange_dark_48}
        pitchers={starters}
      />
      <PitcherRoleSection 
        pitcherRole='SwingMan'
        displayName='Swing Man'
        lightColor={COLORS.pitcherRoles.swingMan_pink_light_94}
        darkColor={COLORS.pitcherRoles.swingMan_pink_dark_51}
        pitchers={swingMen}
      />
      <PitcherRoleSection 
        pitcherRole='LongReliever'
        displayName='Long Reliever'
        lightColor={COLORS.pitcherRoles.longReliever_purple_light_94}
        darkColor={COLORS.pitcherRoles.longReliever_purple_dark_51}
        pitchers={longRelievers}
      />
      <PitcherRoleSection 
        pitcherRole='MiddleReliever'
        displayName='Middle Reliever'
        lightColor={COLORS.pitcherRoles.middleReliever_indigo_light_94}
        darkColor={COLORS.pitcherRoles.middleReliever_indigo_dark_51}
        pitchers={middleRelievers}
      />
      <PitcherRoleSection 
        pitcherRole='SituationalLefty'
        displayName='Situational Lefty'
        lightColor={COLORS.pitcherRoles.situationalLefty_blue_light_94}
        darkColor={COLORS.pitcherRoles.situationalLefty_blue_dark_33}
        pitchers={situationalLefties}
      />
      <PitcherRoleSection 
        pitcherRole='MopUpMan'
        displayName='Mop-Up Man'
        lightColor={COLORS.pitcherRoles.mopUpMan_teal_light_94}
        darkColor={COLORS.pitcherRoles.mopUpMan_teal_dark_33}
        pitchers={mopUpMen}
      />
      <PitcherRoleSection 
        pitcherRole='SetupMan'
        displayName='Set-Up Man'
        lightColor={COLORS.pitcherRoles.setUpMan_green_light_92}
        darkColor={COLORS.pitcherRoles.setUpMan_green_dark_33}
        pitchers={setupMen}
      />
      <PitcherRoleSection 
        pitcherRole='Closer'
        displayName='Closer'
        lightColor={COLORS.pitcherRoles.closer_yellow_light_92}
        darkColor={COLORS.pitcherRoles.closer_yellow_dark_35}
        pitchers={!!closer ? [closer] : []}
      />
    </DragDropContext>
  </Wrapper>

  function handleDragEnd(result: DropResult, provided: ResponderProvided) {

  }
}

interface PitcherRoleSectionProps {
  pitcherRole: PitcherRole
  displayName: string;
  lightColor: string;
  darkColor: string;
  pitchers: PitcherDetails[];
}

function PitcherRoleSection(props: PitcherRoleSectionProps) {
  const { pitcherRole, displayName, lightColor, darkColor, pitchers } = props;
  
  return <SectionWrapper>
    <h2>{displayName}</h2>
    <Droppable droppableId={pitcherRole}>
      {provided => 
        <SectionPitcherContainer 
          ref={provided.innerRef}
          lightColor={lightColor} 
          darkColor={darkColor}
          {...provided.droppableProps}>
            {pitchers.length > 0 &&
            <GridHeaderWrapper>
              <GridHeader></GridHeader>
              <GridHeader alignLeft>Name</GridHeader>
              <GridHeader>Arm</GridHeader>
              <GridHeader>Type</GridHeader>
              <GridHeader>Ovr</GridHeader>
              <GridHeader>Top Spd</GridHeader>
              <GridHeader>Ctrl</GridHeader>
              <GridHeader>Stam</GridHeader>
            </GridHeaderWrapper>}
            {pitchers.map(toPitcherRow)}
            {provided.placeholder}
      </SectionPitcherContainer>}
    </Droppable>
    
  </SectionWrapper>

  function toPitcherRow(details: PitcherDetails, index: number) {
    return <RowWrapper key={details.playerId}>
      <span>{index+1}.</span>
      <PitcherTile index={index} details={details} />
    </RowWrapper>
  }
}

const Wrapper = styled.div`
  width: fit-content;
`

const SectionWrapper = styled.div`
  padding-bottom: 16px;
`

const RowWrapper = styled.div`
  display: grid;
  gap: 16px;
  align-items: center;
  grid-template-columns: 1.5rem auto;
`

const SectionPitcherContainer = styled.div<{ lightColor: string, darkColor: string }>`
  display: flex;
  flex-direction: column;
  gap: 4px;
  background-color: ${p => p.lightColor};
  padding: 8px 16px;
  border: 4px solid ${p => p.darkColor};
  border-radius: 8px;
  text-align: center;
`

const GridHeaderWrapper = styled.div`
  display: grid;
  gap: 16px;
  align-items: center;
  grid-template-columns: 
    calc(1.5rem + 2px)
    calc(18rem + 4px)
    2rem
    2.25rem
    2rem
    4rem
    2rem
    3rem;
`

const GridHeader = styled.span<{ alignLeft?: boolean }>`
  font-weight: bold;
  font-style: italic;
  text-align: ${p => p.alignLeft ? 'left' : undefined};
`

interface PitcherTileProps {
  details: PitcherDetails;
  index: number;
}

function PitcherTile(props: PitcherTileProps) {
  const { details, index } = props;
  
  return <Draggable draggableId={details.playerId.toString()} index={index}>
      {provided => <PitcherTileWrapper ref={provided.innerRef} {...provided.draggableProps}>
        <NameContainer {...provided.dragHandleProps}>
          <Icon icon='bars' />
          <NameContentContainer>
            <PlayerNameBubble 
              positionType='Pitcher'
              size='Medium'      
              title={details.fullName}
              fullWidth
              sourceType={details.sourceType}> 
                {details.savedName}
            </PlayerNameBubble>
          </NameContentContainer>
        </NameContainer>
        <span>{details.throwingArm}</span>
        <span>{details.pitcherType}</span>
        <span>{details.overall}</span>
        <span>{details.topSpeed}</span>
        <span>{details.control}</span>
        <span>{details.stamina}</span>
      </PitcherTileWrapper>}
    </Draggable>
    
}

const PitcherTileWrapper = styled.div`
  display: grid;
  gap: 16px;
  align-items: center;
  grid-template-columns: 
    18rem
    2rem
    2.25rem
    2rem
    4rem
    2rem
    3rem;
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