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
            string databasePath = @"C:\Users\pdnud\OneDrive\Desktop\Json Validator\[1.6.0] Pop_Database - pop_database.csv";
            string populatorPath = @"C:\Users\pdnud\OneDrive\Desktop\Json Validator\Live Playbook.csv";
            Console.WriteLine("Type the Event Name, no spaces with the set number IE: \'TheOffice3\'");
            string eventID = Console.ReadLine();

            Converters converters = new Converters();
            Database database = new Database();
            Gacha gacha = new Gacha();

            var eventPlaybook = converters.PlaybookPopulator(populatorPath, eventID);
            eventPlaybook.FixStartDate(eventPlaybook.startDate);
            List<Database> eventPopData = converters.DatabasePopulator(databasePath, eventPlaybook.startDate); //this isn't working

            Dictionary<string, string> popDict = database.GetPopDict(eventPlaybook.startDateAlternate, databasePath);
           
            NewRoot newRoot = new NewRoot(eventPlaybook, popDict);
            StoreButtonAppearance sba = new StoreButtonAppearance(eventID, popDict);
            PurchaseScreenAppearance psA = new PurchaseScreenAppearance(eventID, popDict);
            MainHubAppearance mhA = new MainHubAppearance(eventID, popDict);
            Tier tiers = new Tier();
            
            Appearance appearance = new Appearance(sba, psA, mhA);
            newRoot.appearance = appearance;

            List<Gacha> gachaList = converters.GachaPopulator(@"C:\Users\pdnud\OneDrive\Desktop\Json Validator\0034_FunkoBlitz_EventRewards_BackToTheFutureSet3_Clear - Event Gacha.csv");
            newRoot.prizes = gacha.PrizeList(gachaList);
            newRoot.tiers = tiers.AssignGuarantee(tiers.GenerateTierList(gachaList), popDict);
          
            string rootOutput = JsonConvert.SerializeObject(newRoot, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            Console.WriteLine(rootOutput);
        }




    }

        

}



 