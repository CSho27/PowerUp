import { KeyedCode } from "../../app/shared/keyedCode";
import { SimpleCode } from "../../app/shared/simpleCode";

export type OptionCode = KeyedCode | SimpleCode;

function isKeyedCode(code: OptionCode): code is KeyedCode {
  return (code as KeyedCode).key !== undefined
}

function isSimpleCode(code: OptionCode): code is SimpleCode {
  return (code as SimpleCode).id !== undefined
}

export function toOption(code: OptionCode) {
  if(isKeyedCode(code))
    return <option key={code.key} value={code.key}>{code.name}</option>
  if(isSimpleCode(code))
    return <option key={code.id} value={code.id.toString()}>{code.name}</option>

  throw 'Invalid OptionCode type';
}

export function toOptions(codes: KeyedCode[] | SimpleCode[]) {
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

export function toSimpleCode(options: SimpleCode[], value: string): SimpleCode {
  const simpleCode = tryToSimpleCode(options, value);
  if(!simpleCode)
    throw `'${value}' not found in options`;
  
  return simpleCode;
}

export function tryToSimpleCode(options: SimpleCode[], value: string): SimpleCode | undefined {
  return options.find(o => o.id.toString() === value);
}
