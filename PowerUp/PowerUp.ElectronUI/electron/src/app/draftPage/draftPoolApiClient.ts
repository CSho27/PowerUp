import { CommandFetcher } from "../../utils/commandFetcher";
import { PlayerDetailsResponse } from "../teamEditor/playerDetailsResponse";

export interface DraftPoolGenerationRequest {
  size: number;
}

export interface DraftPoolGenerationResponse {
  players: PlayerDetailsResponse[];
}


export class DraftPoolApiClient {
  private readonly commandName = 'DraftPoolGeneration';
  private readonly commandFetcher: CommandFetcher;

  constructor(commandFetcher: CommandFetcher) {
    this.commandFetcher = commandFetcher;
  }

  execute = (request: DraftPoolGenerationRequest): Promise<DraftPoolGenerationResponse> => {
    //return new Promise(r => r(testPage));
    return this.commandFetcher.execute(this.commandName, request, false);
  }
}

const testPage: DraftPoolGenerationResponse = {
  "players": [
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40380,
          "fullName": "Rich Gale",
          "savedName": "R.Gale",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 74,
          "batsAndThrows": "R/R",
          "pitcherType": "SP",
          "throwingArm": "R",
          "topSpeed": "92mph",
          "control": "107",
          "stamina": "158"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40381,
          "fullName": "Rocky Nelson",
          "savedName": "R.Nelson",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "FirstBase",
          "positionAbbreviation": "1B",
          "overall": 79,
          "batsAndThrows": "L/L",
          "pitcherType": "SM",
          "throwingArm": "L",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40382,
          "fullName": "Jay Howell",
          "savedName": "J.Howell",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 77,
          "batsAndThrows": "R/R",
          "pitcherType": "RP",
          "throwingArm": "R",
          "topSpeed": "93mph",
          "control": "157",
          "stamina": "85"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40383,
          "fullName": "Bill Greif",
          "savedName": "B.Greif",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 76,
          "batsAndThrows": "R/R",
          "pitcherType": "SP",
          "throwingArm": "R",
          "topSpeed": "93mph",
          "control": "138",
          "stamina": "120"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40384,
          "fullName": "Earl Averill",
          "savedName": "E.Averill",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Contact",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_RunSpeed",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Catcher",
          "positionAbbreviation": "C",
          "overall": 68,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40385,
          "fullName": "Gregor Blanco",
          "savedName": "G.Blanco",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "RightField",
          "positionAbbreviation": "RF",
          "overall": 78,
          "batsAndThrows": "L/L",
          "pitcherType": "SM",
          "throwingArm": "L",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40386,
          "fullName": "Keith Hughes",
          "savedName": "K.Hughes",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Contact",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_RunSpeed",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_ErrorResistance",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "LeftField",
          "positionAbbreviation": "LF",
          "overall": 68,
          "batsAndThrows": "L/L",
          "pitcherType": "SM",
          "throwingArm": "L",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40387,
          "fullName": "Les Mallon",
          "savedName": "L.Mallon",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "NoHomeRuns",
                  "message": "No home-runs hit by player"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "SecondBase",
          "positionAbbreviation": "2B",
          "overall": 75,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40388,
          "fullName": "James Baldwin",
          "savedName": "J.Baldwin",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 75,
          "batsAndThrows": "R/R",
          "pitcherType": "RP",
          "throwingArm": "R",
          "topSpeed": "91mph",
          "control": "156",
          "stamina": "98"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40389,
          "fullName": "Kirk Gibson",
          "savedName": "K.Gibson",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "CenterField",
          "positionAbbreviation": "CF",
          "overall": 83,
          "batsAndThrows": "L/L",
          "pitcherType": "SM",
          "throwingArm": "L",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40390,
          "fullName": "Dib Williams",
          "savedName": "D.Williams",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "ThirdBase",
          "positionAbbreviation": "3B",
          "overall": 77,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40391,
          "fullName": "Tom Umphlett",
          "savedName": "T.Umphlett",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "CenterField",
          "positionAbbreviation": "CF",
          "overall": 75,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40392,
          "fullName": "Tony Cloninger",
          "savedName": "Cloninger",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 86,
          "batsAndThrows": "R/R",
          "pitcherType": "SP",
          "throwingArm": "R",
          "topSpeed": "94mph",
          "control": "129",
          "stamina": "197"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40393,
          "fullName": "Frank Welch",
          "savedName": "F.Welch",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Contact",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_RunSpeed",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_ErrorResistance",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "RightField",
          "positionAbbreviation": "RF",
          "overall": 68,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40394,
          "fullName": "Roger Bresnahan",
          "savedName": "Bresnahan",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "NoHomeRuns",
                  "message": "No home-runs hit by player"
              },
              {
                  "propertyKey": "HitterAbilities_ArmStrength",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Catcher",
          "positionAbbreviation": "C",
          "overall": 74,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40395,
          "fullName": "Bobby Young",
          "savedName": "B.Young",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "SecondBase",
          "positionAbbreviation": "2B",
          "overall": 74,
          "batsAndThrows": "L/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40396,
          "fullName": "Dana Fillingim",
          "savedName": "Fillingim",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 71,
          "batsAndThrows": "L/R",
          "pitcherType": "SP",
          "throwingArm": "R",
          "topSpeed": "89mph",
          "control": "149",
          "stamina": "143"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40397,
          "fullName": "Stan Lopata",
          "savedName": "S.Lopata",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Catcher",
          "positionAbbreviation": "C",
          "overall": 89,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40398,
          "fullName": "Lyn Lary",
          "savedName": "L.Lary",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Shortstop",
          "positionAbbreviation": "SS",
          "overall": 81,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40399,
          "fullName": "Yennier Cano",
          "savedName": "Y.Cano",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "BattingAverage",
                  "errorKey": "NoHittingStats",
                  "message": "Hitting stats not found"
              },
              {
                  "propertyKey": "RunsBattedIn",
                  "errorKey": "NoHittingStats",
                  "message": "Hitting stats not found"
              },
              {
                  "propertyKey": "HomeRuns",
                  "errorKey": "NoHittingStats",
                  "message": "Hitting stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 61,
          "batsAndThrows": "R/R",
          "pitcherType": "RP",
          "throwingArm": "R",
          "topSpeed": "97mph",
          "control": "60",
          "stamina": "89"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40400,
          "fullName": "Jesse Barnes",
          "savedName": "J.Barnes",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 76,
          "batsAndThrows": "L/R",
          "pitcherType": "SP",
          "throwingArm": "R",
          "topSpeed": "90mph",
          "control": "166",
          "stamina": "149"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40401,
          "fullName": "Lou Klimchock",
          "savedName": "Klimchock",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Contact",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_RunSpeed",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_ErrorResistance",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "FirstBase",
          "positionAbbreviation": "1B",
          "overall": 67,
          "batsAndThrows": "L/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40402,
          "fullName": "Mike Donlin",
          "savedName": "M.Donlin",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "FirstBase",
          "positionAbbreviation": "1B",
          "overall": 86,
          "batsAndThrows": "L/L",
          "pitcherType": "SM",
          "throwingArm": "L",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40403,
          "fullName": "Gabby Hartnett",
          "savedName": "G.Hartnett",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_ArmStrength",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Catcher",
          "positionAbbreviation": "C",
          "overall": 80,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40404,
          "fullName": "Mark Grace",
          "savedName": "M.Grace",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "FirstBase",
          "positionAbbreviation": "1B",
          "overall": 87,
          "batsAndThrows": "L/L",
          "pitcherType": "SM",
          "throwingArm": "L",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40405,
          "fullName": "Lew McCarty",
          "savedName": "L.McCarty",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "NoHomeRuns",
                  "message": "No home-runs hit by player"
              },
              {
                  "propertyKey": "HitterAbilities_ArmStrength",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Catcher",
          "positionAbbreviation": "C",
          "overall": 71,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40406,
          "fullName": "Alec Asher",
          "savedName": "A.Asher",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 87,
          "batsAndThrows": "R/R",
          "pitcherType": "RP",
          "throwingArm": "R",
          "topSpeed": "91mph",
          "control": "182",
          "stamina": "147"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40407,
          "fullName": "Bob Nieman",
          "savedName": "B.Nieman",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "LeftField",
          "positionAbbreviation": "LF",
          "overall": 86,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40408,
          "fullName": "Todd Jones",
          "savedName": "T.Jones",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 68,
          "batsAndThrows": "L/R",
          "pitcherType": "CP",
          "throwingArm": "R",
          "topSpeed": "94mph",
          "control": "129",
          "stamina": "84"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40409,
          "fullName": "Curt Simmons",
          "savedName": "C.Simmons",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 73,
          "batsAndThrows": "L/L",
          "pitcherType": "SP",
          "throwingArm": "L",
          "topSpeed": "93mph",
          "control": "130",
          "stamina": "114"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40410,
          "fullName": "Zack Britton",
          "savedName": "Z.Britton",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 79,
          "batsAndThrows": "L/L",
          "pitcherType": "CP",
          "throwingArm": "L",
          "topSpeed": "94mph",
          "control": "152",
          "stamina": "85"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40411,
          "fullName": "Steve Kraly",
          "savedName": "S.Kraly",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 70,
          "batsAndThrows": "L/L",
          "pitcherType": "RP",
          "throwingArm": "L",
          "topSpeed": "90mph",
          "control": "89",
          "stamina": "140"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40412,
          "fullName": "Paul Ratliff",
          "savedName": "P.Ratliff",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Contact",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_RunSpeed",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_ErrorResistance",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Catcher",
          "positionAbbreviation": "C",
          "overall": 67,
          "batsAndThrows": "L/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40413,
          "fullName": "Elam Vangilder",
          "savedName": "Vangilder",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 69,
          "batsAndThrows": "R/R",
          "pitcherType": "SP",
          "throwingArm": "R",
          "topSpeed": "89mph",
          "control": "107",
          "stamina": "134"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40414,
          "fullName": "Al Kaiser",
          "savedName": "A.Kaiser",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Contact",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_RunSpeed",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_Fielding",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              },
              {
                  "propertyKey": "HitterAbilities_ErrorResistance",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "LeftField",
          "positionAbbreviation": "LF",
          "overall": 68,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40415,
          "fullName": "Jesse Carlson",
          "savedName": "J.Carlson",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 74,
          "batsAndThrows": "L/L",
          "pitcherType": "RP",
          "throwingArm": "L",
          "topSpeed": "94mph",
          "control": "151",
          "stamina": "83"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40416,
          "fullName": "Larry Haney",
          "savedName": "L.Haney",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Catcher",
          "positionAbbreviation": "C",
          "overall": 80,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40417,
          "fullName": "Kevin Melillo",
          "savedName": "K.Melillo",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Contact",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_RunSpeed",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_ArmStrength",
                  "errorKey": "NoFieldingStats",
                  "message": "Fielding stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Fielding",
                  "errorKey": "NoFieldingStats",
                  "message": "Fielding stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_ErrorResistance",
                  "errorKey": "NoFieldingStats",
                  "message": "Fielding stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "SecondBase",
          "positionAbbreviation": "2B",
          "overall": 68,
          "batsAndThrows": "L/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40418,
          "fullName": "Del Pratt",
          "savedName": "D.Pratt",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "SecondBase",
          "positionAbbreviation": "2B",
          "overall": 79,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40419,
          "fullName": "Bill Hallman",
          "savedName": "B.Hallman",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "NoHomeRuns",
                  "message": "No home-runs hit by player"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "RightField",
          "positionAbbreviation": "RF",
          "overall": 69,
          "batsAndThrows": "L/L",
          "pitcherType": "SM",
          "throwingArm": "L",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40420,
          "fullName": "Emerson Dickman",
          "savedName": "E.Dickman",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 71,
          "batsAndThrows": "R/R",
          "pitcherType": "RP",
          "throwingArm": "R",
          "topSpeed": "90mph",
          "control": "138",
          "stamina": "110"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40421,
          "fullName": "Mark Gubicza",
          "savedName": "M.Gubicza",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 79,
          "batsAndThrows": "R/R",
          "pitcherType": "SP",
          "throwingArm": "R",
          "topSpeed": "93mph",
          "control": "150",
          "stamina": "153"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40422,
          "fullName": "Bob Gibson",
          "savedName": "B.Gibson",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 94,
          "batsAndThrows": "R/R",
          "pitcherType": "SP",
          "throwingArm": "R",
          "topSpeed": "94mph",
          "control": "166",
          "stamina": "204"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40423,
          "fullName": "Hooks Dauss",
          "savedName": "H.Dauss",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 82,
          "batsAndThrows": "R/R",
          "pitcherType": "SP",
          "throwingArm": "R",
          "topSpeed": "89mph",
          "control": "163",
          "stamina": "210"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40424,
          "fullName": "Roberto Hernandez",
          "savedName": "Hernandez",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 73,
          "batsAndThrows": "R/R",
          "pitcherType": "RP",
          "throwingArm": "R",
          "topSpeed": "96mph",
          "control": "134",
          "stamina": "85"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40425,
          "fullName": "Jacob deGrom",
          "savedName": "J.deGrom",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 92,
          "batsAndThrows": "L/R",
          "pitcherType": "SP",
          "throwingArm": "R",
          "topSpeed": "97mph",
          "control": "154",
          "stamina": "185"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40426,
          "fullName": "Glenn Crawford",
          "savedName": "G.Crawford",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Shortstop",
          "positionAbbreviation": "SS",
          "overall": 80,
          "batsAndThrows": "L/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40427,
          "fullName": "Sparky Lyle",
          "savedName": "S.Lyle",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 78,
          "batsAndThrows": "L/L",
          "pitcherType": "CP",
          "throwingArm": "L",
          "topSpeed": "93mph",
          "control": "158",
          "stamina": "96"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40428,
          "fullName": "Gary Thomasson",
          "savedName": "Thomasson",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "CenterField",
          "positionAbbreviation": "CF",
          "overall": 83,
          "batsAndThrows": "L/L",
          "pitcherType": "SM",
          "throwingArm": "L",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40429,
          "fullName": "Hector Lopez",
          "savedName": "H.Lopez",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "RightField",
          "positionAbbreviation": "RF",
          "overall": 79,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40430,
          "fullName": "Tom Prince",
          "savedName": "T.Prince",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Contact",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_RunSpeed",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_ErrorResistance",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Catcher",
          "positionAbbreviation": "C",
          "overall": 67,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40431,
          "fullName": "Marcell Ozuna",
          "savedName": "M.Ozuna",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "LeftField",
          "positionAbbreviation": "LF",
          "overall": 85,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40432,
          "fullName": "Ben Chapman",
          "savedName": "B.Chapman",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "CenterField",
          "positionAbbreviation": "CF",
          "overall": 83,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40433,
          "fullName": "Tom Murphy",
          "savedName": "T.Murphy",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 69,
          "batsAndThrows": "R/R",
          "pitcherType": "CP",
          "throwingArm": "R",
          "topSpeed": "91mph",
          "control": "139",
          "stamina": "90"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40434,
          "fullName": "Richie Martin",
          "savedName": "R.Martin",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Shortstop",
          "positionAbbreviation": "SS",
          "overall": 77,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40435,
          "fullName": "Jerry Buchek",
          "savedName": "J.Buchek",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Contact",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_RunSpeed",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_ErrorResistance",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Shortstop",
          "positionAbbreviation": "SS",
          "overall": 68,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40436,
          "fullName": "Elmer Rieger",
          "savedName": "E.Rieger",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 66,
          "batsAndThrows": "S/R",
          "pitcherType": "RP",
          "throwingArm": "R",
          "topSpeed": "91mph",
          "control": "147",
          "stamina": "93"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40437,
          "fullName": "Kyle Graham",
          "savedName": "K.Graham",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 62,
          "batsAndThrows": "R/R",
          "pitcherType": "RP",
          "throwingArm": "R",
          "topSpeed": "88mph",
          "control": "89",
          "stamina": "125"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40438,
          "fullName": "Scott Bankhead",
          "savedName": "S.Bankhead",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 71,
          "batsAndThrows": "R/R",
          "pitcherType": "RP",
          "throwingArm": "R",
          "topSpeed": "91mph",
          "control": "132",
          "stamina": "97"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40439,
          "fullName": "Eddie Bonine",
          "savedName": "E.Bonine",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "BattingAverage",
                  "errorKey": "NoHittingStats",
                  "message": "Hitting stats not found"
              },
              {
                  "propertyKey": "RunsBattedIn",
                  "errorKey": "NoHittingStats",
                  "message": "Hitting stats not found"
              },
              {
                  "propertyKey": "HomeRuns",
                  "errorKey": "NoHittingStats",
                  "message": "Hitting stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 73,
          "batsAndThrows": "R/R",
          "pitcherType": "RP",
          "throwingArm": "R",
          "topSpeed": "92mph",
          "control": "143",
          "stamina": "118"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40440,
          "fullName": "John Kerr",
          "savedName": "J.Kerr",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "NoHomeRuns",
                  "message": "No home-runs hit by player"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "ThirdBase",
          "positionAbbreviation": "3B",
          "overall": 73,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40441,
          "fullName": "Harry Gleason",
          "savedName": "H.Gleason",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Contact",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_RunSpeed",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_ErrorResistance",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "ThirdBase",
          "positionAbbreviation": "3B",
          "overall": 68,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40442,
          "fullName": "Bill Voiselle",
          "savedName": "B.Voiselle",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 79,
          "batsAndThrows": "R/R",
          "pitcherType": "SP",
          "throwingArm": "R",
          "topSpeed": "91mph",
          "control": "131",
          "stamina": "166"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40443,
          "fullName": "Russ Springer",
          "savedName": "R.Springer",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 72,
          "batsAndThrows": "R/R",
          "pitcherType": "RP",
          "throwingArm": "R",
          "topSpeed": "93mph",
          "control": "144",
          "stamina": "84"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40444,
          "fullName": "Mike Caldwell",
          "savedName": "M.Caldwell",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 75,
          "batsAndThrows": "R/L",
          "pitcherType": "RP",
          "throwingArm": "L",
          "topSpeed": "91mph",
          "control": "174",
          "stamina": "100"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40445,
          "fullName": "Jumbo Elliott",
          "savedName": "J.Elliott",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 78,
          "batsAndThrows": "R/L",
          "pitcherType": "SP",
          "throwingArm": "L",
          "topSpeed": "90mph",
          "control": "146",
          "stamina": "143"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40446,
          "fullName": "Howie Fox",
          "savedName": "H.Fox",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 80,
          "batsAndThrows": "R/R",
          "pitcherType": "SP",
          "throwingArm": "R",
          "topSpeed": "89mph",
          "control": "152",
          "stamina": "167"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40447,
          "fullName": "Ryne Duren",
          "savedName": "R.Duren",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 72,
          "batsAndThrows": "R/R",
          "pitcherType": "RP",
          "throwingArm": "R",
          "topSpeed": "95mph",
          "control": "97",
          "stamina": "107"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40448,
          "fullName": "Steve Bedrosian",
          "savedName": "Bedrosian",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 65,
          "batsAndThrows": "R/R",
          "pitcherType": "CP",
          "throwingArm": "R",
          "topSpeed": "92mph",
          "control": "105",
          "stamina": "86"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40449,
          "fullName": "Don Ross",
          "savedName": "D.Ross",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "ThirdBase",
          "positionAbbreviation": "3B",
          "overall": 77,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40450,
          "fullName": "Larry Gura",
          "savedName": "L.Gura",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 75,
          "batsAndThrows": "S/L",
          "pitcherType": "RP",
          "throwingArm": "L",
          "topSpeed": "91mph",
          "control": "159",
          "stamina": "99"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40451,
          "fullName": "Clayton Kershaw",
          "savedName": "C.Kershaw",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "BattingAverage",
                  "errorKey": "NoHittingStats",
                  "message": "Hitting stats not found"
              },
              {
                  "propertyKey": "RunsBattedIn",
                  "errorKey": "NoHittingStats",
                  "message": "Hitting stats not found"
              },
              {
                  "propertyKey": "HomeRuns",
                  "errorKey": "NoHittingStats",
                  "message": "Hitting stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 91,
          "batsAndThrows": "L/L",
          "pitcherType": "RP",
          "throwingArm": "L",
          "topSpeed": "96mph",
          "control": "183",
          "stamina": "151"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40452,
          "fullName": "Dennis Powell",
          "savedName": "D.Powell",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 68,
          "batsAndThrows": "R/L",
          "pitcherType": "RP",
          "throwingArm": "L",
          "topSpeed": "93mph",
          "control": "115",
          "stamina": "90"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40453,
          "fullName": "Rob Zastryzny",
          "savedName": "Zastryzny",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 80,
          "batsAndThrows": "R/L",
          "pitcherType": "RP",
          "throwingArm": "L",
          "topSpeed": "96mph",
          "control": "150",
          "stamina": "98"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40454,
          "fullName": "Nelson Briles",
          "savedName": "N.Briles",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 78,
          "batsAndThrows": "R/R",
          "pitcherType": "SP",
          "throwingArm": "R",
          "topSpeed": "92mph",
          "control": "159",
          "stamina": "121"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40455,
          "fullName": "Red Ames",
          "savedName": "R.Ames",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 80,
          "batsAndThrows": "S/R",
          "pitcherType": "SP",
          "throwingArm": "R",
          "topSpeed": "92mph",
          "control": "159",
          "stamina": "174"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40456,
          "fullName": "Lefty Tyler",
          "savedName": "L.Tyler",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 80,
          "batsAndThrows": "L/L",
          "pitcherType": "SP",
          "throwingArm": "L",
          "topSpeed": "90mph",
          "control": "153",
          "stamina": "201"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40457,
          "fullName": "Mike Munoz",
          "savedName": "M.Munoz",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 60,
          "batsAndThrows": "L/L",
          "pitcherType": "RP",
          "throwingArm": "L",
          "topSpeed": "94mph",
          "control": "75",
          "stamina": "80"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40458,
          "fullName": "Andy Karl",
          "savedName": "A.Karl",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 68,
          "batsAndThrows": "R/R",
          "pitcherType": "RP",
          "throwingArm": "R",
          "topSpeed": "89mph",
          "control": "146",
          "stamina": "93"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40459,
          "fullName": "Bill McAfee",
          "savedName": "B.McAfee",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 71,
          "batsAndThrows": "R/R",
          "pitcherType": "RP",
          "throwingArm": "R",
          "topSpeed": "89mph",
          "control": "109",
          "stamina": "142"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40460,
          "fullName": "Red Barrett",
          "savedName": "R.Barrett",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 81,
          "batsAndThrows": "R/R",
          "pitcherType": "SP",
          "throwingArm": "R",
          "topSpeed": "89mph",
          "control": "158",
          "stamina": "162"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40461,
          "fullName": "Ray Poat",
          "savedName": "R.Poat",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 72,
          "batsAndThrows": "R/R",
          "pitcherType": "RP",
          "throwingArm": "R",
          "topSpeed": "93mph",
          "control": "126",
          "stamina": "107"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40462,
          "fullName": "Kevin Jepsen",
          "savedName": "K.Jepsen",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 74,
          "batsAndThrows": "R/R",
          "pitcherType": "RP",
          "throwingArm": "R",
          "topSpeed": "94mph",
          "control": "136",
          "stamina": "83"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40463,
          "fullName": "George Blaeholder",
          "savedName": "Blaeholder",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 83,
          "batsAndThrows": "R/R",
          "pitcherType": "SP",
          "throwingArm": "R",
          "topSpeed": "91mph",
          "control": "163",
          "stamina": "165"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40464,
          "fullName": "Dario Lodigiani",
          "savedName": "Lodigiani",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Contact",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_RunSpeed",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_ArmStrength",
                  "errorKey": "NoFieldingStats",
                  "message": "Fielding stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Fielding",
                  "errorKey": "NoFieldingStats",
                  "message": "Fielding stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_ErrorResistance",
                  "errorKey": "NoFieldingStats",
                  "message": "Fielding stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "ThirdBase",
          "positionAbbreviation": "3B",
          "overall": 67,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40465,
          "fullName": "Al Pedrique",
          "savedName": "A.Pedrique",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Contact",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_RunSpeed",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "ThirdBase",
          "positionAbbreviation": "3B",
          "overall": 69,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40466,
          "fullName": "Aleck Smith",
          "savedName": "A.Smith",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Contact",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_RunSpeed",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_ArmStrength",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              },
              {
                  "propertyKey": "HitterAbilities_ErrorResistance",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Catcher",
          "positionAbbreviation": "C",
          "overall": 67,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40467,
          "fullName": "Andy Hassler",
          "savedName": "A.Hassler",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 83,
          "batsAndThrows": "L/L",
          "pitcherType": "SP",
          "throwingArm": "L",
          "topSpeed": "91mph",
          "control": "117",
          "stamina": "198"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40468,
          "fullName": "Bob Ewing",
          "savedName": "B.Ewing",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 79,
          "batsAndThrows": "R/R",
          "pitcherType": "SP",
          "throwingArm": "R",
          "topSpeed": "90mph",
          "control": "134",
          "stamina": "217"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40469,
          "fullName": "Cupid Childs",
          "savedName": "C.Childs",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "NoHomeRuns",
                  "message": "No home-runs hit by player"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "SecondBase",
          "positionAbbreviation": "2B",
          "overall": 72,
          "batsAndThrows": "L/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40470,
          "fullName": "Harry Malmberg",
          "savedName": "H.Malmberg",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "NoHomeRuns",
                  "message": "No home-runs hit by player"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "SecondBase",
          "positionAbbreviation": "2B",
          "overall": 69,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40471,
          "fullName": "Felix Fermin",
          "savedName": "F.Fermin",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Contact",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_RunSpeed",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Shortstop",
          "positionAbbreviation": "SS",
          "overall": 68,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40472,
          "fullName": "Joe McCarthy",
          "savedName": "J.McCarthy",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Contact",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_RunSpeed",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_ArmStrength",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              },
              {
                  "propertyKey": "HitterAbilities_ErrorResistance",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Catcher",
          "positionAbbreviation": "C",
          "overall": 67,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40473,
          "fullName": "Jon Rauch",
          "savedName": "J.Rauch",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 76,
          "batsAndThrows": "R/R",
          "pitcherType": "RP",
          "throwingArm": "R",
          "topSpeed": "93mph",
          "control": "144",
          "stamina": "111"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40474,
          "fullName": "Bill Wambsganss",
          "savedName": "Wambsganss",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "NoHomeRuns",
                  "message": "No home-runs hit by player"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "SecondBase",
          "positionAbbreviation": "2B",
          "overall": 73,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40475,
          "fullName": "Ozzie Guillen",
          "savedName": "O.Guillen",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "Shortstop",
          "positionAbbreviation": "SS",
          "overall": 79,
          "batsAndThrows": "L/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40476,
          "fullName": "Bob Weiland",
          "savedName": "B.Weiland",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 70,
          "batsAndThrows": "L/L",
          "pitcherType": "SP",
          "throwingArm": "L",
          "topSpeed": "90mph",
          "control": "116",
          "stamina": "140"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40477,
          "fullName": "Art Schult",
          "savedName": "A.Schult",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_Contact",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_Power",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_RunSpeed",
                  "errorKey": "InsufficentHittingStats",
                  "message": "Insufficient hitting stats found"
              },
              {
                  "propertyKey": "HitterAbilities_Fielding",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              },
              {
                  "propertyKey": "HitterAbilities_ErrorResistance",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "LeftField",
          "positionAbbreviation": "LF",
          "overall": 68,
          "batsAndThrows": "R/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40478,
          "fullName": "Lewis Wiltse",
          "savedName": "L.Wiltse",
          "generatedPlayer_Warnings": [],
          "generatedPlayer_IsUnedited": true,
          "position": "Pitcher",
          "positionAbbreviation": "P",
          "overall": 75,
          "batsAndThrows": "R/L",
          "pitcherType": "RP",
          "throwingArm": "L",
          "topSpeed": "89mph",
          "control": "164",
          "stamina": "157"
      },
      {
          "sourceType": "Generated",
          "canEdit": true,
          "playerId": 40479,
          "fullName": "Len Gabrielson",
          "savedName": "Gabrielson",
          "generatedPlayer_Warnings": [
              {
                  "propertyKey": "EarnedRunAverage",
                  "errorKey": "NoPitchingStats",
                  "message": "Pitching stats not found"
              },
              {
                  "propertyKey": "HitterAbilities_ErrorResistance",
                  "errorKey": "InsufficentFieldingStats",
                  "message": "Insufficient fielding stats found"
              }
          ],
          "generatedPlayer_IsUnedited": true,
          "position": "LeftField",
          "positionAbbreviation": "LF",
          "overall": 80,
          "batsAndThrows": "L/R",
          "pitcherType": "SM",
          "throwingArm": "R",
          "topSpeed": "74mph",
          "control": "0",
          "stamina": "0"
      }
  ]
}