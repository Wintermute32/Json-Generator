# Changelog

## [1.0.17] - 2021-05-28 - Kemuel
* Add Jenkinsfile pipeline and updated package.json to publish to npm.tictocgames.com

## [1.0.16] - 2021-05-26 - Kemuel
* Testing webhooks

## [1.0.15] - 2021-05-26 - Kemuel
* NPM repository testing

## [1.0.14] - 2021-04-29 - Zaid
* Adding TTG Copyright header to cs files

These are the release notes for the TicToc.GoogleSheets package which was first introduced on March 21, 2020 with Unity 2018.2.

## [1.0.13] - 2020-08-13
- Fixed a bug that rows in the first sheet cannot be deleted.

## [1.0.12] - 2020-07-15
- Rows in sheets other than the first sheet can be deleted.

## [1.0.11] - 2020-07-14
- Tests are added back but only run when not in cloud build.
- credentials.json moved from Resources folder to package.

## [1.0.10] - 2020-06-26
- Removed tests to prevent build errors due to not being able to login in cloud build.

## [1.0.9] - 2020-06-26
- Fixed the bug that the first row of a spreadsheet cannot be deleted.

## [1.0.8] - 2020-06-26
- Attempting to fix the google.apis.core folder sometimes not downloading through the Unity Package Manager.

## [1.0.7] - 2020-06-22
- Putting the latest into the package, looks like some helpers for getting the user name and email?

## [1.0.6] - 2020-06-15
- Added support to write to and delete data from spreadsheets.

## [1.0.5] - 2020-03-22
- Added support to receive spreadsheets in a 2D array of strings instead of in csv format.

## [1.0.4] - 2020-03-22
- Attempting to use .NET Standard 2.0 libraries instead of .NET 4.5

## [1.0.3] - 2020-03-21
- Trying to move Google libraries outside of the assembly definition.

## [1.0.2] - 2020-03-21
- Overriding the references of the TicToc.GoogleSheets assembly definition file to directly reference various Google libraries.

## [1.0.1] - 2020-03-21
- Deleted a meta file to see if it helps Unity load this library's code in other projects.

## [1.0.0] - 2020-03-21
- Supports authenticating with Google
- Can get data from Google Sheets and convert it into csv format.
