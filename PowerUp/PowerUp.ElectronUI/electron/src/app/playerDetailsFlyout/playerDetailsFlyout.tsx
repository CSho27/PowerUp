import { useState } from "react";
import styled from "styled-components";
import { getGradeFor0_15, getGradeForControl, getGradeForPower, getGradeForStamina, GradeLetter } from "../../components/gradeLetter/gradeLetter";
import { IconButton } from "../../components/icon/iconButton";
import { OutlineHeader } from "../../components/outlineHeader/outlineHeader";
import { PlayerNameBubble } from "../../components/textBubble/playerNameBubble";
import { TrajectoryArrow, TrajectoryValue } from "../../components/trajcetoryArrow/trajectoryArrow";
import { COLORS, FONT_SIZES } from "../../style/constants";
import { AppContext } from "../app";
import { getPositionType } from "../shared/positionCode";
import { HitterDetailsDto, PitcherDetailsDto, PlayerFlyoutDetailsResponse } from "./getPlayerFlyoutDetailsApiClient";
import { PitchArsenalDisplay } from "./pitchArsenalDisplay";
import { PitchBreakMeter } from "./pitchBreakMeter";

export interface PlayerDetailsFlyoutProps {
  appContext: AppContext;
  response: PlayerFlyoutDetailsResponse;
}

export function PlayerDetailsFlyout(props: PlayerDetailsFlyoutProps) {
  const { appContext, response } = props;
  const { 
    playerId, 
    sourceType,
    primaryPosition,
    savedName, 
    informalDisplayName, 
    uniformNumber,
    overall,
    hitterDetails,
    pitcherDetails,
    positionCapabilities
  } = response;

  const [pageNumber, setPageNumber] = useState(1);

  return <PlayerDetailsBackground>
    <PlayerDetailsContainer>
      <PlayerSavedNameSection>
        <PlayerNameBubble
          appContext={appContext}
          playerId={playerId} 
          sourceType={sourceType} 
          positionType={getPositionType(primaryPosition)} 
          size='Medium'
          withoutInfoFlyout
          fullWidth>
            {savedName}
        </PlayerNameBubble>
      </PlayerSavedNameSection>
      <PlayerDetailsContentContainer>
        <PlayerName>
          {informalDisplayName}
          <OutlineHeader style={{ flex: 'auto' }} fontSize={FONT_SIZES._24} strokeWeight={1} textColor={COLORS.primaryBlue.regular_45} strokeColor={COLORS.jet.superlight_90}>
            {uniformNumber}
          </OutlineHeader>
        </PlayerName>
        <DetailWrapper>
          <PlayerDetailLabel>Ovr</PlayerDetailLabel>
          <Value>{overall}</Value>
        </DetailWrapper>
        {((primaryPosition !== 'Pitcher' && pageNumber === 1) || (primaryPosition === 'Pitcher' && pageNumber === 3)) && 
        <HitterFlyoutContent {...hitterDetails} />}
        {((primaryPosition !== 'Pitcher' && pageNumber === 3) || (primaryPosition === 'Pitcher' && pageNumber === 1)) && 
        <PitcherFlyoutContent {...pitcherDetails} />}
        {pageNumber == 2 && <>
          position capabilities
        </>}
      </PlayerDetailsContentContainer>
      <Pager>
          <IconButton icon='angle-left' onClick={() => setPageNumber(p => p - 1)} disabled={pageNumber <= 1} />
          <div>{pageNumber}</div>
          <IconButton icon='angle-right' onClick={() => setPageNumber(p => p + 1)} disabled={pageNumber >= 3}/>
        </Pager>
    </PlayerDetailsContainer>
  </PlayerDetailsBackground>
}

function HitterFlyoutContent(props: HitterDetailsDto) {
  return <>
    <NewRowDetailWrapper>
      <PlayerDetailLabel>Con</PlayerDetailLabel>
      <Value>{props.contact}</Value>
      <GradeLetter grade={getGradeFor0_15(props.contact)} size='Small' />
    </NewRowDetailWrapper>
    <DetailWrapper>
      <PlayerDetailLabel>Trj</PlayerDetailLabel>
      <Value>{props.trajectory}</Value>
      <TrajectoryArrow value={props.trajectory as TrajectoryValue} size='Small' />
    </DetailWrapper>
    <DetailWrapper>
      <PlayerDetailLabel>Pow</PlayerDetailLabel>
      <Value>{props.power}</Value>
      <GradeLetter grade={getGradeForPower(props.power)} size='Small' />
    </DetailWrapper>
    <DetailWrapper>
      <PlayerDetailLabel>E Res</PlayerDetailLabel>
      <Value>{props.errorResistance}</Value>
      <GradeLetter grade={getGradeFor0_15(props.errorResistance)} size='Small' />
    </DetailWrapper>
    <DetailWrapper>
      <PlayerDetailLabel>Run Spd</PlayerDetailLabel>
      <Value>{props.runSpeed}</Value>
      <GradeLetter grade={getGradeFor0_15(props.runSpeed)} size='Small' />
    </DetailWrapper>
    <DetailWrapper>
      <PlayerDetailLabel>B/T</PlayerDetailLabel>
      <Value>{props.batsAndThrows}</Value>
    </DetailWrapper>
    <DetailWrapper>
      <PlayerDetailLabel>Arm Str</PlayerDetailLabel>
      <Value>{props.armStrength}</Value>
      <GradeLetter grade={getGradeFor0_15(props.armStrength)} size='Small' />
    </DetailWrapper>
    <PositionsWrapper>
      <PlayerDetailLabel>Pos</PlayerDetailLabel>
      <Value>{props.positions}</Value>
    </PositionsWrapper>
    <DetailWrapper>
      <PlayerDetailLabel>Fld</PlayerDetailLabel>
      <Value>{props.fielding}</Value>
      <GradeLetter grade={getGradeFor0_15(props.fielding)} size='Small' />
    </DetailWrapper>
  </>
}

function PitcherFlyoutContent(props: PitcherDetailsDto) {
  const { topSpeed, throwingArm, pitcherType, control, stamina, ...pitchArsenal } = props;

  return <>
    <DetailWrapper>
      <PlayerDetailLabel>Top Spd</PlayerDetailLabel>
      <Value>{props.topSpeed}</Value>
    </DetailWrapper>
    <DetailWrapper>
      <PlayerDetailLabel>Throws</PlayerDetailLabel>
      <Value>{props.throwingArm}</Value>
    </DetailWrapper>
    <PitcherTypeWrapper>
      <PlayerDetailLabel>Type</PlayerDetailLabel>
      <Value>{props.pitcherType}</Value>
    </PitcherTypeWrapper>
    <DetailWrapper>
      <PlayerDetailLabel>Ctrl</PlayerDetailLabel>
      <Value>{props.control}</Value>
      <GradeLetter grade={getGradeForControl(props.control)} size='Small' />
    </DetailWrapper>
    <DetailWrapper>
      <PlayerDetailLabel>Stam</PlayerDetailLabel>
      <Value>{props.stamina}</Value>
      <GradeLetter grade={getGradeForStamina(props.stamina)} size='Small' />
    </DetailWrapper>
    <PitchBreakSection>
      <PitchArsenalDisplay isLefty={props.throwingArm === 'Left'} {...pitchArsenal} />
    </PitchBreakSection>
  </>
}

const PlayerDetailsBackground = styled.div`
  background-color: ${COLORS.primaryBlue.lighter_69_s70};
  padding: 16px;
  padding-top: 32px;
  border-radius: 16px;
  height: 338px;
`

const PlayerDetailsContainer = styled.div`
  width: 100%;
  height: 100%;
  display: grid;
  grid-template-rows: auto auto;
  background-color: ${COLORS.white.regular_100};
  border-radius: 16px;
  position: relative;
`

const PlayerSavedNameSection = styled.div`
  min-width: 250px;
  background-color: ${COLORS.primaryBlue.lighter_69_s70};
  position: absolute;
  top: -32px;
  left: -8px;
  border-radius: 16px;
  padding: 8px;
`

const PlayerDetailsContentContainer = styled.div`
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 0 16px;
  padding: 8px;
  padding-top: 24px;
`

const PlayerName = styled.div`
  grid-column: span 2;
  font-size: ${FONT_SIZES._18};
  font-weight: 600;
  font-style: italic;
  display: flex;
  align-items: center;
  gap: 16px;
`

const DetailWrapper = styled.div`
  display: grid;
  grid-template-columns: 70px 30px 30px;
  gap: 8px;
`

const PositionsWrapper = styled.div`
  display: flex;
  justify-content: space-between;
  gap: 8px;
  white-space: nowrap;
`

const NewRowDetailWrapper = styled(DetailWrapper)`
  grid-column: 1;
`

const PitcherTypeWrapper = styled.div`
  display: grid;
  grid-template-columns: 70px 1fr;
  gap: 8px;
`

const PlayerDetailLabel = styled.div`
  font-weight: 600;
  font-style: italic;
  text-align: left;
`

const Value = styled.div`
  font-style: italic;
`

const Pager = styled.div`
  display: flex;
  align-items: flex-end;
  justify-content: flex-end;
  gap: 8px;
  padding: 0px 16px;
  font-size: ${FONT_SIZES._18};
`

const PitchBreakSection = styled.div`
  grid-column: span 2;
`


