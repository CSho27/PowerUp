import { Dispatch } from "react";
import { SpecialAbilitiesOptions } from "./loadPlayerEditorApiClient";
import { SpecialAbilities, SpecialAbilitiesAction } from "./specialAbilitiesState"

export interface SpecialAbilitiesEditorProps {
  options: SpecialAbilitiesOptions;
  details: SpecialAbilities;
  update: Dispatch<SpecialAbilitiesAction>;
}

export function SpecialAbilitiesEditor(props: SpecialAbilitiesEditorProps) {
  return <div>
    Special Abilities
  </div>
}