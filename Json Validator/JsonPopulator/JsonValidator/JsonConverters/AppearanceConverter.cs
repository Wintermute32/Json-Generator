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

        public Appearance GenerateAppearance()
        {
            StoreButtonConverter sBA = new StoreButtonConverter(form);
            PurchaseScreenConverter pSA = new PurchaseScreenConverter(form);
            MainHubConverter mHA = new MainHubConverter(form);

            var comboBoxes = form.Controls.OfType<ComboBox>().ToList();
            var checkBoxes = form.Controls.OfType<CheckBox>().ToList();


            Appearance appearance = new Appearance()
            {
                isEventBox = checkBoxes.Find(x => x.Name == "isEventCheck").Checked,
                mysteryBoxType = comboBoxes.Find(x => x.Name == "MysteryBoxCB").Text,
                isOEDBox = checkBoxes.Find(x => x.Name == "oedBoxCheck").Checked,
                isVIPBox = checkBoxes.Find(x => x.Name == "isVIPBoxCheck").Checked,

                theme = comboBoxes.Find(x => x.Name == "themeCB").Text,
                storeButtonAppearance = sBA.GenerateStoreBA(),
                purchaseScreenAppearance = pSA.GeneratePurchaseScreenApeparance(),
                mainHubAppearance = mHA.GenerateHubApp(),
            };

            return appearance;
        }
     
    }
}
