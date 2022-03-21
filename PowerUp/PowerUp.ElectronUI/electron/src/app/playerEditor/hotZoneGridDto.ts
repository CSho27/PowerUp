export interface HotZoneGridDto {
  upAndIn: HotZonePreference;
  up: HotZonePreference;
  upAndAway: HotZonePreference;
  middleIn: HotZonePreference;
  middle: HotZonePreference;
  middleAway: HotZonePreference;
  lowAndIn: HotZonePreference;
  low: HotZonePreference;
  lowAndAway: HotZonePreference;
}

export type HotZonePreference = 'Neutral' | 'Hot' | 'Cold';