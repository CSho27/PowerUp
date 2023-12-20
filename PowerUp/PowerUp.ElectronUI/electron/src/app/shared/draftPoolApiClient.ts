import { CommandFetcher } from "../../utils/commandFetcher";
import { PlayerDetailsResponse } from "../teamEditor/playerDetailsResponse";

export interface DraftPoolGenerationResponse {
  players: PlayerDetailsResponse[];
}

export class DraftPoolApiClient {
  private readonly commandName = 'DraftPoolGeneration';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (): Promise<DraftPoolGenerationResponse> => {
    return this.commandFetcher.execute(this.commandName, {});
  }
}