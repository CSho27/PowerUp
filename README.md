# PowerUp
![PowerUp_Splashscreen](https://user-images.githubusercontent.com/30477054/160299027-774f51e4-43ff-45c5-af74-cf7471e844d0.png)

## Overview
PowerUp is a Roster Generation and Editing Tool For MLB PowerPros (2007). Its ultimate purpose is to make it as easy as possible to create and generate accurate rosters for the game. The app stores off a database of editable players that can be placed onto teams in a roster, then exports the data into a game save file for use in the game. It is currently still in development, so many of its core features have not been implemented yet.

**NOTE**: PowerUp currently only works with Wii and Dolphin ROM game save files. There are plans to port this to PS2 in the future.

## Screenshots:
![Roster Page](https://user-images.githubusercontent.com/30477054/160299040-aea0c0b6-6dc4-43bc-90f2-72db55137a19.png)
![Player Editor - Hitter](https://user-images.githubusercontent.com/30477054/160299044-046b1ee8-11cf-4062-9927-dc95070d85cf.png)

## Use Guide
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
 - While the app is still in development, you will notice appearances, special abilities, and a few other properties will not export correctly
 - Until those features are implemented, the app will be overwriting random players, and your exports will take on the properties of the original player
 - Once all the features of the app have been completed, this problem will correct itself
 - There is no guaruntee your alpha database will work when used with newer versions of the app. App data will not be backwards compatible until v1.0.0

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
*Probably not for another few months. I'm currently working on this alone and there's a lot of work left to do.*
