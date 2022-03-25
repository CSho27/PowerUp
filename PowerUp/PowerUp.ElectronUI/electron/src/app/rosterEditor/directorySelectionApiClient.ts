export type FileSystemSelectionType =
| 'File'
| 'Directory'

export interface FileSystemSelectionRequest {
  selectionType: FileSystemSelectionType;
}

export interface FileSystemSelectionResponse {
  path: string | null;
}

export class DirectorySelectionApiClient {
  execute = async (request: FileSystemSelectionRequest): Promise<FileSystemSelectionResponse> => {
    try {
      const response = await fetch('./file-system-selection', {
        method: 'POST',
        mode: 'same-origin',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(request)
      });
      return await response.json(); 
    } catch (error) {
      console.error(error);
      return new Promise((_, reject) => reject(error));
    }
  }
}