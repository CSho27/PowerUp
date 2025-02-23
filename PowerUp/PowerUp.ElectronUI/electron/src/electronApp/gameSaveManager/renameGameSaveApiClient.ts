import { ResultResponse } from "../../app/shared/resultResponse";
import { CommandFetcher } from "../../utils/commandFetcher";

export interface RenameGameSaveRequest {
  gameSaveId: number;
  name: string | null;
}

export class RenameGameSaveApiClient {
  private readonly commandName = 'RenameGameSave';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  } 

  readonly execute = (request: RenameGameSaveRequest): Promise<ResultResponse> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}