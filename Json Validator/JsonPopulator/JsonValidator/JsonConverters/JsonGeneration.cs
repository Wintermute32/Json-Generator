using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.IO;
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
            TierConverter tierConverter = new TierConverter();

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
                tiers = tierConverter.GenerateTierList(flowBoxes),
                lastChanceBoxPrizes = GetLastChanceList(flowBoxes),
            };

            Debug.WriteLine(FormatJson(finalRoot));
            File.WriteAllText(@"C:\Users\pdnud\OneDrive\Desktop\Json Validator\[1.5.0] mystery_boxes_config - Default.json", FormatJson(finalRoot));
            System.Diagnostics.Process.Start(@"C:\Users\pdnud\OneDrive\Desktop\Json Validator\[1.5.0] mystery_boxes_config - Default.json");

        }

        public List<LastChanceBoxPrize> GetLastChanceList(List<FlowLayoutPanel> flowlist)
        {
            var popLists = flowlist.Find(x => x.Name == "lastChanceBoxPanel").Controls.OfType<GroupBox>().ToList();
            List<LastChanceBoxPrize> lastChanceList = new List<LastChanceBoxPrize>();

            foreach (var x in popLists)
            {
                var combos = x.Controls.OfType<ComboBox>().ToList();
                LastChanceBoxPrize lastCBox = new LastChanceBoxPrize();
                lastCBox.rewardType = combos[0].Text;
                lastCBox.rewardId = combos[1].Text;
                lastCBox.amount = Convert.ToInt32(combos[2].Text);
                lastCBox.instances = Convert.ToInt32(combos[3].Text);
                lastChanceList.Add(lastCBox);
            }

            return lastChanceList;
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

        public string FormatJson(NewRoot newRoot)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
            };

            settings.Converters.Add(new MyConverter());

            string json = JsonConvert.SerializeObject(newRoot, settings);
            return json;
        }

    }
    class MyConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) //controlling what types are affected with formatting, indented or none
        {
            return objectType == typeof(string[]) || objectType == typeof(Tier) || objectType == typeof(Prize) 
                || objectType == typeof(List<string>) || objectType == typeof(LastChanceBoxPrize);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(JsonConvert.SerializeObject(value, Formatting.None));
        }
    }

} 
