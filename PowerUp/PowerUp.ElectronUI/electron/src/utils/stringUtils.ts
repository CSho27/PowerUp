export function trim(value: string, trimStr = '\\s'): string {
  const regex = new RegExp(`^(${trimStr})+|(${trimStr})+$`, 'g');
  return value.replace(regex, '');
}