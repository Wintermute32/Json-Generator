using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JsonValidator.JsonConverters
{
    public class StoreButtonConverter
    {
        Form1 form;
        public StoreButtonConverter(Form1 form)
        {
            this.form = form;
        }

        //public StoreButtonAppearance GenerateStoreBA()
        //{
        //    var comboBoxes = form.Controls.OfType<ComboBox>().ToList();
        //    var flowBoxes = form.Controls.OfType<FlowLayoutPanel>().ToList();

        //    var popLists = flowBoxes.Find(x => x.Name == "storePopsPanel").Controls.OfType<ComboBox>().ToList();
        //    List<string> _popIds = new List<string>();

        //    foreach (var x in popLists) //break me into a new class with store appearance
        //    {
        //        _popIds.Add(x.Text);
        //    }

        //    StoreButtonAppearance storeButtonAppearance = new StoreButtonAppearance()
        //    {
        //        Style = comboBoxes.Find(x => x.Name == "styleCB").Text,
        //        RibbonLocKey = comboBoxes.Find(x => x.Name == "ribbonLocKeyCB").Text,
        //        TitleLocKey = comboBoxes.Find(x => x.Name == "titleLocCB").Text,
        //        SubtitleLocKey = comboBoxes.Find(x => x.Name == "subLocCB").Text,
        //        PopIds = _popIds,
        //        Order = Convert.ToInt32(comboBoxes.Find(x => x.Name == "orderCB").Text),
        //        Discount = Convert.ToInt32(comboBoxes.Find(x => x.Name == "discountCB").Text)
        //    };

        //    return storeButtonAppearance;
        //}
    }
}
