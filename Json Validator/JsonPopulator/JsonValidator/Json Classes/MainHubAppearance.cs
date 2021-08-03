using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

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
        public static MainHubAppearance GenerateHubAppearance(Form1 form)
        {
            var flowBoxes = form.Controls.OfType<FlowLayoutPanel>().ToList();
            var checkBoxes = form.Controls.OfType<CheckBox>().ToList();
            var comboBoxes = form.Controls.OfType<ComboBox>().ToList();

            var popLists = flowBoxes.Find(x => x.Name == "mainHubPanel").Controls.OfType<ComboBox>().ToList();
            List<string> _popIds = new List<string>();

            foreach (var x in popLists) //break me into a new class with store appearance
            {
                _popIds.Add(x.Text);
            }

            MainHubAppearance mainHubApp = new MainHubAppearance()
            {
                CanShowInCarousel = checkBoxes.Find(x => x.Name == "canShowCarouselBox").Checked,
                Style = comboBoxes.Find(x => x.Name == "style2CB").Text,
                TitleLocKey = comboBoxes.Find(x => x.Name == "titleLocKeyCB").Text,
                SubtitleLocKey = comboBoxes.Find(x => x.Name == "mainhubSubLocKey").Text,
                PopIds = _popIds,

            };
            return mainHubApp;
        }
    }
}
