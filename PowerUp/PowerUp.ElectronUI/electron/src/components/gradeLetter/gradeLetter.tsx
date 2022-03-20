import styled from "styled-components"
import { COLORS, FONT_SIZES } from "../../style/constants";
import { OutlineHeader } from "../outlineHeader/outlineHeader"


export type Grade = 
| 'A'
| 'B'
| 'C'
| 'D'
| 'E'
| 'F'
| 'G'

export interface GradeLetterProps {
  grade: Grade
}

export function GradeLetter(props: GradeLetterProps) {
  return <OutlineHeader
    slanted
    allCaps
    fontSize={FONT_SIZES._32}
    strokeWeight={1} 
    textColor={colors[props.grade]} 
    strokeColor={COLORS.white.regular_100}  
  >
    {props.grade}
  </OutlineHeader>
}

const colors: { [g in Grade]: string } = {
  A: COLORS.grades.A_pink_51,
  B: COLORS.grades.B_red_46,
  C: COLORS.grades.C_orange_59,
  D: COLORS.grades.D_yellow_49,
  E: COLORS.grades.E_green_34,
  F: COLORS.grades.F_blue_54,
  G: COLORS.grades.G_gray_55
}
