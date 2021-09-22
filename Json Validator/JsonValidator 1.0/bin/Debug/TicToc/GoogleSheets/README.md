# TicToc.GoogleSheets

A library to help get data from Google Sheets. Meant for development use, not for production use. There are download and access limits and security concerns using this in a production build.

## Getting Started Unity 2019.3

- Use the package manager to add the git URL git@bitbucket.org:TicTocGames/tictoc.googlesheets.git

## Getting Started Unity 2019.2

- Make sure Unity can use Git with an SSH key from the command line. (Mac Users: https://tictocgames.atlassian.net/wiki/spaces/TTT/pages/789218875/Fix+Unity+Package+Manager+not+able+to+use+Git+LFS+on+Macs)
- Find Packages/manifest.json in your Unity Project
- Edit the dependencies to look like this:

```
    "dependencies": {
       "com.tictocgames.googlesheets": "ssh://git@bitbucket.org/TicTocGames/tictoc.googlesheets.git",
        ...
    },
```
