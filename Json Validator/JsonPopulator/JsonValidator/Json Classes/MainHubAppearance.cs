using System.Collections.Generic;

namespace JsonValidator
{
    public class MainHubAppearance
    {
        public bool CanShowInCarousel { get; set; }
        public string Style { get; set; }
        public string TitleLocKey { get; set; }
        public string SubtitleLocKey { get; set; }
        public List<string> PopIds { get; set; }
        public MainHubAppearance() { }
        public MainHubAppearance(string fandomName, Dictionary<string, string> popDict)
        {
            List<string> mainHubRarities = new List<string>() { "epic", "legendary", "rare" };
            List<string> addToMePopId = new List<string>();

            CanShowInCarousel = true;
            TitleLocKey = fandomName + "BoxTitle";
            Style = "Pink";
            
            foreach (var x in mainHubRarities)
                foreach (KeyValuePair<string, string> entry in popDict)
                    if (x == entry.Value)
                        addToMePopId.Add(entry.Key);

            PopIds = addToMePopId;
        }
    }
}
