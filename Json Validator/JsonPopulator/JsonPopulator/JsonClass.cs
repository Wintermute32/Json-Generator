using System;
using System.Collections.Generic;
using System.Text;

namespace JsonPopulator
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class StoreButtonAppearance
    {
        public string style { get; set; }
        public string ribbonLocalizationKey { get; set; }
        public string titleLocalizationKey { get; set; }
        public string subtitleLocalizationKey { get; set; }
        public List<string> popIds { get; set; }
        public int order { get; set; }
        public int discount { get; set; }
    }

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

    public class Appearance
    {
        public bool isEventBox { get; set; }
        public string mysteryBoxType { get; set; }
        public string theme { get; set; }
        public StoreButtonAppearance storeButtonAppearance { get; set; }
        public PurchaseScreenAppearance purchaseScreenAppearance { get; set; }
        public MainHubAppearance mainHubAppearance { get; set; }
    }

    public class Prize
    {
        public string rewardType { get; set; }
        public string rewardId { get; set; }
        public int amount { get; set; }
        public int instances { get; set; }
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

    public class Root
    {

        public string boxId { get; set; }
        public string fandomId { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public Appearance appearance { get; set; }
        public string behaviourType { get; set; }
        public List<string> featuredPopIds { get; set; }
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

        public void GetFeaturedPopIds()
        {

        }


    }


}
