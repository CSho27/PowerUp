import { CommandFetcher, getDefaultRequestOptions } from "../../utils/commandFetcher";

export type FileSystemSelectionType =
| 'File'
| 'Directory'

export interface FileSystemSelectionRequest {
  selectionType: FileSystemSelectionType;
  fileFilter?: FileFilterRequest;
}

export interface FileFilterRequest {
  name: string;
  allowedExtensions: string[];
}

export interface FileSystemSelectionResponse {
  path: string | null;
}

/** @deprecated This api client is no longer wired up to anything */
export class FileSystemSelectionApiClient {
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = async (request: FileSystemSelectionRequest): Promise<FileSystemSelectionResponse> => {
    try {
      const response = await fetch('./file-system-selection', {
        method: 'POST',
        body: JSON.stringify(request),
        ...getDefaultRequestOptions({ 'Content-Type': 'application/json' })
      });
      return await response.json(); 
    } catch (error) {
      this.commandFetcher.log('Error', error);
      return new Promise((_, reject) => reject(error));
    }
  }
}