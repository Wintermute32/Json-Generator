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
using JsonPopulator.Json_Classes;

namespace JsonPopulator
{

    class Program
    {
        //TEST TEST

        public static void Main(string[] args)
        {
            Console.WriteLine("Type the Event Name, no spaces with the set number IE: \'TheOffice3\'");
            string eventID = Console.ReadLine();
            Converters converters = new Converters();

            var eventPlaybook = converters.PlaybookPopulator(@"C:\Users\pdnud\OneDrive\Desktop\Json Validator\Live Playbook.csv", eventID);
            eventPlaybook.FixStartDate(eventPlaybook.startDate);

            Console.WriteLine(eventPlaybook.startDateAlternate);

            List<Database> eventPopData = converters.DatabasePopulator(@"C:\Users\pdnud\OneDrive\Desktop\Json Validator\[1.6.0] Pop_Database - pop_database.csv", eventPlaybook.startDate);
            List<Gacha> gachaList = converters.GachaPopulator(@"C:\Users\pdnud\OneDrive\Desktop\Json Validator\0032_FunkoBlitz_EventRewards_TheOffice3_Clear - Event Gacha.csv");

            NewRoot newRoot = new NewRoot(eventPlaybook);


            string rootOutput = JsonConvert.SerializeObject(newRoot, Formatting.Indented);

            Console.WriteLine(rootOutput);
        }




    }

        

}



 