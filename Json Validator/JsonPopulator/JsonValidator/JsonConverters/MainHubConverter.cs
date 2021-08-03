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
        //public MainHubAppearance GenerateHubApp()
        //{
        //    var flowBoxes = form.Controls.OfType<FlowLayoutPanel>().ToList();
        //    var checkBoxes = form.Controls.OfType<CheckBox>().ToList();
        //    var comboBoxes = form.Controls.OfType<ComboBox>().ToList();

        //    var popLists = flowBoxes.Find(x => x.Name == "mainHubPanel").Controls.OfType<ComboBox>().ToList();
        //    List<string> _popIds = new List<string>();

        //    foreach (var x in popLists) //break me into a new class with store appearance
        //    {
        //        _popIds.Add(x.Text);
        //    }

        //    MainHubAppearance mainHubApp = new MainHubAppearance()
        //    {
        //        CanShowInCarousel = checkBoxes.Find(x => x.Name == "canShowCarouselBox").Checked,
        //        Style = comboBoxes.Find(x => x.Name == "style2CB").Text,
        //        TitleLocKey = comboBoxes.Find(x => x.Name == "titleLocKeyCB").Text,
        //        SubtitleLocKey = comboBoxes.Find(x => x.Name == "mainhubSubLocKey").Text,
        //        PopIds = _popIds,
                
        //    };
        //    return mainHubApp;
        //}
    }
}
