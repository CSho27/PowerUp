import { Breadcrumbs, Crumb } from "../../components/breadcrumbs/breadbrumbs";
import { AppContext } from "../app";
import { PowerUpLayout } from "../shared/powerUpLayout";

export interface RosterEditorPageProps {
  appContext: AppContext;
  teamNames: string[];
}

export function RosterEditorPage(props: RosterEditorPageProps) {
  const { appContext, teamNames } = props;
  
  return <PowerUpLayout headerText='Edit Roster'>
    <Breadcrumbs>
      <Crumb key={1} onClick={() => console.log(1)}>1</Crumb>
      <Crumb key={2} onClick={() => console.log(2)}>2</Crumb>
      <Crumb key={3} onClick={() => console.log(3)}>3</Crumb>
      <Crumb key={4} onClick={() => console.log(4)}>4</Crumb>
      <Crumb key={5} onClick={() => console.log(5)}>5</Crumb>
      <Crumb key={6} >6</Crumb>
    </Breadcrumbs>
    {teamNames.map(t => <div key={t}>{t}</div>)}
  </PowerUpLayout>
}