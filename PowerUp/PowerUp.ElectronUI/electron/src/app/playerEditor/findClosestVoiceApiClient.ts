import { CommandFetcher } from "../../utils/commandFetcher";
import { SimpleCode } from "../shared/simpleCode";

export interface FindClosestVoiceRequest {
  firstName: string;
  lastName: string;
}

export class FindClosestVoiceApiClient {
  private readonly commandName = 'FindClosestVoice';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  } 

  readonly execute = (request: FindClosestVoiceRequest): Promise<SimpleCode> => {
    return this.commandFetcher.execute(this.commandName, request);
  }
}