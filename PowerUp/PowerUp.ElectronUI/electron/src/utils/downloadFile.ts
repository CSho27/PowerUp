export function downloadFile(file: File | Blob, name?: string): void {
  const aElement = document.createElement('a');
  aElement.setAttribute('download', name ?? (file as File).name ?? 'Untitled');
  aElement.setAttribute('target', '_blank');
  aElement.href = URL.createObjectURL(file);
  aElement.click();
  URL.revokeObjectURL(aElement.href);
}
