using System.Collections.Generic;

namespace JsonValidator
{
    public class MainHubAppearance
    {
        public bool canShowInCarousel { get; set; }
        public string style { get; set; }
        public string titleLocalizationKey { get; set; }
        public string subtitleLocalizationKey { get; set; }
        public List<string> popIds { get; set; }
        public MainHubAppearance() { }
        public MainHubAppearance(string fandomName, Dictionary<string, string> popDict)
        {
            List<string> mainHubRarities = new List<string>() { "epic", "legendary", "rare" };
            List<string> addToMePopId = new List<string>();

            canShowInCarousel = true;
            titleLocalizationKey = fandomName + "BoxTitle";
            style = "Pink";
            
            foreach (var x in mainHubRarities)
                foreach (KeyValuePair<string, string> entry in popDict)
                    if (x == entry.Value)
                        addToMePopId.Add(entry.Key);

            popIds = addToMePopId;
        }
    }
}
