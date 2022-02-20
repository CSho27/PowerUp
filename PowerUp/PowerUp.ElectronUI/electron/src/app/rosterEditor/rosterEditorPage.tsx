import { IAppContext } from "../appContext";
import { PowerUpLayout } from "../shared/powerUpLayout";

export interface RosterEditorPageProps {
  appContext: IAppContext;
  teamNames: string[];
}

export function RosterEditorPage(props: RosterEditorPageProps) {
  const { appContext, teamNames } = props;
  
  return <PowerUpLayout headerText='Edit Roster'>
    {teamNames.map(t => <div key={t}>{t}</div>)}
  </PowerUpLayout>
}