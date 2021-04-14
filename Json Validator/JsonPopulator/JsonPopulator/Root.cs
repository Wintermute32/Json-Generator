using System.Collections.Generic;

namespace JsonPopulator
{
    public class Root
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

        public Root(string eventName, LivePlaybook livePlaybook)
        {
            GetRootValues(eventName, livePlaybook);
        }

        public void GetRootValues(string eventName, LivePlaybook livePlay)
        {
            boxId = livePlay.boxID;
            fandomId = eventName + "Fandom";
            startDate = livePlay.startDate;
            endDate = livePlay.endDate;
            behaviourType = "";
        }

        public void SetFeaturedPopIds(Dictionary<string, string> popIdDict)
        {
            List<string> whatever = new List<string>() { "common", "rare", "epic", "legendary", "event exclusive" };

            foreach (KeyValuePair<string, string> entry in popIdDict)
                for (int i = 0; i < popIdDict.Count; i++)
                    if (entry.Value == whatever[i])
                        whatever[i] = entry.Key;

            featuredPopIdsList = whatever;
        }

    }

}
