<h1>MLB Statistics Source Research</h1>

<h2>Overall Findings:</h2>

- There really isn't a single best source for getting the data we're looking for programatically
- It's hard to find a place that:
  - Has all the data we need
  - Has it in a format that will be easily accessible programatically
- Because of this the player generator will likely start by looking for the best possible data and will fall back to progressively more innacurate options
- Options 1-3 (since they're all run by MLB) share some amount of data and use the same list of player ids, which will make them easier to use together
- Because of this, the best strategy is probably to use some combination of Options 1-3
- The FanGraphs data could also be useful just to get accurate pitch type data for 2002-2017
<br/>

<h2>Options:</h2> 

1. <strong>lookup-service-prod.mlb.com</strong>:
- API reference: https://appac.github.io/mlb-data-api-docs
- This is what was used for the original PowerPros Roster Generator
- Has very complete historical data for players and teams
- Has basic player statistics going back a very long time
- Is missing fielding statistics for some notable players
- No longer supported by MLB, but is still up and running
<br/>

2. <strong>statsapi.mlb.com</strong>:
- API reference: http://statsapi.mlb.com/docs/ (Not publically available)
- MLB's official statistics api
- Public facing, but its docs are not for some reason
- According to MLB this API is only for the use of the MLB and its affiliates, they just put zero effort into protecting their apis from public calls
- This npm library can be used as a partial api reference https://github.com/asbeane/mlb-stats-api
- This is probably the best api available with the best chance of long term support, but without the docs it's borderline worthless
<br/>

3. <strong>baseballsavant.mlb.com</strong>:
- API reference: none
- MLB's site for displaying statcast/sabermetric data
- Will be good for creating super accurate current day players
- No public facing api, but downloading the files and parsing out the data should be fairly doable
- Look for 'download as csv' method and pick the data out of there
- Timespan:
  - sprint speed: 2015-present
  - pitch arsenal data: 2017-present
<br/>

4. <strong>BaseballReference.com</strong>:
- API reference: none
- The most complete database of baseball statistics out there
- Will be borderline impossible to get the data without just screen scraping (which will be slow)
- Even if we could find the player ids for the data we want, the best way to download the data is just to download an xls in an extremely old format
- Really cool resource, but would be unreasonably difficult to program against
<br/>

5. <strong>Retrosheet</strong>:
- This has the most in-depth historical data, but getting it in a readable form either requires interpreting game outcome files or screen scraping
- Unbelievably cool, but probably not practical for our purposes
<br/>

6. <strong>Brooks Baseball / Pitch FX</strong>:
- Has the most comprehensive statistics of pitch speed and movement (back to 2007)
- Will either require screen scraping or some serious hacking to get at the data
- Data is being pulled from somewhere public but I don't know where to find it
<br/>

7. <strong>FanGraphs</strong>:
- Has a surprising amount of data publically available it's easier to get at than baseball reference
- Inludes statcast/pitchFx style data that will be needed for more accurate player generation
- Might be difficult to search because the player ids wont match up with the mlb ones
- Search method also should not be difficult to call, and should give us enough result to find a player's id given name and birth date
- Seems to be missing sprint speed, which is one of the most crucial statcast stats needed
- FanGraphs scouting report (FSR) data is interesting and looks a lot like attribute, so it might be useful
- However, historical players do not have FSRs, so that may only be useful for current players
- Has pitch frequency data going back to 2002 and pitch specific velo/movement data 2005 (in json)
<br/>

8. <strong>Lahman's Baseball Database</strong>:
- Seems to have very complete historical statistics
- Doesn't look to have great Roster listings
- Would be missing a lot of player info data that we currently have access to
- Would not allow us to do mid-season rosters
- It doesn't give us all of the same stats the other api does, but it looks like all of those stats should be calculable
- The only major stat I can see that wouldn't be calculable would be Save Opportunities
<br/>
