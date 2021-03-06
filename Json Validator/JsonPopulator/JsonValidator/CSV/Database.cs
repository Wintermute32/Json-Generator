using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration.Attributes;

namespace JsonValidator.CSV
{
    class Database
    {
        [Name("PopId")]
        public string popID { get; set; }
        [Name("Rarity")]
        public string rarity { get; set; }
        [Name("ReleaseDate")]
        public string releaseDate { get; set; }
        [Name("ExclusivityType")]
        public string eventExclusive { get; set; }


        public List<string> GetAllPopID(string databasePath)
        {
            string[] allLines = File.ReadAllLines(databasePath);
            List<string> popIDs = new List<string>();

            for (int i = 0; i < allLines.Length; i++)
                popIDs.Add(allLines[i].Split(',')[0]);

            return popIDs;

        }

        public Dictionary<string, string> GetPopDict(string startDate, string databasePath)
        {
            string[] allLines = File.ReadAllLines(databasePath);
            Dictionary<string, string> popDict = new Dictionary<string, string>();

            for (int i = 0; i < allLines.Length; i++)
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
    }
}
