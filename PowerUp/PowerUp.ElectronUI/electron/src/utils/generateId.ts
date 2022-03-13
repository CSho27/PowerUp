
let currentId = 0;
export function GenerateId(): number {
  currentId++;
  return currentId;
}