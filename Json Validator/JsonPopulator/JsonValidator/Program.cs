using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Windows.Forms;
using JsonValidator.CSV;

namespace JsonValidator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

       public static NewRoot GetJsonObject(string databasePath, string playbookPath, string eventID)
        {
            Converters converters = new Converters();
            Database database = new Database();
            Gacha gacha = new Gacha();

            var eventPlaybook = converters.PlaybookPopulator(playbookPath, eventID);
            eventPlaybook.FixStartDate(eventPlaybook.startDate);
            List<Database> eventPopData = converters.DatabasePopulator(databasePath, eventPlaybook.startDate); //this isn't working

            Dictionary<string, string> popDict = database.GetPopDict(eventPlaybook.startDateAlternate, databasePath);

            NewRoot newRoot = new NewRoot(eventPlaybook, popDict);
            StoreButtonAppearance sba = new StoreButtonAppearance(eventID, popDict);

            PurchaseScreenAppearance psA = new PurchaseScreenAppearance(eventID, popDict);
            MainHubAppearance mhA = new MainHubAppearance(eventID, popDict);
            Tier tiers = new Tier();
            LastChanceBoxPrize lastChancePrize = new LastChanceBoxPrize();

            Appearance appearance = new Appearance(sba, psA, mhA);
            newRoot.appearance = appearance;

            List<Gacha> gachaList = converters.GachaPopulator(@"C:\Users\pdnud\OneDrive\Desktop\Json Validator\0034_FunkoBlitz_EventRewards_BackToTheFutureSet3_Clear - Event Gacha.csv");
            newRoot.prizes = gacha.PrizeList(gachaList);
            newRoot.tiers = tiers.AssignGuarantee(tiers.GenerateTierList(gachaList), popDict);
            newRoot.lastChanceBoxPrizes = lastChancePrize.AssignBoxValues(popDict);

            return newRoot;

            //SerializeJson(newRoot);
        }

        public static string SerializeJson(NewRoot newRoot)
        {
            //Accepts Completed NewRoot Object
            var serializerSettings = new JsonSerializerSettings();

            string rootOutput = JsonConvert.SerializeObject(newRoot, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            return rootOutput;
        }
    }
}
