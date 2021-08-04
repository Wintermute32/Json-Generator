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
            Database database = new Database();
            Gacha gacha = new Gacha();
            Playbook eventPlaybook = new Playbook();
            Tier tiers = new Tier();

            eventPlaybook = UpdatePlaybookObj(playbookPath, eventID, eventPlaybook);

            Dictionary<string, string> popDict = database.GetPopDict(eventPlaybook.StartDateAlternative, databasePath);
            StoreButtonAppearance sba = new StoreButtonAppearance(eventID, popDict);
            PurchaseScreenAppearance psA = new PurchaseScreenAppearance(eventID, popDict);
            MainHubAppearance mhA = new MainHubAppearance(eventID, popDict);
            List<Gacha> gachaList = gacha.GachaPopulator(gachaPath);
            NewRoot newRoot = new NewRoot(eventPlaybook, popDict);

            newRoot.appearance = new Appearance(sba, psA, mhA);
            newRoot.prizes = gacha.PrizeList(gachaList);
            newRoot.tiers = tiers.AssignGuarantee(tiers.GenerateTierList(gachaList), popDict);
            newRoot.LastChanceBoxPrizes = NewRootGeneration.AssignBoxValues(popDict);

            return newRoot;
       }
       private static Playbook UpdatePlaybookObj(string playbookPath, string eventID, Playbook eventPlaybook)
       {
            try
            {
                eventPlaybook = eventPlaybook.PlaybookPopulator(playbookPath, eventID);
                eventPlaybook.FixStartDate(eventPlaybook.StartDate);
            }
            catch (Exception)
            {

                string message = "This ID wasn't found in the playbook. Check playbook ID and update";
                string title = "Playbook Error";
                MessageBox.Show(message, title);
                eventPlaybook = new Playbook { EventID = "not found", StartDateAlternative = "0/0/0000" };
            }

            return eventPlaybook;
       }
    }
}
