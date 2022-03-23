import { HotZonePreference } from "./hotZoneGrid";

export interface HotZoneGridDto {
  upAndIn: HotZonePreference;
  up: HotZonePreference;
  upAndAway: HotZonePreference;
  middleIn: HotZonePreference;
  middle: HotZonePreference;
  middleAway: HotZonePreference;
  downAndIn: HotZonePreference;
  down: HotZonePreference;
  downAndAway: HotZonePreference;
}