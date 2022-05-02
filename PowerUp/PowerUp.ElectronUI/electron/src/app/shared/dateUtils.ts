
const SHORT_DATE_TIME_OPTIONS: Intl.DateTimeFormatOptions = {
  dateStyle: 'short',
  timeStyle: 'medium'
}

export function toShortDateTimeString(date: Date, includeSeconds: boolean): string {
  return new Intl.DateTimeFormat('en-US', SHORT_DATE_TIME_OPTIONS).format(date)
}