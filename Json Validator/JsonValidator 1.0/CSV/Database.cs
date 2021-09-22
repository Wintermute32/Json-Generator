using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using CsvHelper.Configuration.Attributes;

namespace JsonValidator.CSV
{
	class Database
	{
		[Name("PopId")]
		public string PopID { get; set; }
		[Name("Rarity")]
		public string Rarity { get; set; }
		[Name("ReleaseDate")]
		public string ReleaseDate { get; set; }
		[Name("ExclusivityType")]
		public string EventExclusive { get; set; }
		public static List<string> PopIDList { get; private set; }
		public static  Dictionary<string, string> PopDict { get; private set; }
		public Database(string databaseCSVStr, string startDate)
		{
			PopIDList = GetAllPopID(databaseCSVStr);
			PopDict = GetPopDict(startDate, databaseCSVStr);
		}
		private List<string> GetAllPopID(string databaseCSVStr)
		{
			string[] allLines = File.Exists(databaseCSVStr) ? File.ReadAllLines(databaseCSVStr) : databaseCSVStr.Split('\n');
		
			List<string> popIDs = new List<string>();

			for (int i = 0; i < allLines.Length; i++)
				popIDs.Add(allLines[i].Split(',')[0]);

			return popIDs;
		}
		private Dictionary<string, string> GetPopDict(string startDate, string databaseCSVStr)
		{
			Dictionary<string, string> popDict = new Dictionary<string, string>();

			if (databaseCSVStr == null)
				return new Dictionary<string, string>() {{ "No Id's Found", "No Ids Found" }};

			string[] allLines = File.Exists(databaseCSVStr) ? File.ReadAllLines(databaseCSVStr) : databaseCSVStr.Split('\n');

			for (int i = 0;  i < allLines.Length; i++)
			{
				bool isEvent = false;
				if (allLines[i].Contains(startDate))
				{
					var lineSplit = allLines[i].Split(',');

					foreach (var x in lineSplit)
						if (x == "Event")
							isEvent = true;

					popDict.Add(lineSplit[0], isEvent ? "event exclusive" : lineSplit[6].ToLower());
				}
			}
			return popDict;
		}
		//public bool CheckPopIds(List<string> popIdList, string popName)
		//{
		//	if (popIdList.Contains(popName))
		//		return true;

		//	return false;
		//}
		//public List<string> ParsePopDictionary(Dictionary<string, string> popDict)
		//{
		//	List<string> popIDs = new List<string>();
		//	foreach (var x in popDict)
		//	{
		//		if (x.Key != null)
		//			popIDs.Add(x.Key);
		//	}
		//	return popIDs;
		//}
	}
}
