using System;
using System.Collections.Generic;
using System.Text;
using JsonValidator.CSV;

namespace JsonValidator
{
     public class NewRoot
    {
        public string boxId { get; set; }
        public string fandomId { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public Appearance appearance { get; set; }
        public string behaviourType { get; set; }
        public List<string> featuredPopIdsList { get; set; }
        public List<Prize> prizes { get; set; }
        public List<Tier> tiers { get; set; }
        public List<LastChanceBoxPrize> lastChanceBoxPrizes { get; set; }

        List<string> rarityList = new List<string>() { "common", "rare", "epic", "legendary" };

        public NewRoot(){}

        public NewRoot(Playbook playbook, Dictionary<string, string> popDict)
        {
            behaviourType = "";
            boxId = "e" + playbook.eventNumber + "_" + playbook.boxID.Trim();
            fandomId = playbook.fandomName;
            FixDates(playbook.startDate, playbook.endDate);
            SetFeaturedPopIds(popDict);
        }
        public void FixDates(string startDate, string endDate)
        {

           var revisedStart = DateTime.Parse(startDate).ToString("MM/dd/yyyy HH:mm");
           var revisedEnd = DateTime.Parse(endDate).AddDays(-1).ToString("MM/dd/yyyy HH:mm");

            this.startDate = revisedStart;
            this.endDate = revisedEnd;
        }

        public void SetFeaturedPopIds(Dictionary<string, string> popIdDict)
        {
            foreach (KeyValuePair<string, string> entry in popIdDict)
                for (int i = 0; i < rarityList.Count; i++)
                    if (entry.Value == rarityList[i])
                        rarityList[i] = entry.Key;

            featuredPopIdsList = rarityList;
        }


    }
}
