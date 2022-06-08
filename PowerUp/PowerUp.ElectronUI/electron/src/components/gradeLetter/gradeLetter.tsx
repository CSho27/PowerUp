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
  grade: Grade;
  size?: 'Small' | 'Medium';
}

export function GradeLetter(props: GradeLetterProps) {
  return <OutlineHeader
    slanted
    allCaps
    fontSize={props.size === 'Small'
      ? FONT_SIZES._24
      : FONT_SIZES._32}
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



export function getGradeFor0_15(value: number): Grade {
  if(value >= 14) return 'A';
  if(value >= 12) return 'B';
  if(value >= 10) return 'C';
  if(value >= 8)  return 'D';
  if(value >= 6)  return 'E';
  if(value >= 4)  return 'F';
  else            return 'G'; 
}

export function getGradeForPower(value: number): Grade {
  if(value >= 180) return 'A';
  if(value >= 140) return 'B';
  if(value >= 120) return 'C';
  if(value >= 100) return 'D';
  if(value >= 85)  return 'E';
  if(value >= 25)  return 'F';
  else             return 'G'; 
}

export function getGradeForControl(value: number): Grade {
  if(value >= 180) return 'A';
  if(value >= 155) return 'B';
  if(value >= 135) return 'C';
  if(value >= 120) return 'D';
  if(value >= 110) return 'E';
  if(value >= 100) return 'F';
  else             return 'G'; 
}

export function getGradeForStamina(value: number): Grade {
  if(value >= 150) return 'A';
  if(value >= 110) return 'B';
  if(value >= 80)  return 'C';
  if(value >= 60)  return 'D';
  if(value >= 30)  return 'E';
  if(value >= 15)  return 'F';
  else             return 'G'; 
}

