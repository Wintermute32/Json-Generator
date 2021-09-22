using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

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
            subtitleLocalizationKey = "EventCarouselitemSubtitle";
            style = "Pink";
            
            foreach (var x in mainHubRarities)
                foreach (KeyValuePair<string, string> entry in popDict)
                    if (x == entry.Value)
                        addToMePopId.Add(entry.Key);

            popIds = addToMePopId;
        }
        public MainHubAppearance(Form1 form)
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

            canShowInCarousel = checkBoxes.Find(x => x.Name == "canShowCarouselBox").Checked;
            style = comboBoxes.Find(x => x.Name == "style2CB").Text;
            titleLocalizationKey = comboBoxes.Find(x => x.Name == "mainHubTitleLocKeyCB").Text;
            subtitleLocalizationKey = comboBoxes.Find(x => x.Name == "mainhubSubLocKey").Text;
            popIds = _popIds;
        }
    }
}
