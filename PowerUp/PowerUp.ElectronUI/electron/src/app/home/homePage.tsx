import { IAppContext } from "../appContext";
import { PowerUpLayout } from "../shared/powerUpLayout";

export interface HomePageProps {
  appContext: IAppContext
}

export function HomePage(props: HomePageProps) {
  return <PowerUpLayout />
}