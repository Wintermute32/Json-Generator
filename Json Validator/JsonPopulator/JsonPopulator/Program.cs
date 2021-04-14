using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

//Create objects using JsonClass Object. 
//Parse Relevant Documents to pull out string data.
//Assign that data based on logic to properties in appropriate Class.
//Combine those Objects into one single Json.
namespace JsonPopulator
{

    class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Type the Event Name, no spaces with the set number IE: \'TheOffice3\'");
            string eventID = Console.ReadLine();

            LivePlaybook livePlay = new LivePlaybook(@"C:\Users\pdnud\OneDrive\Desktop\Json Validator\Live Playbook.csv", eventID);
            Root root = new Root(eventID, livePlay);
            PopDatabase popData = new PopDatabase(@"C:\Users\pdnud\OneDrive\Documents\Repos\Json Generator\Json Validator\JsonPopulator\Docs\[1.5.0] Pop_Database - pop_database.csv", root);
            var startDate = popData.startDate;
            var popDict = popData.GetPopDict(startDate);
            StoreButtonAppearance sbA = new StoreButtonAppearance(eventID, popDict);
            Appearance appearance = new Appearance(sbA);
            root.SetFeaturedPopIds(popDict);

            GachaParser gachaPar = new GachaParser(popData);
            var parsedGacha = gachaPar.ParseEventSheet(@"C:\Users\pdnud\OneDrive\Desktop\Json Validator\0032_FunkoBlitz_EventRewards_TheOffice3_Clear - Event Gacha.csv");

            var testMe = gachaPar.RetPopPrizeLine(parsedGacha);


            root.appearance = appearance;
            root.prizes = gachaPar.RetPopPrizeLine(parsedGacha);

            string rootOutput = JsonConvert.SerializeObject(root, Formatting.Indented);

            Console.WriteLine(rootOutput);

        }

    }

}


 