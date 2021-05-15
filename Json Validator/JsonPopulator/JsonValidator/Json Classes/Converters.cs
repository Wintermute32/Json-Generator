using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Diagnostics;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Linq;
using JsonValidator.CSV;

namespace JsonValidator
{
    class Converters
    {
        public Playbook PlaybookPopulator(string playbookPath, string eventID)
        {
  
          var playbookRecords = playbookHelper(playbookPath);

           foreach (var x in playbookRecords)
                if (x.fandomName.ToLower().Contains(eventID.ToLower()))
                {
                    Console.WriteLine("Playbook found!");
                    return x;
                }

            Console.WriteLine("Event Name Not Found");
            return null;
        }

        public List<Database> DatabasePopulator(string databasePath, string startDate)
        {
            List<Database> popData = new List<Database>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.HeaderValidated = null;
            config.MissingFieldFound = null;
            var reader = new StreamReader(databasePath);

            var csv = new CsvReader(reader, config);
            var dataBase = csv.GetRecords<Database>().ToList();

            foreach (var x in dataBase)
                if (x.releaseDate.Contains(startDate))
                {
                    Console.WriteLine("database found!");
                    popData.Add(x);
                }

            if (popData.Count == 0)
                Console.WriteLine("Event Name Not Found");

            return popData;
        }

        public List<Gacha> GachaPopulator(string gachaPath)
        {

            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.HeaderValidated = null;
            config.MissingFieldFound = null;
            config.IgnoreBlankLines = true;

            var reader = new StreamReader(gachaPath);
            reader.ReadLine();

            var csv = new CsvReader(reader, config);
            var gachaData = csv.GetRecords<Gacha>().ToList();
            gachaData.RemoveRange(21, gachaData.Count - 21);

            if (gachaData.Count == 0)
            {
                Console.WriteLine("Gacha's Not Found");
            }

            return gachaData;
        }

        public List<string> GetBoxIds(string playbookPath)
        {
            List<string> boxIDs = new List<string>();
            var playbookList = playbookHelper(playbookPath);

            foreach (var x in playbookList)
            {
                if (x.eventID != null && x.eventID != "")
                    boxIDs.Add(x.eventID);
            }
            return boxIDs;
        }

        public List<Playbook> playbookHelper(string playbookPath)
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
    }
}
