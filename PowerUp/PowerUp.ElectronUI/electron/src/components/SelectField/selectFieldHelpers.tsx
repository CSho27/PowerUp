import { KeyedCode } from "../../app/shared/keyedCode";
import { SimpleCode } from "../../app/shared/simpleCode";
import { ListboxOption, ListboxOptionProps } from "@reach/listbox";
import { ReactElement } from "react";

export type OptionCode = KeyedCode | SimpleCode;

function isKeyedCode(code: OptionCode): code is KeyedCode {
  return (code as KeyedCode).key !== undefined
}

function isSimpleCode(code: OptionCode): code is SimpleCode {
  return (code as SimpleCode).id !== undefined
}

export function toOption(code: OptionCode) {
  if(isKeyedCode(code))
    return <ListboxOption key={code.key} value={code.key}>{code.name}</ListboxOption>
  if(isSimpleCode(code))
    return <ListboxOption key={code.id} value={code.id.toString()}>{code.name}</ListboxOption>

  throw 'Invalid OptionCode type';
}

export function toOptions(codes: KeyedCode[] | SimpleCode[], icludeEmptyOption?: boolean) {
  const options = codes.map(toOption);
  return icludeEmptyOption
    ? withEmptyOption(options)
    : options;
}

export function withEmptyOption(options: ReactElement<ListboxOptionProps>[]): ReactElement<ListboxOptionProps>[] {
  return [<ListboxOption key='' value='None' style={{ color: 'transparent' }}>None</ListboxOption>, ...options]
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
