using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace JsonPopulator
{
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
