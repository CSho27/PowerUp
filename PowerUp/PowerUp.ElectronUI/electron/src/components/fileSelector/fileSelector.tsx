import styled from "styled-components";
import { Button } from "../button/button";

export type FileSelectionFn = (filter?: FileFilter) => Promise<File | null>;

export interface FileSelectorProps {
  file: File | undefined;
  onOpen: FileSelectionFn;
  onSelection: (path: File | null) => void;
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

export function FileSelector(props: FileSelectorProps) {
  const { file, onOpen, onSelection, id, fileFilter, disabled } = props;
  
  return <FileSystemSelectorWrapper>
    <ButtonWrapper>
      <Button 
        id={id}
        size='Small'
        variant='Outline'
        icon='file'
        disabled={disabled}
        onClick={selectDirectory}
        >
        Choose File
      </Button>
    </ButtonWrapper>
    <SelectedItemDisplay>{file?.name}</SelectedItemDisplay>
  </FileSystemSelectorWrapper>

  async function selectDirectory() {
    const response = await onOpen(fileFilter);
    if(!!response)
      onSelection(response);
  }
}

const FileSystemSelectorWrapper = styled.div`
  display: flex;
  align-items: baseline;
`

const ButtonWrapper = styled.div`
  flex: 0 0 auto;
`

const SelectedItemDisplay = styled.span`
  flex: 1 0 auto;
`