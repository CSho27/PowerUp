import { useRef } from "react";
import styled from "styled-components";
import { Button } from "../button/button";
import { FileSystemSelectionApiClient, FileSystemSelectionType } from "./fileSystemSelectionApiClient";

export interface FileSystemSelectorProps {
  type: FileSystemSelectionType;
  selectedPath: string | undefined;
  onSelection: (path: string | undefined) => void;
  id?: string;
}

export function FileSystemSelector(props: FileSystemSelectorProps) {
  const { id, type, selectedPath, onSelection } = props;
  const directorySelectionApiClientRef = useRef(new FileSystemSelectionApiClient());
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
        onClick={selectDirectory}
        >
        Choose {type}
      </Button>
    </ButtonWrapper>
    <PathDisplay>{displayPath}</PathDisplay>
    <SelectedItemDisplay>{selectedItem}</SelectedItemDisplay>
  </FileSystemSelectorWrapper>

  async function selectDirectory() {
    const response = await directorySelectionApiClientRef.current.execute({ selectionType: type });
    if(response.path != selectedPath)
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