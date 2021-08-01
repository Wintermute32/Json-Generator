using System.Collections.Generic;
using System.Diagnostics;

namespace JsonValidator
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class StoreButtonAppearance
    {
        public string Style { get; set; }
        public string RibbonLocKey { get; set; }
        public string TitleLocKey { get; set; }
        public string SubtitleLocKey { get; set; }
        public List<string> PopIds { get; set; }
        public int Order { get; set; }
        public int Discount { get; set; }
        public StoreButtonAppearance ()
        { }
        public StoreButtonAppearance(string ipTitle, Dictionary<string, string> popDict)
        {
            //these are hardcoded values. Need to make them dynamic if trying to add non event box
            Style = "LargePink";
            RibbonLocKey = "EventBoxRibbon";
            TitleLocKey = ipTitle + "BoxTitle";
            SubtitleLocKey = "";
            PopIds = getPopIds(popDict);
            Order = 2;
            Discount = 0;
        }
        private List<string> getPopIds(Dictionary<string, string> popDict)
        {
            List<string> rarities = new List<string>() { "rare", "legendary", "epic", "common" };
            List<string> orderedRarityList = new List<string>();

            for (int i = 0; i < rarities.Count; i++)
            {
                foreach (KeyValuePair<string, string> entry in popDict)
                    if (entry.Value == rarities[i])
                    {
                        orderedRarityList.Add(entry.Key);
                        Debug.WriteLine("store button entries are:" + entry.Key);
                    }
            }
            return orderedRarityList;
        }
    }
}
