using System;
using System.Collections.Generic;
using System.Text;
using JsonValidator.CSV;
using Newtonsoft.Json;

namespace JsonValidator
{
    public class NewRoot //The Top Class for all Json classes
    { 
        public string BoxID { get; set; }
        [JsonIgnore]
        public string EventNumber { get; set; }
        public string BoxReplacesID { get; set; }
        public string FandomID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Appearance Appearance { get; set; }
        public string BehaviorType { get; set; }
        public List<string> FeaturedPopIdList { get; set; }
        public List<Prize> Prizes { get; set; }
        public List<Tier> Tiers { get; set; }
        public List<LastChanceBoxPrize> LastChanceBoxPrizes { get; set; }         
        public NewRoot(){} //Used when generating TestJson
        public NewRoot(Playbook playbook, Dictionary<string, string> popDict) //used to populate Winforms UI
        {
            BehaviorType = "PullBased";
            BoxID = "e" + playbook.EventNumber + "_bxtFE_VIP0_" + playbook.BoxID.Trim();
            EventNumber = playbook.EventNumber;
            FandomID = playbook.FandomName;
            FixDates(playbook.StartDate);
            SetFeaturedPopIds(popDict);
        } 
        public void FixDates(string startDate)
        {
           var revisedStart = DateTime.Parse(startDate).ToString("MM/dd/yyyy HH:mm");
           var revisedEnd = DateTime.Parse(startDate).AddDays(6).ToString("MM/dd/yyyy HH:mm");

           this.StartDate = revisedStart;
           this.EndDate = revisedEnd;
        }
        private void SetFeaturedPopIds(Dictionary<string, string> popIdDict)
        {
            List<string> rarityList = new List<string>() { "common", "rare", "epic", "legendary" };
            
            foreach (KeyValuePair<string, string> entry in popIdDict)
                for (int i = 0; i < rarityList.Count; i++)
                    if (entry.Value == rarityList[i])
                        rarityList[i] = entry.Key;

            FeaturedPopIdList = rarityList;
        }
    }
}
