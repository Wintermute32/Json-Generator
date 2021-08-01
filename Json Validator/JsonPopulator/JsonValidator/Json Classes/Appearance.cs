using System;
using Newtonsoft.Json;
namespace JsonValidator
{
    public class Appearance : NewRoot
    {
        [JsonIgnore]
        public bool IsOEDBox { get; set; } 
        [JsonIgnore]
        public bool IsHolidayBox { get; set; }
        [JsonIgnore]
        public bool IsVIPBox { get; set; }
        public bool IsEventBox { get; set; } //not ignoring since this is a field on the Json
        public string MysteryBoxType { get; set; }
        public string Theme { get; set; }
        public StoreButtonAppearance StoreButtonAppearance { get; set; }
        public PurchaseScreenAppearance PurchaseScreenAppearance { get; set; }
        public MainHubAppearance MainHubAppearance { get; set; }
        public Appearance(){ }
        public Appearance(StoreButtonAppearance sbA, PurchaseScreenAppearance psA, MainHubAppearance mhA)
        {
            IsEventBox = true;
            MysteryBoxType = "LuckyMystery";
            Theme = "";
            StoreButtonAppearance = sbA;
            PurchaseScreenAppearance = psA;
            MainHubAppearance = mhA;
        }
    }
}
