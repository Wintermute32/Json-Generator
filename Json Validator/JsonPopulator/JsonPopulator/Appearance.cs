namespace JsonPopulator
{
    public class Appearance
    {
        public bool isEventBox { get; set; }
        public string mysteryBoxType { get; set; }
        public string theme { get; set; }
        public StoreButtonAppearance storeButtonAppearance { get; set; }
        public PurchaseScreenAppearance purchaseScreenAppearance { get; set; }
        public MainHubAppearance mainHubAppearance { get; set; }


        public Appearance(StoreButtonAppearance sbA)
        {
            isEventBox = true;
            mysteryBoxType = "LuckyMystery";
            theme = "";
            storeButtonAppearance = sbA;

        }
    }

}
