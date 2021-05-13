using System.Collections.Generic;
using System.Diagnostics;

namespace JsonValidator
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

        public StoreButtonAppearance(string ipTitle, Dictionary<string, string> popDict)
        {
            style = "LargePink";
            ribbonLocalizationKey = "EventBoxRibbon";
            titleLocalizationKey = ipTitle + "BoxTitle";
            subtitleLocalizationKey = "";
            popIds = getPopIds(popDict);
            order = 2;
            discount = 0;
        }

        public List<string> getPopIds(Dictionary<string, string> popDict)
        {
            List<string> rarities = new List<string>() { "rare", "epic", "legendary", "common" };
            List<string> whatever = new List<string>();

            for (int i = 0; i < rarities.Count; i++)
            {
                foreach (KeyValuePair<string, string> entry in popDict)
                    if (entry.Value == rarities[i])
                    {
                        whatever.Add(entry.Key);
                        Debug.WriteLine("store button entries are:" + entry.Key);
                    }
            }

            return whatever;
        }
    }

}
