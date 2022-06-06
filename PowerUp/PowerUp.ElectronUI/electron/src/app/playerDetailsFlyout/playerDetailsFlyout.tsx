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
import { PlayerFlyoutDetailsResponse } from "./getPlayerFlyoutDetailsApiClient";

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
        {((primaryPosition !== 'Pitcher' && pageNumber === 1) || (primaryPosition === 'Pitcher' && pageNumber === 3)) && <>
        <NewRowDetailWrapper>
          <PlayerDetailLabel>Con</PlayerDetailLabel>
          <Value>{hitterDetails.contact}</Value>
          <GradeLetter grade={getGradeFor0_15(hitterDetails.contact)} size='Small' />
        </NewRowDetailWrapper>
        <DetailWrapper>
          <PlayerDetailLabel>Trj</PlayerDetailLabel>
          <Value>{hitterDetails.trajectory}</Value>
          <TrajectoryArrow value={hitterDetails.trajectory as TrajectoryValue} size='Small' />
        </DetailWrapper>
        <DetailWrapper>
          <PlayerDetailLabel>Pow</PlayerDetailLabel>
          <Value>{hitterDetails.power}</Value>
          <GradeLetter grade={getGradeForPower(hitterDetails.power)} size='Small' />
        </DetailWrapper>
        <DetailWrapper>
          <PlayerDetailLabel>E Res</PlayerDetailLabel>
          <Value>{hitterDetails.errorResistance}</Value>
          <GradeLetter grade={getGradeFor0_15(hitterDetails.errorResistance)} size='Small' />
        </DetailWrapper>
        <DetailWrapper>
          <PlayerDetailLabel>Run Spd</PlayerDetailLabel>
          <Value>{hitterDetails.runSpeed}</Value>
          <GradeLetter grade={getGradeFor0_15(hitterDetails.runSpeed)} size='Small' />
        </DetailWrapper>
        <DetailWrapper>
          <PlayerDetailLabel>B/T</PlayerDetailLabel>
          <Value>{hitterDetails.batsAndThrows}</Value>
        </DetailWrapper>
        <DetailWrapper>
          <PlayerDetailLabel>Arm Str</PlayerDetailLabel>
          <Value>{hitterDetails.armStrength}</Value>
          <GradeLetter grade={getGradeFor0_15(hitterDetails.armStrength)} size='Small' />
        </DetailWrapper>
        <PositionsWrapper>
          <PlayerDetailLabel>Pos</PlayerDetailLabel>
          <Value>{hitterDetails.positions}</Value>
        </PositionsWrapper>
        <DetailWrapper>
          <PlayerDetailLabel>Fld</PlayerDetailLabel>
          <Value>{hitterDetails.fielding}</Value>
          <GradeLetter grade={getGradeFor0_15(hitterDetails.fielding)} size='Small' />
        </DetailWrapper>
        </>}
        {((primaryPosition !== 'Pitcher' && pageNumber === 3) || (primaryPosition === 'Pitcher' && pageNumber === 1)) && <>
          <DetailWrapper>
            <PlayerDetailLabel>Top Spd</PlayerDetailLabel>
            <Value>{pitcherDetails.topSpeed}</Value>
          </DetailWrapper>
          <DetailWrapper>
            <PlayerDetailLabel>Throws</PlayerDetailLabel>
            <Value>{pitcherDetails.throwingArm}</Value>
          </DetailWrapper>
          <PitcherTypeWrapper>
            <PlayerDetailLabel>Type</PlayerDetailLabel>
            <Value>{pitcherDetails.pitcherType}</Value>
          </PitcherTypeWrapper>
          <DetailWrapper>
          <PlayerDetailLabel>Ctrl</PlayerDetailLabel>
          <Value>{pitcherDetails.control}</Value>
          <GradeLetter grade={getGradeForControl(pitcherDetails.control)} size='Small' />
        </DetailWrapper>
        <DetailWrapper>
          <PlayerDetailLabel>Stam</PlayerDetailLabel>
          <Value>{pitcherDetails.stamina}</Value>
          <GradeLetter grade={getGradeForStamina(pitcherDetails.stamina)} size='Small' />
        </DetailWrapper>
        </>}
        {pageNumber == 2 && <>
          position capabilities
        </>}
        <Pager>
          <IconButton icon='angle-left' onClick={() => setPageNumber(p => p - 1)} disabled={pageNumber <= 1} />
          <div>{pageNumber}</div>
          <IconButton icon='angle-right' onClick={() => setPageNumber(p => p + 1)} disabled={pageNumber >= 3}/>
        </Pager>
      </PlayerDetailsContentContainer>
    </PlayerDetailsContainer>
  </PlayerDetailsBackground>
}

const PlayerDetailsBackground = styled.div`
  background-color: ${COLORS.primaryBlue.lighter_69_s70};
  padding: 16px;
  padding-top: 32px;
  border-radius: 16px;
`

const PlayerDetailsContainer = styled.div`
  width: 100%;
  height: 100%;
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
  grid-column: span 2;
  display: flex;
  align-items: center;
  justify-content: flex-end;
  gap: 8px;
  font-size: ${FONT_SIZES._18};
`


