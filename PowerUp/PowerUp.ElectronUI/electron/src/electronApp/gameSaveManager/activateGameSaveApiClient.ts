import { ResultResponse } from "../../app/shared/resultResponse";
import { CommandFetcher } from "../../utils/commandFetcher";

export interface ActivateGameSaveRequest {
  gameSaveId: number;
}

export class ActivateGameSaveApiClient {
  private readonly commandName = 'ActivateGameSave';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  } 

  readonly execute = (request: ActivateGameSaveRequest): Promise<ResultResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}