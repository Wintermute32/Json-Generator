using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using CsvHelper;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration.Attributes;

namespace JsonPopulator
{
    public class Root
    {
        [Name("Event ID **")]
        public string boxId { get; set; }
        [Name("Collection ID **")]
        public string fandomId { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public Appearance appearance { get; set; }
        public string behaviourType { get; set; }
        public List<string> featuredPopIdsList { get; set; }
        public List<Prize> prizes {get; set;}
        public List<Tier> tiers {get; set;}
        public List<LastChanceBoxPrize> lastChanceBoxPrizes { get; set; }

        public Root()
        {
            behaviourType = "";
        }

        public void GetRootValues(string eventName, LivePlaybook livePlay)
        {
            boxId = 'e'+livePlay.boxID;
            fandomId = eventName + "Fandom";
            startDate = livePlay.startDate;
            endDate = livePlay.endDate;
            behaviourType = "";
        }

        public void SetFeaturedPopIds(Dictionary<string,string> popIdDict)
        {
            List<string> whatever = new List<string>() { "common", "rare", "epic", "legendary"};

            foreach (KeyValuePair<string, string> entry in popIdDict)
                for (int i = 0; i < whatever.Count; i++)
                    if (entry.Value == whatever[i]) 
                        whatever[i] = entry.Key;

            featuredPopIdsList = whatever;
        }

         public void GetStartDate(string dateLine)
        {
            List<DateTime> dateAndTime = new List<DateTime>();

            foreach (var x in dateLine.Split(","))
            {
                if (x.Contains('/') || x.Contains(':'))
                    dateAndTime.Add(DateTime.Parse(x));
            }

            if (dateAndTime.Count > 2)
            {
                Console.WriteLine("Date Time not calculated Properly");
                return;
            }
                startDate = dateAndTime[0].ToString("MM/dd/yyyy HH:mm");
                endDate = dateAndTime[1].AddDays(-1).ToString("MM/dd/yyyy HH:mm");
        }

        public void ReArrangeRootDates(Root root)
        {
            List<DateTime> dateAndTime = new List<DateTime>();

            root.startDate = dateAndTime[0].ToString("MM/dd/yyyy HH:mm");
            root.endDate = dateAndTime[1].AddDays(-1).ToString("MM/dd/yyyy HH:mm");
        }



    }

}
