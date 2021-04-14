using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
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
      


        public void CheckPopIds(List<string> popIds)
        {

        }

    }


    class EventDoc
    {
        List<string> eventSheet = new List<string>();

        Dictionary<string, string> eventDocDict = new Dictionary<string, string>() { { "boxID", null } };
        public string boxID { get; set; }
        public string path { get; set; }


        public EventDoc(string pathInput)
        {
            this.path = pathInput;
            ParseEventSheet(path);
        }

        public void ParseEventSheet(string path)
        {
            string[] allLines = File.ReadAllLines(path);

            foreach (var x in allLines)
            {
                if (Regex.IsMatch(x, @"^[a-zA-Z0-9_]+$"))
                    eventSheet.Add(x);
            }

            foreach (var x in eventSheet)
                Console.WriteLine(x);

            for (int i = 0; i < eventSheet.Count; i++)
            {
                if (i == 1 && eventSheet[i] != null)
                    eventDocDict["boxID"] = Regex.Replace(eventSheet[i], @"\s", "");
            }

            boxID = eventDocDict[boxID];
        }
        //x.Replace(" ", String.Empty)

        //String[] allWords = Regex.Split(File.ReadAllText("File.txt"), @"[\s,;:.!?-]+");

        //var result = Regex.Split(text, "\r\n|\r|\n")

        //StreamWriter sw = new StreamWriter();
        //File.


        //Root.boxID = EventID
        //Root.FandomID = Collection ID
        //Root.Startdate = Start Time
        //Root.EndDate = EndTime - 1 dateTime
        //Appearance.isEventBox = true (default)
        //mysteryBoxType = LuckyMystery (default)
        //ribbonLocalizationKey = EventBoxRibbon
        //titleLocalizationKey = CollectionID - Fandom + BoxTitle
        //subtitleLocalizationKey = "";
    }


}
