import { CommandFetcher } from "../utils/commandFetcher";

export interface IAppContext {
  commandFetcher: CommandFetcher;
  setPage: (newPage: React.ReactNode) => void;
}