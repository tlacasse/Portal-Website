# Portal
Personal Homepage

Windows-Desktop-like main page with visual Icons with bookmark-worthy links.
And other various Utilities.

Mithril.js client-side, C# Web API server-side, Sqlite database.

### Setup

Match this directory structure:
- `~/PortalWebsite/` - Website Project
	- `/_Build/` - Runnable Build Output
		- `/Portal/` - Non-Build Managed Items
			- `PortalWebsite.db` - Sqlite Database
			- `/Icons/-1.png` - Default "New Icon" image

Run `~/PortalWebsite/build.ps1` (requires node.js and gulp) to build and copy into the Build Output.
Include the `-ui` flag or the `-server` flag for only client/server side building.

Database is set up with [these tables and views](https://github.com/tlacasse/Portal-Website/tree/master/Portal/sqlite).
			
*I use a plain white image for the "New Icon".*

### Future Plans

* "Saved" message after saving / confirmation button on cancel.
* API Viewer
* Security & navigation for on-the-go phone viewing
* Database front-ends
	* Classes
	* Dinners
	* Games
* Dining hall menu viewing
	* Rating
	* Recommended meal
* Laundry view
