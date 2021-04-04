using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Globalization;


namespace JsonPopulator
{
    public class LivePlaybook
    {
        public string fandomName { get; set; }
        public string playbookPath { get; set; }
        public string startDate { get; set; }
        public string boxID { get; set; }

        public string endDate { get; set; }
        public string behaviourType { get; set; }
        public List<string> featuredPopIds { get; set; }


        public LivePlaybook(string playbookPath, string fandomName)
        {
            this.playbookPath = playbookPath;
            this.fandomName = fandomName;
            string foundLine = FindLine();
            GetStartDate(foundLine);
            GetEventNumber(foundLine);
        }

        public string FindLine()
        {
            string[] allLines = File.ReadAllLines(playbookPath);

            for (int i = 0; i < allLines.Length; i++)
            {
                if (allLines[i].Contains(fandomName))
                    return allLines[i];
            }

            return "";
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
                startDate = dateAndTime[0].ToString("MM/dd/yy HH:mm");
                endDate = dateAndTime[1].AddDays(-1).ToString("MM/dd/yy HH:mm");
        }

        public void GetEventNumber(string line)
        {
            foreach (var x in line.Split(","))
            {
                if (Regex.IsMatch(x, @"\b\d{4}"))
                    boxID = x + "_" + fandomName;
            }

            if (boxID.Length)

        }

    }
}
