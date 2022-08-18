import { CommandFetcher } from "../../utils/commandFetcher";

export interface GetCurrentGameSaveManagerDirectoryResponse {
  gameSaveManagerDirectoryPath: string | null;
}

export class GetCurrentGameSaveManagerDirectoryApiClient {
  private readonly commandName = 'GetGameSaveManagerDirectory';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  } 

  readonly execute = (): Promise<GetCurrentGameSaveManagerDirectoryResponse> => {
    return this.commandFetcher.execute(this.commandName, {});
  }
}