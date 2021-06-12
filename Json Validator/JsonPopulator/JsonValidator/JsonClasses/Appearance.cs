using System;
using Newtonsoft.Json;
namespace JsonValidator
{
    public class Appearance
    {
        [JsonIgnore]
        public bool isOEDBox { get; set; }
        
        [JsonIgnore]
        public bool isHolidayBox { get; set; }

        [JsonIgnore]
        public bool isVIPBox { get; set; }
        public bool isEventBox { get; set; } //not ignoring since this is a field on the Json
        public string mysteryBoxType { get; set; }
        public string theme { get; set; }
        public StoreButtonAppearance storeButtonAppearance { get; set; }
        public PurchaseScreenAppearance purchaseScreenAppearance { get; set; }
        public MainHubAppearance mainHubAppearance { get; set; }
        
        public Appearance(StoreButtonAppearance sbA, PurchaseScreenAppearance psA, MainHubAppearance mhA)
        {
            isEventBox = true;
            mysteryBoxType = "LuckyMystery";
            theme = "";
            storeButtonAppearance = sbA;
            purchaseScreenAppearance = psA;
            mainHubAppearance = mhA;
        }

        public Appearance()
        { }
    }

}
