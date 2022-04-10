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

export function toOptions(codes: KeyedCode[] | SimpleCode[], icludeEmptyOption?: boolean) {
  const options = codes.map(toOption);
  return icludeEmptyOption
    ? [<option key='' value=''></option>, ...options]
    : options;
}

export function toKeyedCode<TKeyedCode extends KeyedCode>(options: TKeyedCode[], value: string): TKeyedCode {
  const keyedCode = tryToKeyedCode(options, value);
  if(!keyedCode)
    throw `'${value}' not found in options`;
  
  return keyedCode;
}

export function tryToKeyedCode<TKeyedCode extends KeyedCode>(options: TKeyedCode[], value: string): TKeyedCode | undefined {
  return options.find(o => o.key === value);
}

export function toSimpleCode<TSimpleCode extends SimpleCode>(options: TSimpleCode[], value: string): TSimpleCode {
  const simpleCode = tryToSimpleCode(options, value);
  if(!simpleCode)
    throw `'${value}' not found in options`;
  
  return simpleCode;
}

export function tryToSimpleCode<TSimpleCode extends SimpleCode>(options: TSimpleCode[], value: string): TSimpleCode | undefined {
  return options.find(o => o.id.toString() === value);
}

export function fromOptions<TOptionCode extends OptionCode>(options: TOptionCode[], value: string): TOptionCode {
  const code = tryFromOptions(options, value);
  if(!code)
    throw `'${value}' not found in options`;

  return code;
}

export function tryFromOptions<TOptionCode extends OptionCode>(options: TOptionCode[], value: string): TOptionCode | undefined {
  if(options.length === 0)
    return undefined;
  
  if(options.every(isKeyedCode))
    return options.find(o => (o as KeyedCode).key === value);
  if(options.every(isSimpleCode))
    return options.find(o => (o as SimpleCode).id.toString() === value);
}
