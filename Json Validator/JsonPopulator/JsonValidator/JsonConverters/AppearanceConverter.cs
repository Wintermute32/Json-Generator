using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JsonValidator.JsonConverters
{
    class AppearanceConverter
    {
        Form1 form;
        public AppearanceConverter(Form1 form)
        {
            this.form = form;
        }
        //public Appearance GenerateAppearance()
        //{
        //    StoreButtonConverter sBA = new StoreButtonConverter(form);
        //    PurchaseScreenConverter pSA = new PurchaseScreenConverter(form);
        //    MainHubConverter mHA = new MainHubConverter(form);

        //    var comboBoxes = form.Controls.OfType<ComboBox>().ToList();
        //    var checkBoxes = form.Controls.OfType<CheckBox>().ToList();

        //    Appearance appearance = new Appearance()
        //    {
        //        IsEventBox = checkBoxes.Find(x => x.Name == "isEventCheck").Checked,
        //        MysteryBoxType = comboBoxes.Find(x => x.Name == "MysteryBoxCB").Text,
        //        IsOEDBox = checkBoxes.Find(x => x.Name == "oedBoxCheck").Checked, //do i need this?

        //        Theme = comboBoxes.Find(x => x.Name == "themeCB").Text,
        //        StoreButtonAppearance = sBA.GenerateStoreBA(),
        //        PurchaseScreenAppearance = pSA.GeneratePurchaseScreenApeparance(),
        //        MainHubAppearance = mHA.GenerateHubApp(),
        //    };

        //    return appearance;
        //}
     
    }
}
