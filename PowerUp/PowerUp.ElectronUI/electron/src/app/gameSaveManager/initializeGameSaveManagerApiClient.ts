import { CommandFetcher } from "../../utils/commandFetcher";

export interface InitializeGameSaveManagerRequest {
  directoryPath?: string;
}

export interface InitializeGameSaveManagerResponse {
  success: boolean;
}

export class InitializeGameSaveManagerApiClient {
  private readonly commandName = 'InitializeGameSaveManager';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  } 

  readonly execute = (request: InitializeGameSaveManagerRequest): Promise<InitializeGameSaveManagerResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}