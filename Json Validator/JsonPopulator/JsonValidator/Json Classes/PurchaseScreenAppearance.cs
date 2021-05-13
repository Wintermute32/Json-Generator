using System.Collections.Generic;

namespace JsonValidator
{
    public class PurchaseScreenAppearance
    {
        public string titleLocalizationKey { get; set; }
        public List<string> popIds { get; set; }

        public PurchaseScreenAppearance(string fandomName, Dictionary<string, string> popS)
        {
            List<string> purchaseRarities = new List<string>() {"common", "rare", "epic", "legendary"};
            List<string> addToMePopId = new List<string>();

            foreach (var x in purchaseRarities)
                foreach (KeyValuePair<string, string> entry in popS)
                    if (x == entry.Value)
                        addToMePopId.Add(entry.Key);

            popIds = addToMePopId;
            titleLocalizationKey = fandomName + "BoxTitle";
        }
    }

}
