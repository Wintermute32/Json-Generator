using System.Collections.Generic;

namespace JsonValidator
{
    public class PurchaseScreenAppearance
    {
        public string TitleLocKey { get; set; }
        public List<string> PopIds { get; set; }
        public PurchaseScreenAppearance() { }
        public PurchaseScreenAppearance(string fandomName, Dictionary<string, string> pops)
        {
            List<string> purchaseRarities = new List<string>() {"common", "rare", "epic", "legendary"};
            List<string> addToMePopId = new List<string>();

            foreach (var x in purchaseRarities)
                foreach (KeyValuePair<string, string> entry in pops)
                    if (x == entry.Value)
                        addToMePopId.Add(entry.Key);

            PopIds = addToMePopId;
            TitleLocKey = fandomName + "BoxTitle";
        }
    }

}
