import styled from "styled-components"
import { Grade, GradeLetter } from "../../components/gradeLetter/gradeLetter"
import { RotationWrapper } from "../../components/rotationWrapper/rotationWrapper"
import { COLORS } from "../../style/constants"

export interface PositionCapabilityDisplayProps {
  pitcher: Grade;
  catcher: Grade;
  firstBase: Grade;
  secondBase: Grade;
  thirdBase: Grade;
  shortstop: Grade;
  leftField: Grade;
  centerField: Grade;
  rightField: Grade;
}

export function PositionCapabilityDisplay(props: PositionCapabilityDisplayProps) {
  return <Wrapper>
    <FieldWrapper>
      <RotationWrapper rotation='225deg'>
        <FieldOutline>
          <InfieldOutline>
          </InfieldOutline>
        </FieldOutline>
    </RotationWrapper>
    </FieldWrapper>
    <PositioningContainer top={120} left={143}>
      <GradeLetter grade={props.pitcher} />
    </PositioningContainer>
    <PositioningContainer top={173} left={143}>
      <GradeLetter grade={props.catcher} />
    </PositioningContainer>
    <PositioningContainer top={100} right={88}>
      <GradeLetter grade={props.firstBase} />
    </PositioningContainer>
    <PositioningContainer top={65} right={115}>
      <GradeLetter grade={props.secondBase} />
    </PositioningContainer>
    <PositioningContainer top={65} left={115}>
      <GradeLetter grade={props.shortstop} />
    </PositioningContainer>
    <PositioningContainer top={100} left={88}>
      <GradeLetter grade={props.thirdBase} />
    </PositioningContainer>
    <PositioningContainer top={35} left={60}>
      <GradeLetter grade={props.leftField} />
    </PositioningContainer>
    <PositioningContainer top={10} left={143}>
      <GradeLetter grade={props.centerField} />
    </PositioningContainer>
    <PositioningContainer top={35} right={60}>
      <GradeLetter grade={props.rightField} />
    </PositioningContainer>
  </Wrapper>
}

const Wrapper = styled.div`
  position: relative;
`

const FieldWrapper = styled.div`
  position: absolute;
  width: 100%;
  top: -60px;
  display: flex;
  justify-content: center;
`

const FieldOutline = styled.div`
  height: 180px;
  width: 180px;
  border: 2px solid ${COLORS.richBlack.regular_5};
  border-radius: 0 5% 80% 5%;
`

const InfieldOutline = styled.div`
  height: 80px;
  width: 80px;
  border-bottom: 2px solid ${COLORS.richBlack.regular_5};
  border-right: 2px solid ${COLORS.richBlack.regular_5};
`

const PositioningContainer = styled.div<{ top?: number, left?: number, right?: number, bottom?: number }>`
  position: absolute;
  background-color: white;
  border-radius: 50%;
  top: ${p => p.top}px;
  left: ${p => p.left}px;
  right: ${p => p.right}px;
  bottom: ${p => p.bottom}px;
  display: flex;
  align-items: center;
  justify-content: center;
`