/*
 * Tic Toc Games Copyright 2021
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.txt', which is part of this source code package.
 */
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace TicToc.GoogleSheets
{
	public interface IGoogleSheetsApi
	{
		/// <summary>
		/// This library requires OAuth2 before fetching spreadsheets from Google Sheets.
		/// </summary>
		bool IsAuthenticated { get; }

		/// <summary>
		/// Perform OAuth2. Required to access anything on Google Sheets.
		/// </summary>
		/// <returns></returns>
		bool Authenticate();

		/// <summary>
		/// Fetch a sheet in csv format
		/// </summary>
		/// <param name="googleSheetId">
		/// This is the long string in a sheet's share URL.
		/// Eg: 1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms in https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/
		/// </param>
		/// <param name="range">Eg: "SheetTabName!A2:E"</param>
		/// <returns>A task when awaited will fetch the sheet as a csv.</returns>
		string GetAsCsv(string googleSheetId, string range);

		/// <summary>
		/// First dimension is the number of rows. The second dimension is the column index.
		/// </summary>
		string[,] GetAsMultidimensionalArray(string googleSheetId, string range);

		/// <summary>
		/// Append data to the end of the spreadsheet.
		/// </summary>
		/// <param name="googleSheetId"></param>
		/// <param name="range">Eg: "SheetTabName!A2:E".</param>
		/// <param name="data">The data to be appended. Will be treated as strings.</param>
		void AppendRow(string googleSheetId, string range, List<object> data);

		/// <summary>
		/// Append a 2D string array to the end of range.
		/// </summary>
		/// <param name="googleSheetId"></param>
		/// <param name="range">Data is always appended to the end of spreadsheet. Make sure sheet name is correct.</param>
		/// <param name="arr">The string array to be appended. The dimensions will be kept.</param>
		void Append2DStrArray(string googleSheetId, string range, string[,] arr);

		/// <summary>
		/// Update the data in range.
		/// </summary>
		void Update(string googleSheetId, string range, List<object> data);

		/// <summary>
		/// Delete a row in the spreadsheet and shift the rest up.
		/// </summary>
		/// <param name="googleSheetId"></param>
		/// <param name="row"> Use the row number as shown in the google sheet (i.e. starting at 1) </param>
		/// <param name="sheetId">The part after gid= in a spreadsheet link. eg: https://docs.google.com/spreadsheets/d/spreadsheetID/edit#gid=1234, then use 1234</param>
		void DeleteRow(string googleSheetId, int row, int sheetId = 0);

		/// <summary>
		/// Get the current logged user's full name.
		/// </summary>
		/// <returns>Current user's full name or empty string if login failed.</returns>
		string GetUserName();

		/// <summary>
		/// Get the current logged user's email.
		/// </summary>
		/// <returns>Current user's email or empty string if login failed.</returns>
		string GetUserEmail();
		string RetrieveDriveSheetLongString(string eventNum, string eventID);

		string GetGachaSheetRange(string googleSheetID);
	}

	public class GoogleSheetsApi : IGoogleSheetsApi
	{
		public static readonly IGoogleSheetsApi Instance = new GoogleSheetsApi();

		private GoogleSheetsApi() { }

		// If modifying these scopes, delete your previously saved credentials
		// at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
		private static readonly string[] Scopes =
		{
			SheetsService.Scope.Spreadsheets,
			"https://www.googleapis.com/auth/userinfo.email",
			"https://www.googleapis.com/auth/userinfo.profile",
			"https://www.googleapis.com/auth/drive.readonly"
		};
		private const string ApplicationName = "Google Sheets API .NET Quickstart";

		public bool IsAuthenticated => _credential != null && _service != null;

		private UserCredential _credential;
		private SheetsService _service;

		private Userinfo _userInfo;

		DriveService service;
		public Dictionary<string, Google.Apis.Drive.v3.Data.File> files = new Dictionary<string, Google.Apis.Drive.v3.Data.File>();

		public bool Authenticate()
		{
			if (_credential == null)
			{
				var credPath = Path.GetFullPath(Path.Combine("TicToc", "GoogleSheets", "credentials.json"));
				byte[] bytes;
				try
				{
					bytes = File.ReadAllBytes(credPath);
				}
				catch (DirectoryNotFoundException)
				{
					Debug.WriteLine("This Directory WASNT FOUND!");
					credPath = Path.Combine(Application.StartupPath, "TicToc", "GoogleSheets", "credentials.json");
					bytes = File.ReadAllBytes(credPath);
				}


				using (var stream = new MemoryStream(bytes))
				{
					// The file token.json stores the user's access and refresh tokens, and is created
					// automatically when the authorization flow completes for the first time.
					var tokenPath = Path.Combine(Application.CommonAppDataPath, "token");
					Debug.WriteLine("Token Path is :" + tokenPath);
					_credential = null;

					// A thread to handle authorization
					// Would timeout after msTimeout ms and throw an exception to prevent Unity hangs forever. 
					// If the user logins after timeout, the login attempt will not go through (as if the user has not logged in) 
					int msTimeout = 60000; //60s //TODO: how long should this be?
					var thread = new Thread(() =>
							_credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
								GoogleClientSecrets.FromStream(stream).Secrets,
								Scopes,
								"user",
								CancellationToken.None,
								new FileDataStore(tokenPath, true)).Result
							)
					{
						IsBackground = false
					};
					thread.Start();
					if (!thread.Join(msTimeout))
					{

						throw new Exception("Timeout exception!!! Restore unity...");
					}

					Debug.WriteLine($"Token file saved to: {tokenPath}");

				}

				var oauthService = new Oauth2Service(
					new BaseClientService.Initializer()
					{
						HttpClientInitializer = _credential,
						ApplicationName = "TicToc Google Sheets"
					}
					);
				_userInfo = oauthService.Userinfo.Get().ExecuteAsync().Result;
				Debug.WriteLine("Logged in as: " + _userInfo.Name + ", email: " + _userInfo.Email);
			}

			if (_service == null)
			{
				// Create Google Sheets API service.
				_service = new SheetsService(new BaseClientService.Initializer
				{
					HttpClientInitializer = _credential,
					ApplicationName = ApplicationName
				});
			}

			return IsAuthenticated;
		}

		// ReSharper disable 3 CommentTypo
		/// <summary>
		/// Convert the google sheet into a csv string.
		/// </summary>
		/// <param name="googleSheetId">Eg: "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms"</param>
		/// <param name="range">Eg: "Class Data!A2:E"</param>
		/// <returns></returns>
		public string GetAsCsv(string googleSheetId, string range)
		{
			if (!IsAuthenticated) return null;

			// Define request parameters.
			// const string spreadsheetId = "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms";
			// const string range = "Class Data!A2:E";

			var request = _service.Spreadsheets.Values.Get(googleSheetId, range);

			ValueRange response;

			try
			{
				response = request.Execute();
			}
			catch
			{
				return "";
			}

			var values = response.Values;

			if (values == null || values.Count <= 0) return null;
			var asCsv = "";
			for (var rowIndex = 0; rowIndex < values.Count; rowIndex++)
			{
				var row = values[rowIndex];
				for (var columnIndex = 0; columnIndex < row.Count; columnIndex++)
				{
					//ensures we don't mistake comma's in numbers for delimiters 
					//by adding quotes around numbers with commas IE "5,000"
					asCsv += row[columnIndex].ToString().Contains(',') ?
						'\"' + row[columnIndex].ToString() + '\"' : row[columnIndex].ToString();

					//asCsv += row[columnIndex].ToString();
					if (columnIndex < row.Count - 1) asCsv += ",";
				}
				// Google Sheets does not put a trailing end-of-line when downloading sheets as a csv file.
				if (rowIndex < values.Count - 1) asCsv += "\n";
			}
			return asCsv;
		}

		/// <summary>
		/// Instead of recreating the csv, this puts everything in a two dimensional array already separated out.
		/// </summary>
		public string[,] GetAsMultidimensionalArray(string googleSheetId, string range)
		{
			if (!IsAuthenticated) return null;

			// Define request parameters.
			// const string spreadsheetId = "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms";
			// const string range = "Class Data!A2:E";
			var request = _service.Spreadsheets.Values.Get(googleSheetId, range);

			var response = request.Execute();
			var values = response.Values;
			if (values == null || values.Count <= 0) return null;
			var rowCount = values.Count;
			var maxColumnCount = 0;

			// How wide do we need to make our 2D array?
			foreach (var row in values)
			{
				var columnCountForThisRow = row.Count;
				// This wide
				//maxColumnCount = Mathf.Max(maxColumnCount, columnCountForThisRow);
			}

			var toReturn = new string[rowCount, maxColumnCount];
			for (var rowIndex = 0; rowIndex < values.Count; rowIndex++)
			{
				var row = values[rowIndex];
				for (var columnIndex = 0; columnIndex < maxColumnCount; columnIndex++)
				{
					if (columnIndex < row.Count)
						toReturn[rowIndex, columnIndex] = row[columnIndex].ToString();
				}
			}

			return toReturn;
		}

		/// <summary>
		/// Append data to the end of range.
		/// </summary>
		/// <param name="googleSheetId"></param>
		/// <param name="range">Data is always appended to the end of spreadsheet. Make sure sheet name is correct.</param>
		/// <param name="data">Data to be inputed. Will be treated as strings.</param>
		public void AppendRow(string googleSheetId, string range, List<object> data)
		{
			var valueRange = new ValueRange { Values = new List<IList<object>> { data } };

			var request = _service.Spreadsheets.Values.Append(valueRange, googleSheetId, range);
			request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW;

			var response = request.Execute();
		}

		/// <summary>
		/// Append a 2D string array to the end of range.
		/// </summary>
		/// <param name="googleSheetId"></param>
		/// <param name="range">Data is always appended to the end of spreadsheet. Make sure sheet name is correct.</param>
		/// <param name="arr">The string array to be appended. The dimensions will be kept.</param>
		public void Append2DStrArray(string googleSheetId, string range, string[,] arr)
		{
			int numRows = arr.GetLength(0);
			int numColumns = arr.GetLength(1);

			var data = new List<object>();
			for (int i = 0; i < numRows; i++)
			{
				for (int j = 0; j < numColumns; j++)
				{
					data.Add(arr[i, j]);
				}
				AppendRow(googleSheetId, range, data);
				data.Clear();
			}
		}

		/// <summary>
		/// Update a range in the spreadsheet.
		/// </summary>
		public void Update(string googleSheetId, string range, List<object> data)
		{
			var valueRange = new ValueRange { Values = new List<IList<object>> { data } };

			var request = _service.Spreadsheets.Values.Update(valueRange, googleSheetId, range);
			request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;

			var response = request.Execute();
		}

		/// <summary>
		/// Delete a row in the spreadsheet and shift the rest up.
		/// </summary>
		/// <param name="googleSheetId"></param>
		/// <param name="row"> Use the number as shown in the google sheet (i.e. starting at 1) </param>
		/// <param name="sheetId">The part after gid= in a spreadsheet link. eg: https://docs.google.com/spreadsheets/d/spreadsheetID/edit#gid=1234, then use 1234</param>
		public void DeleteRow(string googleSheetId, int row, int sheetId = 0)
		{
			if (row < 1 || sheetId < 0) return;
			var requestBody = new Request()
			{
				DeleteDimension = new DeleteDimensionRequest()
				{
					Range = new DimensionRange()
					{
						SheetId = sheetId,
						Dimension = "ROWS",
						StartIndex = Convert.ToInt32(row - 1),
						EndIndex = Convert.ToInt32(row)
					}
				}
			};

			var requestContainer = new List<Request>() { requestBody };

			var deleteRequest = new BatchUpdateSpreadsheetRequest { Requests = requestContainer };

			var actualDeletion = new SpreadsheetsResource.BatchUpdateRequest(_service, deleteRequest, googleSheetId);
			actualDeletion.Execute();
		}

		/// <summary>
		/// Get the current logged user's full name.
		/// </summary>
		/// <returns>Current user's full name or empty string if login failed.</returns>
		public string GetUserName()
		{
			if (!IsAuthenticated || _userInfo == null) return string.Empty;
			return _userInfo.Name;
		}

		/// <summary>
		/// Get the current logged user's email.
		/// </summary>
		/// <returns>Current user's email or empty string if login failed.</returns>
		public string GetUserEmail()
		{
			if (!IsAuthenticated || _userInfo == null) return string.Empty;
			return _userInfo.Email;
		}
		public string RetrieveDriveSheetLongString(string eventNum, string eventID)
		{
			string ApplicationName = "Drive API .NET Quickstart";

			service = new DriveService(new BaseClientService.Initializer()
			{
				HttpClientInitializer = _credential,
				ApplicationName = ApplicationName,
			});

			eventID = eventID.Replace("x", "");

			//Getting rid of ID numbers in event names since
			//already searching within an event sheets number
			foreach (var x in eventID)
				if (Char.IsDigit(x))
					eventID = eventID.Replace(x.ToString(), "");

			// Define parameters of request.
			FilesResource.ListRequest listRequest = service.Files.List();
			listRequest.Q = $"mimeType = 'application/vnd.google-apps.spreadsheet' and name contains '{eventNum}'";

			// List files.
			IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
				.Files;

			if (files != null && files.Count > 0)
			{
				Debug.WriteLine("Files in files count :" + files.Count);

				foreach (var file in files)
				{
					Debug.WriteLine("Files found include: " + file.Name);
					if (file.Name.Replace("Set", "").Contains(eventID))
					{
						Debug.WriteLine("NAME IS : " + file.Name + "File ID is : " + file.Id);
						return file.Id;
					}
				}

			}

			return null;
		}
		private string AbsPath(Google.Apis.Drive.v3.Data.File file)
		{
			var name = file.Name;

			//if (file.Parents.Count() == 0)
			//{
			//	return name;
			//}

			var path = new List<string>();

			while (true)
			{
				var parent = GetParent(file.Parents[0]);

				// Stop when we find the root dir
				if (parent.Parents == null || parent.Parents.Count() == 0)
				{
					break;
				}

				path.Insert(0, parent.Name);
				file = parent;
			}
			path.Add(name);
			return path.Aggregate((current, next) => Path.Combine(current, next));
		}

		private Google.Apis.Drive.v3.Data.File GetParent(string id)
		{
			// Check cache
			if (files.ContainsKey(id))
			{
				return files[id];
			}

			// Fetch file from drive
			var request = service.Files.Get(id);
			request.Fields = "name,parents";
			var parent = request.Execute();

			// Save in cache
			files[id] = parent;

			return parent;
		}

		public string GetGachaSheetRange(string googleSheetID)
		{
			string range = "";
			try
			{
				var sheets = _service.Spreadsheets.Get(googleSheetID).Execute().Sheets;

				foreach (var x in sheets)
				{
					string title = x.Properties.Title.ToLower();
					if (title.Contains("gacha") && !title.Contains("var") && !title.Contains("day"))

						range = x.Properties.Title + "!A1:S48";
				}
			}

			catch
			{
				MessageBox.Show("Manually drag-drop the gacha CSV Instead", "Can't find this events gacha tab");
			}
			return range;


		}
	}
}