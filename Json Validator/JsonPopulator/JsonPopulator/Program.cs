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

namespace JsonPopulator
{

    class Program
    {

        public static void Main(string[] args)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);

            var reader = new StreamReader(@"C:\Users\pdnud\OneDrive\Desktop\Json Validator\0032_FunkoBlitz_EventRewards_TheOffice3_Clear - Event Gacha.csv");
            reader.ReadLine();
            var csv = new CsvReader(reader, config);
            var records = csv.GetRecords<Tier>().ToList();
            
            foreach (var x in records)
                Console.WriteLine("This is the records output: " + x.ToString());


            string eventID = Console.ReadLine();

            LivePlaybook livePlay = new LivePlaybook(@"C:\Users\pdnud\OneDrive\Desktop\Json Validator\Live Playbook.csv", eventID);

            Root root = new Root(eventID, livePlay);

            //RunMeFirst();

        }

        static void RunMeFirst()
        {
            Console.WriteLine("Type the Event Name, no spaces with the set number IE: \'TheOffice3\'");
            string eventID = Console.ReadLine();

            LivePlaybook livePlay = new LivePlaybook(@"C:\Users\pdnud\OneDrive\Desktop\Json Validator\Live Playbook.csv", eventID);
            Root root = new Root(eventID, livePlay);
            PopDatabase popData = new PopDatabase(@"C:\Users\pdnud\OneDrive\Desktop\Json Validator\[1.6.0] Pop_Database - pop_database.csv", root);
            var startDate = popData.startDate;
            var popDict = popData.GetPopDict(startDate);


            StoreButtonAppearance sbA = new StoreButtonAppearance(eventID, popDict);
            Appearance appearance = new Appearance(sbA);
            root.SetFeaturedPopIds(popDict);

            GachaParser gachaPar = new GachaParser();
            var parsedGacha = gachaPar.ParseEventSheet(@"C:\Users\pdnud\OneDrive\Desktop\Json Validator\0032_FunkoBlitz_EventRewards_TheOffice3_Clear - Event Gacha.csv");
            appearance.purchaseScreenAppearance = new PurchaseScreenAppearance(eventID, popDict);
            root.appearance = appearance;
            root.prizes = gachaPar.RetPopPrizeLine(parsedGacha);

            string rootOutput = JsonConvert.SerializeObject(root, Formatting.Indented);

            Console.WriteLine(rootOutput);
        }

    }

}


 