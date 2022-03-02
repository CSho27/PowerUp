import { KeyedCode } from "../../app/shared/keyedCode";

export function toOption(code: KeyedCode) {
  return <option key={code.key} value={code.key}>{code.name}</option>
}

export function toOptions(codes: KeyedCode[]) {
  return codes.map(toOption);
}

export function toKeyedCode(options: KeyedCode[], value: string): KeyedCode {
  const keyedCode = tryToKeyedCode(options, value);
  if(!keyedCode)
    throw `'${value}' not found in options`;
  
  return keyedCode;
}

export function tryToKeyedCode(options: KeyedCode[], value: string): KeyedCode | undefined {
  return options.find(o => o.key === value);
}
