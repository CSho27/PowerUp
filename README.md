# PowerUp
![PowerUp_Splashscreen](https://user-images.githubusercontent.com/30477054/160299027-774f51e4-43ff-45c5-af74-cf7471e844d0.png)

## Overview
PowerUp is a Roster Generation and Editing Tool For MLB PowerPros (2007). Its ultimate purpose is to make it as easy as possible to create and generate accurate rosters for the game. The app stores off a database of editable players that can be placed onto teams in a roster, then exports the data into a game save file for use in the game. It is still actively being developed, so some of its core features are still in the works.

**NOTE**: PowerUp currently only works with Wii and Dolphin ROM game save files. There are plans to port this to PS2 in the near future.

## Screenshots:
![Roster Editor with Flyout Open](https://user-images.githubusercontent.com/30477054/172521405-810a2728-d1bb-40fe-a2d5-a177ffe03687.png)
![Lineup Editor](https://user-images.githubusercontent.com/30477054/172521485-76b55bd3-8135-4371-9404-ccb71d8625d6.png)
![Pitcher Roles Editor](https://user-images.githubusercontent.com/30477054/172521498-b6b86bc9-303c-472a-988a-e3212bc35285.png)
![Player Editor - Hitter](https://user-images.githubusercontent.com/30477054/172521472-1f7290cd-7e4f-49d9-8b31-ae8a0ada6913.png)

## Use Guide
### Key Concepts
#### Entity Type
![image](https://user-images.githubusercontent.com/30477054/172523201-4bbf6d04-55c9-4c56-af42-bd93b4922c84.png)
- In order to preserve certain data in the system, there are 3 different types of players, teams, and roster:
  - Base: Included in original game. Cannot be edited.
  - Imported: Imported from another Game Save file. Cannot be edited.
  - Custom: Created by the app. Properties can be edited.
- If you wish to edit a player, team, or roster that is not editable, you can **make a copy**, which will give you an editable version of that entity.

#### Referential Data
![image](https://user-images.githubusercontent.com/30477054/172523937-01285df2-61c4-424e-9300-a35d2b92c428.png)
- Each player, team, and roster in the game has a unique id associated with it
- Rosters and Teams store *references* to their Teams and Players, which means that:
  - You can use a player you create on more than one team
  - You can use a team you create on more than one roster
  - Any time you edit a player on a team, that player will also be edited on every other team he is on
  - Any time you edit a team on a roster, that team wll also be edited on every other roster it is on
 
### Choosing a Roster to Edit
![image](https://user-images.githubusercontent.com/30477054/172524588-e6c5b3dd-0a0c-4a8b-8aec-e4f3f2a409a7.png)
- When you open the app, you will have three different options for how to open a roster to edit:
  - Open Existing Roster: This lets you choose a roster from the list of rosters saved in your system and opens whichever one you select.
  - Copy Existing Roster: This makes an editable copy of any roster that previously existed in your system, and opens it.   
  - Import Roster: This allows you to choose a GameSave file from your filesystem to import into the app, and opens it when the import is complete.

### Edit Roster
![image](https://user-images.githubusercontent.com/30477054/172524911-8df2b924-9ce2-4001-adf0-16336d94b10c.png)


- Select one of the 3 options on the home page to open a roster to edit
- To edit a player, click the edit icon next to the player on the left
- When done editing player properties, click save in the upper right
- To navigate back to the roster/home pages, use the links in the upper left
- To export an edited roster file, click the export icon in the upper right
- From here, you can select to either use a blank file or copy from an existing one. If you copy from an existing one, this will allow you to still have your settings and purchases from your original game save file. This will **not** save over your orginal game save file, instead, it will just write to a copy of that file.
- Select a directory to output your game save file
- The outputted file will have a name in the format {RosterName}.ps2maus.dat
- In order to use this roster in the game, you will need to:
  - Find the Dolphin/Wii folder where the original game save is stored 
  - **Rename or create a copy of your original game save file (DONT OVERWITE YOUR MAIN GAME SAVE)**
  - Delete the Roster name prefix from the beginning of the file name
  - Place it in the same folder where you found your original game save file
- In order for the game to load the new data, you will have to restart the game if it is already running

Alpha Notes:
 - There is no guaruntee your alpha database will work when used with newer versions of the app. App data will not be backwards compatible until v1.0.0.
 - You *will* be able to import exported game saves from alpha versions.

## FAQ:
1. Will the rosters work in season mode? <br/>
*Yes, you will be able to use the rosters in season mode _however_ the ages of the players, their contracts, and the number of years they've been in the majors will all be off, so you may see some players retiring early and some erratic behavior from managers signing players. Draft classes will also be unedited.*

2. Are you able to use the players to make custom teams? <br/>
*Not currently, but I hope to add that ability in the future*

3. Will I be able to use the exported GameSaves on PS2 <br/>
*Not currently, but I hope to give you the option in the future to either export game saves in Wii or PS2 format*

4. Will exported GameSaves work in the 2008 version of the game <br/>
*No, the game saves will be in a different format. I will look into the possibility of supporting 2008 game saves in the future, but it may not be possible.*

5. When will the full version be done? <br/>
*Probably not until August or September 2022. I'm currently working on this alone and there's a lot of work left to do.*
