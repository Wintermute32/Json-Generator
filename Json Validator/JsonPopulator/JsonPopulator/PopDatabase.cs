using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;

namespace JsonPopulator
{
    class PopDatabase
    {
        public string startDate { get; set; }
        string databasePath;
        //List<string> popIds = new List<string>();

        public PopDatabase(string databasePath, Root root)
        {
            this.databasePath = databasePath;
            this.startDate = FixStartDate(root.startDate);
            Console.WriteLine(startDate);
        }

        public Dictionary<string, string> GetPopDict (string startDate)
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

        public string FixStartDate(string startDate)
        {
            return DateTime.Parse(startDate).ToString("M/d/yyyy");
        }
      
        public bool CheckPopIds(string popId, List<string> popData)
        {
            string[] allLines = File.ReadAllLines(databasePath);
        }

    }

}
