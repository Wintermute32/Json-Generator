using System;
using System.Collections.Generic;
using System.Windows.Forms;
using JsonValidator.CSV;
using TicToc.GoogleSheets;

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
			Gacha gacha = new Gacha();
			Playbook eventPlaybook = new Playbook();
			Tier tiers = new Tier();
			eventPlaybook = Playbook.UpdatePlaybookObj(playbookPath, eventID, eventPlaybook);
			new Database(databasePath, eventPlaybook.StartDateAlternative);

			Dictionary<string, string> popDict = Database.PopDict; 
			StoreButtonAppearance sba = new StoreButtonAppearance(eventID, popDict);
			PurchaseScreenAppearance psA = new PurchaseScreenAppearance(eventID, popDict);
			MainHubAppearance mhA = new MainHubAppearance(eventID, popDict);
			//List<Gacha> gachaList = gacha.GachaPopulator(gachaPath);
			List<Gacha> gachaList = gacha.GoogleGachaPopulator(gachaPath);

			NewRoot newRoot = new NewRoot(eventPlaybook, popDict);

			newRoot.appearance = new Appearance(sba, psA, mhA);
			newRoot.prizes = gacha.GeneratePrizeList(gachaList, Database.PopIDList);
			newRoot.tiers = tiers.AssignGuarantee(tiers.GenerateTierList(gachaList), popDict);
			newRoot.lastChanceBoxPrizes = NewRootGeneration.AssignBoxValues(popDict);

			return newRoot;
	   }
 
	}
}
