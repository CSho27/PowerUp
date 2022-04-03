import { Dispatch } from "react";
import { SpecialAbilities, SpecialAbilitiesAction } from "./specialAbilitiesState"

export interface SpecialAbilitiesEditorProps {
  details: SpecialAbilities;
  update: Dispatch<SpecialAbilitiesAction>;
}

export function SpecialAbilitiesEditor(props: SpecialAbilitiesEditorProps) {
  return <div>
    Special Abilities
  </div>
}