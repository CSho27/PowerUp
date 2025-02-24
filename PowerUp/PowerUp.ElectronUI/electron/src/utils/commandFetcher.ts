import { PerformWithSpinnerCallback } from "../app/appContext";
import { ContentDisposition } from "./ContentDisposition";
import { trim } from "./stringUtils";

export class CommandFetcher {
  private readonly commandUrl: string;
  private readonly performWithSpinner: PerformWithSpinnerCallback;

  constructor(commandUrl: string, performWithSpinner: PerformWithSpinnerCallback) {
    this.commandUrl = commandUrl;
    this.performWithSpinner = performWithSpinner;
  }

  readonly execute = async (commandName: string, request: any, useSpinner?: boolean): Promise<any> => {
    return this.executeRequest(commandName, request, null, useSpinner);
  }

  readonly executeWithFile = async (commandName: string, request: any, file: File | null, useSpinner?: boolean): Promise<any> => {
    return this.executeRequest(commandName, request, file, useSpinner);
  }

  readonly log = async (level: LogLevel, message: unknown) => {
    return this.execute("WriteLog", { logLevel: level, message: message });
  }

  private readonly executeRequest = async (commandName: string, request: any, file: File | null, useSpinner?: boolean): Promise<any> => {
    const shouldUseSpinner = useSpinner ?? true; 
    if(commandName !== 'WriteLog') this.log('Debug', `Executing command: ${commandName} with request: ${JSON.stringify(request)}`);
    return shouldUseSpinner
      ? this.performWithSpinner(() => this.performFetch(commandName, request, file))
      : this.performFetch(commandName, request, file);
  }

  private readonly performFetch = async (commandName: string, request: any, file: File | null) => {
    try {
      const response = await fetch(this.commandUrl, this.createRequest(commandName, request, file));
      const responseType = response.headers.get('Content-Type');
      if(responseType?.includes('application/json')) {
        return await response.json();
      }
      if(responseType?.includes('multipart/form-data')) {
        const disposition = new ContentDisposition(response.headers.get('content-disposition'));
        const rawFileName = disposition['filename'] as string | undefined;
        const fileName = !!rawFileName
          ? trim(rawFileName, '"')
          : 'Untitled';
        const blob = await response.blob();
        return new File([blob], fileName);
      }
      else {
        throw await response.text();
      }
    } catch (error) {
      if(commandName !== 'WriteLog') this.log('Error', JSON.stringify(error));
      return new Promise((_, reject) => reject(error));
    }
  }

  private readonly createRequest = (commandName: string, request: any, file: File | null): RequestInit => {
    const formData = new FormData();
    formData.append('CommandName', commandName);
    formData.append('Request', JSON.stringify(request));
    if(!!file) formData.append('File', file);
    return {
      method: 'POST',
      mode: 'same-origin',
      body: formData
    }
  }
}

export type LogLevel = 
| 'Trace' 
| 'Debug' 
| 'Information' 
| 'Warning' 
| 'Error' 
| 'Critical' 
| 'None';

export interface WriteLogRequest {
  logLevel: LogLevel;
  message: string;
}