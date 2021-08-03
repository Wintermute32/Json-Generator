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
        public static StoreButtonAppearance GenerateStoreBA(Form1 form)
        {
            var comboBoxes = form.Controls.OfType<ComboBox>().ToList();
            var flowBoxes = form.Controls.OfType<FlowLayoutPanel>().ToList();

            var popLists = flowBoxes.Find(x => x.Name == "storePopsPanel").Controls.OfType<ComboBox>().ToList();
            List<string> _popIds = new List<string>();

            foreach (var x in popLists) //break me into a new class with store appearance
            {
                _popIds.Add(x.Text);
            }

            StoreButtonAppearance storeButtonAppearance = new StoreButtonAppearance()
            {
                Style = comboBoxes.Find(x => x.Name == "styleCB").Text,
                RibbonLocKey = comboBoxes.Find(x => x.Name == "ribbonLocKeyCB").Text,
                TitleLocKey = comboBoxes.Find(x => x.Name == "titleLocCB").Text,
                SubtitleLocKey = comboBoxes.Find(x => x.Name == "subLocCB").Text,
                PopIds = _popIds,
                Order = Convert.ToInt32(comboBoxes.Find(x => x.Name == "orderCB").Text),
                Discount = Convert.ToInt32(comboBoxes.Find(x => x.Name == "discountCB").Text)
            };

            return storeButtonAppearance;
        }
    }
}
