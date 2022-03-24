export interface DirectorySelectionResponse {
  directoryPath: string;
}

export class DirectorySelectionApiClient {
  execute = async (): Promise<DirectorySelectionResponse> => {
    try {
      const response = await fetch('./select-directory', {
        method: 'GET',
        mode: 'same-origin',
      });
      return await response.json(); 
    } catch (error) {
      console.error(error);
      return new Promise((_, reject) => reject(error));
    }
  }
}