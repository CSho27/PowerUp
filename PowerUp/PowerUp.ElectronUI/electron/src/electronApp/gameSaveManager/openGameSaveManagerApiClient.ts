import { CommandFetcher } from "../../utils/commandFetcher";

export interface OpenGameSaveManagerResponse {
  activeGameSaveId: number | null;
  gameSaveOptions: GameSaveDto[];
}

export interface GameSaveDto {
  id: number;
  name: string;
}

export class OpenGameSaveManagerApiClient {
  private readonly commandName = 'OpenGameSaveManager';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  } 

  readonly execute = (): Promise<OpenGameSaveManagerResponse> => {
    return this.commandFetcher.execute(this.commandName, {});
  }
}