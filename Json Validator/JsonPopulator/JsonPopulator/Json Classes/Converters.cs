using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Linq;
using JsonPopulator.CSV;

namespace JsonPopulator
{
    class Converters
    {
        public Playbook PlaybookPopulator(string playbookPath, string eventID)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.HeaderValidated = null;
            config.MissingFieldFound = null;
            var reader = new StreamReader(playbookPath);
            reader.ReadLine();
            reader.ReadLine(); //reassigns header as third line from top of CSV


            var csv = new CsvReader(reader, config);
            var playbookRecords = csv.GetRecords<Playbook>().ToList();

            foreach (var x in playbookRecords)
                if (x.fandomName.Contains(eventID))
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
    }
}
