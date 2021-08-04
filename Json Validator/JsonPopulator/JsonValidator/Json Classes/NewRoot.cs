using System;
using System.Collections.Generic;
using System.Text;
using JsonValidator.CSV;
using Newtonsoft.Json;

namespace JsonValidator
{
    public class NewRoot //The Top Class for all Json classes
    { 
        public string boxId { get; set; }
        [JsonIgnore]
        public string EventNumber { get; set; }
        public string boxVipReplaces { get; set; }
        public string fandomId { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public Appearance appearance { get; set; }
        public string behaviorType { get; set; }
        public List<string> featuredPopIds { get; set; }
        public List<Prize> prizes { get; set; }
        public List<Tier> tiers { get; set; }
        public List<LastChanceBoxPrize> LastChanceBoxPrizes { get; set; }         
        public NewRoot(){} //Used when generating TestJson
        public NewRoot(Playbook playbook, Dictionary<string, string> popDict) //used to populate Winforms UI
        {
            behaviorType = "PullBased";
            boxId = "e" + playbook.EventNumber + "_bxtFE_VIP0_" + playbook.BoxID.Trim();
            EventNumber = playbook.EventNumber;
            fandomId = playbook.FandomName;
            FixDates(playbook.StartDate);
            SetFeaturedPopIds(popDict);
        } 
        public void FixDates(string startDate)
        {
           var revisedStart = DateTime.Parse(startDate).ToString("MM/dd/yyyy HH:mm");
           var revisedEnd = DateTime.Parse(startDate).AddDays(6).ToString("MM/dd/yyyy HH:mm");

           this.startDate = revisedStart;
           this.endDate = revisedEnd;
        }
        private void SetFeaturedPopIds(Dictionary<string, string> popIdDict)
        {
            List<string> rarityList = new List<string>() { "common", "rare", "epic", "legendary" };
            
            foreach (KeyValuePair<string, string> entry in popIdDict)
                for (int i = 0; i < rarityList.Count; i++)
                    if (entry.Value == rarityList[i])
                        rarityList[i] = entry.Key;

            featuredPopIds = rarityList;
        }
    }
}
