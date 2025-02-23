import { FileFilter, FileSelector,  } from "../../components/fileSelector/fileSelector";
import { useAppContext } from "../appContext";

export interface FileSelectionInputProps {
  file: File | undefined;
  onSelection: (path: File | null) => void;
  id?: string;
  fileFilter?: FileFilter;
  disabled?: boolean;
}

export function FileSelectionInput({ 
  file, 
  onSelection,
  disabled, 
  fileFilter, 
  id
}: FileSelectionInputProps) {
  const { openFileSelector } = useAppContext();
  return <FileSelector
    file={file}
    onOpen={openFileSelector}
    onSelection={onSelection}
    disabled={disabled}
    fileFilter={fileFilter}
    id={id}
  />
}