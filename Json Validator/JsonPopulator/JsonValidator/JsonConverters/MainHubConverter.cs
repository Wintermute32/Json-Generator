using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JsonValidator.JsonConverters
{
    class MainHubConverter
    {
        Form1 form;
        public MainHubConverter(Form1 form)
        {
            this.form = form;
        }
        public MainHubAppearance GenerateHubApp()
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
                canShowInCarousel = checkBoxes.Find(x => x.Name == "canShowCarouselBox").Checked,
                style = comboBoxes.Find(x => x.Name == "style2CB").Text,
                titleLocalizationKey = comboBoxes.Find(x => x.Name == "titleLocKeyCB").Text,
                subtitleLocalizationKey = comboBoxes.Find(x => x.Name == "mainhubSubLocKey").Text,
                popIds = _popIds,
                
            };
            return mainHubApp;
        }
    }
}
