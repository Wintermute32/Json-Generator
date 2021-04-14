using System;
using System.Collections.Generic;
using System.Text;

namespace JsonPopulator
{

    public class PurchaseScreenAppearance
    {
        public string titleLocalizationKey { get; set; }
        public List<string> popIds { get; set; }


    }

    public class MainHubAppearance
    {
        public bool canShowInCarousel { get; set; }
        public string style { get; set; }
        public string titleLocalizationKey { get; set; }
        public string subtitleLocalizationKey { get; set; }
        public List<string> popIds { get; set; }
    }

    public class Guarantee
    {
        public string SpecificPopId { get; set; }
        public int specificPopAmount { get; set; }
        public bool? LuckyPopPrize { get; set; }
    }

    public class Tier
    {
        public int cost { get; set; }
        public int numPulls { get; set; }
        public Guarantee guarantee { get; set; }
    }

    public class LastChanceBoxPrize
    {
        public string rewardType { get; set; }
        public string rewardId { get; set; }
        public int amount { get; set; }
        public int instances { get; set; }
    }

}
