using System.IO;
using System.Collections.Generic;
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
        public static List<string> GetAllPopID(string databasePath)
        {
            string[] allLines = File.ReadAllLines(databasePath);
            List<string> popIDs = new List<string>();

            for (int i = 0; i < allLines.Length; i++)
                popIDs.Add(allLines[i].Split(',')[0]);

            return popIDs;
        }
        public Dictionary<string, string> GetPopDict(string startDate, string databasePath)
        {
            if (databasePath == null)
                return null;

            string[] allLines = null;
            
            if (File.Exists(databasePath))
                allLines = File.ReadAllLines(databasePath);
            
            Dictionary<string, string> popDict = new Dictionary<string, string>();
            
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
        public bool CheckPopIds(List<string> popIdList, string popName)
        {
            if (popIdList.Contains(popName))
                return true;

            return false;
        }
        public List<string> ParsePopDictionary(Dictionary<string, string> popDict)
        {
            List<string> popIDs = new List<string>();
            foreach (var x in popDict)
            {
                if (x.Key != null)
                    popIDs.Add(x.Key);
            }
            return popIDs;
        }
    }
}
