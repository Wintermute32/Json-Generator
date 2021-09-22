﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JsonValidator
{
    public class PurchaseScreenAppearance
    {
        public string titleLocalizationKey { get; set; }
        public List<string> popIds { get; set; }
        public PurchaseScreenAppearance() { }
        public PurchaseScreenAppearance(string fandomName, Dictionary<string, string> pops)
        {
            if (pops == null)
                return; 

            List<string> purchaseRarities = new List<string>() {"common", "rare", "epic", "legendary"};
            List<string> addToMePopId = new List<string>();

            foreach (var x in purchaseRarities)
                foreach (KeyValuePair<string, string> entry in pops)
                    if (x == entry.Value)
                        addToMePopId.Add(entry.Key);

            popIds = addToMePopId;
            titleLocalizationKey = fandomName + "BoxTitle";
        }
        public PurchaseScreenAppearance (Form1 form)
        {
            var flowBoxes = form.Controls.OfType<FlowLayoutPanel>().ToList();
            var textBoxes = form.Controls.OfType<TextBox>().ToList();

            var popLists = flowBoxes.Find(x => x.Name == "purchasePopsPanel").Controls.OfType<ComboBox>().ToList();
            List<string> _popIds = new List<string>();

            foreach (var x in popLists) //break me into a new class with store appearance
            {
                _popIds.Add(x.Text);
            }

            titleLocalizationKey = textBoxes.Find(x => x.Name == "purTitleLocKey").Text;
            popIds = _popIds;

        }
    }

}