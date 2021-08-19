using System;
using System.IO;
using System.Collections.Generic;
using CsvHelper;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration;


namespace JsonValidator.CSV
{
    public class Playbook
    {
        [Name("Event IP")]
        public string BoxID { get; set; }
        [Name("Event Num")]
        public string EventNumber { get; set; }
        [Name("Collection ID **")]
        public string FandomName { get; set; }
        [Name("Start Time (UTC)")]
        public string StartDate { get; set; }
        [Name("End Time \n(UTC)")]
        public string EndDate { get; set; }    
        [Name("Event ID **")]
        public string EventID { get; set; }
        public string StartDateAlternative { get; set; }

        public List<string> BoxIDList = new List<string>();
        public void FixStartDate(string startDate)
        {
            this.StartDateAlternative = DateTime.Parse(startDate).ToString("M/d/yyyy");
        }
        public Playbook PlaybookPopulator(string playbookPath, string eventID)
        {
            var playbookRecords = GetPlayBookList(playbookPath);

            foreach (var x in playbookRecords)
                if (x.EventID.ToLower().Contains(eventID.Replace(" ", "").ToLower()))
                {
                    Console.WriteLine("Playbook found!");
                    return x;
                }
            Console.WriteLine("Event Name Not Found");
            return null;
        }
        public static List<string> GetBoxIds(string playbookPath)
        {
            List<string> boxIDs = new List<string>();

            var playbookList = GetPlayBookList(playbookPath);

            foreach (var x in playbookList)
            {
                if (x.EventID != null && x.EventID != "")
                {
                    boxIDs.Add(x.EventID);
                }
            }
            return boxIDs;
        }
        public static List<Playbook> GetPlayBookList(string playbookPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);

            config.HeaderValidated = null;
            config.MissingFieldFound = null;
            var reader = new StreamReader(playbookPath);
            reader.ReadLine();
            reader.ReadLine(); //reassigns header as third line from top of CSV

            Debug.WriteLine("Playbook being read");

            var csv = new CsvReader(reader, config);
            var playbookRecords = csv.GetRecords<Playbook>().ToList();
            return playbookRecords;
        }
        public static Playbook UpdatePlaybookObj(string playbookPath, string eventID, Playbook eventPlaybook)
        {
            eventPlaybook = eventPlaybook.PlaybookPopulator(playbookPath, eventID);
            eventPlaybook.FixStartDate(eventPlaybook.StartDate);
            return eventPlaybook;
        }
    }
}
