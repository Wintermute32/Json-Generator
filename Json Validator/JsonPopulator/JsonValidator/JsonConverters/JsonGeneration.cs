using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using JsonValidator.JsonConverters;

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

            AppearanceConverter appearance = new AppearanceConverter(form);
            PrizesConverter prizesConverter = new PrizesConverter();

            NewRoot finalRoot = new NewRoot()
            {
                boxId = comboBoxes.Find(x => x.Name == "boxIdCB").Text,
                fandomId = textBoxes.Find(x => x.Name == "fandomIdCB").Text,
                startDate = dateTimePicker.Find(x => x.Name == "startDatePicker").Text,
                endDate = dateTimePicker.Find(x => x.Name == "endDatePicker").Text,
                appearance = appearance.GenerateAppearance(),
                behaviourType = comboBoxes.Find(x => x.Name == "behaviorCB").Text,
                featuredPopIdsList = GetFeaturedPops(flowBoxes),
                prizes = prizesConverter.GeneratePrizeList(flowBoxes),
            };
            GenerateTierList(flowBoxes);

            Debug.WriteLine(SerializeJson(finalRoot));
        }

        public void GenerateTierList(List<FlowLayoutPanel> flowlist)
        {

            // all have text box, cost
            // combo box, numpulls
            // either popid(l), or bool value(m)
            //l has combo box, amount

            var popLists = flowlist.Find(x => x.Name == "tierPanel").Controls.OfType<GroupBox>().ToList();
            Debug.WriteLine("poplist count: " + popLists.Count);
            List<ITierBox> tierpoplist = new List<ITierBox>();

            foreach (var x in popLists)
            {
                // logic for picking how to assign the various TierBox values
                // Get the type of box (L M or S)
                // Then assign proper values to the Tier object.
                // Return list of Tiers.
                Tier tier = new Tier();
                var comboB = x.Controls.OfType<ComboBox>().ToList();
                if (comboB.Count > 2)
                    for (int i )
               





            }

        }

        public List<string> GetFeaturedPops(List<FlowLayoutPanel> flowList)
        {
             var popLists = flowList.Find(x => x.Name == "storePopsPanel").Controls.OfType<ComboBox>().ToList();
            List<string> _popIds = new List<string>();

            foreach (var x in popLists) //break me into a new class with store appearance
            {
                _popIds.Add(x.Text);
            }
            return _popIds;
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
