using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace JsonPopulator
{

    class Program
    {
        

        public static void Main(string[] args)
        {
            string playbookPath = @"/Users/piercenudd/Json-Generator/Json Validator/JsonPopulator/Docs/Live Playbook.csv";
            string databasePath = @"/Users/piercenudd/Json-Generator/Json Validator/JsonPopulator/Docs/[1.5.0] Pop_Database - pop_database.csv";
            string eventSheetPath = @"/Users/piercenudd/Json-Generator/Json Validator/JsonPopulator/Docs/0032_FunkoBlitz_EventRewards_TheOffice3_Clear - Event Gacha.csv";

            Console.WriteLine("Type the Event Name, no spaces with the set number IE: \'TheOffice3\'");
            string eventID = Console.ReadLine();

            LivePlaybook livePlay = new LivePlaybook(playbookPath, eventID);
            Root root = new Root(eventID, livePlay);
            PopDatabase popData = new PopDatabase(databasePath, root);
            var startDate = popData.startDate;
            var popDict = popData.GetPopDict(startDate);
            StoreButtonAppearance sbA = new StoreButtonAppearance(eventID, popDict);
            Appearance appearance = new Appearance(sbA);
            root.SetFeaturedPopIds(popDict);

            GachaParser gachaPar = new GachaParser();
            var parsedGacha = gachaPar.ParseEventSheet(eventSheetPath);

            var testMe = gachaPar.RetPopPrizeLine(parsedGacha);
            root.appearance = appearance;
            root.prizes = gachaPar.RetPopPrizeLine(parsedGacha);

            string rootOutput = JsonConvert.SerializeObject(root, Formatting.Indented);

            Console.WriteLine(rootOutput);

        }

    }

}


 