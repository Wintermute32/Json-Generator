using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JsonValidator
{
    public class JsonGeneration
    {

        //should probably go back and name these objects to reference and assign to New Root properties
        public void GenerateMyJson(Form1 form)
        {
            var comboBoxes = form.Controls.OfType<ComboBox>().ToList();
            var flowBoxes = form.Controls.OfType<FlowLayoutPanel>().ToList();
            var dateTimePicker = form.Controls.OfType<DateTimePicker>().ToList();
            var checkBoxes = form.Controls.OfType<CheckBox>().ToList();
            var textBoxes = form.Controls.OfType<TextBox>().ToList();

            var popLists = flowBoxes.Find(x => x.Name == "storePopsPanel").Controls.OfType<ComboBox>().ToList();
            List<string> _popIds = new List<string>();
            
            foreach (var x in popLists) //break me into a new class with store appearance
            {
                _popIds.Add(x.Text);
            }

            StoreButtonAppearance storeButtonAppearance = new StoreButtonAppearance()
            {
                style = comboBoxes.Find(x => x.Name == "styleCB").Text,
                ribbonLocalizationKey = comboBoxes.Find(x => x.Name == "ribbonLocKeyCB").Text,
                titleLocalizationKey = comboBoxes.Find(x => x.Name == "subLocKeyCB").Text,
                popIds = _popIds,
                order = Convert.ToInt32(comboBoxes.Find(x => x.Name == "orderCB").Text),
                discount = Convert.ToInt32(comboBoxes.Find(x => x.Name == "discountCB").Text)
            };

            Appearance appearance = new Appearance()
            {
                isEventBox = checkBoxes.Find(x => x.Name == "isEventCheck").Checked,
                mysteryBoxType = comboBoxes.Find(x => x.Name == "MysteryBoxCB").Text,
                theme = comboBoxes.Find(x => x.Name == "themeCB").Text,
                storeButtonAppearance = storeButtonAppearance
              
            };

            NewRoot finalRoot = new NewRoot()
            {
                boxId = comboBoxes.Find(x => x.Name == "boxIdCB").Text,
                fandomId = textBoxes.Find(x => x.Name == "fandomIdCB").Text,
                startDate = dateTimePicker.Find(x => x.Name == "startDatePicker").Text,
                endDate = dateTimePicker.Find(x => x.Name == "endDatePicker").Text,
                appearance = appearance
            };

            Debug.WriteLine(SerializeJson(finalRoot));
        }
      public string SerializeJson(NewRoot newRoot)
      {
            //Accepts Completed NewRoot Object
            var serializerSettings = new JsonSerializerSettings();

            string rootOutput = JsonConvert.SerializeObject(newRoot, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            return rootOutput;
      }
    }
} 
