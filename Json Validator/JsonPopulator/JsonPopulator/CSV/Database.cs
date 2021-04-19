﻿using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using CsvHelper;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration.Attributes;

namespace JsonPopulator.CSV
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


        public List<string> PopIds(List<Database> popDatabase)
        {
            List<string> popIds = new List<string>();
            foreach (var x in popDatabase)
                popIds.Add(x.popID);

            return popIds;

        }

        public Dictionary<string, string> GetPopDict(string startDate, string databasePath)
        {
            string[] allLines = File.ReadAllLines(databasePath);
            Dictionary<string, string> popDict = new Dictionary<string, string>();

            for (int i = 0; i < allLines.Length; i++)
            {
                if (allLines[i].Contains(startDate))
                {
                    var lineSplit = allLines[i].Split(",");
                    bool isEvent = false;

                    foreach (var x in lineSplit)
                    {
                        if (x.ToLower() == "event")
                            isEvent = true;
                    }

                    if (isEvent == true)
                        popDict.Add(lineSplit[0], "event exclusive");

                    else
                        popDict.Add(lineSplit[0], lineSplit[6].ToLower());
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
