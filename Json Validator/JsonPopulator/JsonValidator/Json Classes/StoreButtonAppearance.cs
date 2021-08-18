using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System;

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
        public StoreButtonAppearance ()
        { }
        public StoreButtonAppearance(string ipTitle, Dictionary<string, string> popDict)
        {
            //these are hardcoded values. Need to make them dynamic if trying to add non event box
            style = "LargePink";
            ribbonLocalizationKey = "EventBoxRibbon";
            titleLocalizationKey = ipTitle + "BoxTitle";
            subtitleLocalizationKey = "";
            popIds = getPopIds(popDict);
            order = 2;
            discount = 0;
        }
        public StoreButtonAppearance (Form1 form)
        {
            var comboBoxes = form.Controls.OfType<ComboBox>().ToList();
            var flowBoxes = form.Controls.OfType<FlowLayoutPanel>().ToList();

            var popLists = flowBoxes.Find(x => x.Name == "storePopsPanel").Controls.OfType<ComboBox>().ToList();
            List<string> _popIds = new List<string>();

            foreach (var x in popLists) //break me into a new class with store appearance
            {
                _popIds.Add(x.Text);
            }

            style = comboBoxes.Find(x => x.Name == "styleCB").Text;
            ribbonLocalizationKey = comboBoxes.Find(x => x.Name == "ribbonLocKeyCB").Text;
            titleLocalizationKey = comboBoxes.Find(x => x.Name == "titleLocCB").Text;
            subtitleLocalizationKey = comboBoxes.Find(x => x.Name == "subLocCB").Text;
            popIds = _popIds;
            order = Convert.ToInt32(comboBoxes.Find(x => x.Name == "orderCB").Text);
            discount = Convert.ToInt32(comboBoxes.Find(x => x.Name == "discountCB").Text);
        }
        private List<string> getPopIds(Dictionary<string, string> popDict)
        {
            if (popDict == null)
                return new List<string>();

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
