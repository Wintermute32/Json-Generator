using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JsonValidator.JsonConverters
{
    class PurchaseScreenConverter
    {
        Form1 form;
        public PurchaseScreenConverter(Form1 form)
        {
            this.form = form;
        }
        //public PurchaseScreenAppearance GeneratePurchaseScreenApeparance()
        //{
        //    var flowBoxes = form.Controls.OfType<FlowLayoutPanel>().ToList();
        //    var textBoxes = form.Controls.OfType<TextBox>().ToList();

        //    var popLists = flowBoxes.Find(x => x.Name == "purchasePopsPanel").Controls.OfType<ComboBox>().ToList();
        //    List<string> _popIds = new List<string>();

        //    foreach (var x in popLists) //break me into a new class with store appearance
        //    {
        //        _popIds.Add(x.Text);
        //    }

        //    PurchaseScreenAppearance purchaseScreenAppearance = new PurchaseScreenAppearance()
        //    {
        //        TitleLocKey = textBoxes.Find(x => x.Name == "purTitleLocKey").Text,
        //        PopIds = _popIds
        //    };

        //    return purchaseScreenAppearance;
        //}
    }
}
