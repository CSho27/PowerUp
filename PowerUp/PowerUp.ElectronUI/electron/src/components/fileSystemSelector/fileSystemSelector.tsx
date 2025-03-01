import { useMemo, useRef } from "react";
import styled from "styled-components";
import { Button } from "../button/button";
import { FileSystemSelectionApiClient, FileSystemSelectionType } from "./fileSystemSelectionApiClient";
import { AppContext } from "../../app/appContext";

export interface FileSystemSelectorProps {
  appContext: AppContext;
  type: FileSystemSelectionType;
  selectedPath: string | undefined;
  onSelection: (path: string | undefined) => void;
  id?: string;
  fileFilter?: FileFilter;
  disabled?: boolean;
}

export interface FileFilter {
  name: string;
  allowedExtensions: FileExtension[];
}

export type FileExtension =
| 'dat'

export function FileSystemSelector(props: FileSystemSelectorProps) {
  const { appContext, type, selectedPath, onSelection, id, fileFilter, disabled } = props;
  const directorySelectionApiClient = useMemo(() => new FileSystemSelectionApiClient(appContext.commandFetcher), []);
  const splitPath = selectedPath?.split(/\\|\//);
  const selectedItem = splitPath
    ? `/${splitPath.pop()}`
    : undefined;
  const displayPath = splitPath?.join('/');
  
  return <FileSystemSelectorWrapper>
    <ButtonWrapper>
      <Button 
        id={id}
        size='Small'
        variant='Outline'
        icon={type === 'Directory'
          ? 'folder'
          : 'file'}
        disabled={disabled}
        onClick={selectDirectory}
        >
        Choose {type}
      </Button>
    </ButtonWrapper>
    <PathDisplay>{displayPath}</PathDisplay>
    <SelectedItemDisplay>{selectedItem}</SelectedItemDisplay>
  </FileSystemSelectorWrapper>

  async function selectDirectory() {
    const response = await directorySelectionApiClient.execute({ 
      selectionType: type,
      fileFilter: !!fileFilter
        ? { name: fileFilter.name, allowedExtensions: fileFilter.allowedExtensions }
        : undefined
    });
    if(response.path && response.path != selectedPath)
      onSelection(response.path ?? undefined);
  }
}

const FileSystemSelectorWrapper = styled.div`
  display: flex;
  align-items: baseline;
`

const ButtonWrapper = styled.div`
  flex: 0 0 auto;
`

const PathDisplay = styled.span`
  padding-left: 16px;
  flex: 0 1 auto;
  text-overflow: ellipsis;
  white-space: nowrap;
  overflow: hidden;
`

const SelectedItemDisplay = styled.span`
  flex: 1 0 auto;
`