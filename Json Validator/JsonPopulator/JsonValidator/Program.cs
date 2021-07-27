using System;
using System.Collections.Generic;
using System.Windows.Forms;
using JsonValidator.CSV;

namespace JsonValidator
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

       public static NewRoot GetJsonObject(string databasePath, string playbookPath, string gachaPath, string eventID)
        {
            //returns completed NewRoot Object

            Converters converters = new Converters();
            Database database = new Database();
            Gacha gacha = new Gacha();
            Playbook eventPlaybook;

            try
            {
                eventPlaybook = converters.PlaybookPopulator(playbookPath, eventID);
                eventPlaybook.FixStartDate(eventPlaybook.StartDate);
            }
            catch (Exception)
            {
                string message = "This ID wasn't found in the playbook. Check playbook ID and upate";
                string title = "Playbbok Error";
                MessageBox.Show(message, title);
                eventPlaybook = new Playbook { EventID = "not found", StartDateAlternative = "0/0/0000"};
            }

            //List<Database> eventPopData = converters.DatabasePopulator(databasePath, eventPlaybook.startDate); //this isn't working

            Dictionary<string, string> popDict = database.GetPopDict(eventPlaybook.StartDateAlternative, databasePath);

            NewRoot newRoot = new NewRoot(eventPlaybook, popDict);
            StoreButtonAppearance sba = new StoreButtonAppearance(eventID, popDict);

            PurchaseScreenAppearance psA = new PurchaseScreenAppearance(eventID, popDict);
            MainHubAppearance mhA = new MainHubAppearance(eventID, popDict);
            Tier tiers = new Tier();

            Appearance appearance = new Appearance(sba, psA, mhA);
            newRoot.appearance = appearance;

            List<Gacha> gachaList = converters.GachaPopulator(gachaPath);
            newRoot.prizes = gacha.PrizeList(gachaList);
            newRoot.tiers = tiers.AssignGuarantee(tiers.GenerateTierList(gachaList), popDict);
            newRoot.lastChanceBoxPrizes = converters.AssignBoxValues(popDict);

            return newRoot;
        }
    }
}
