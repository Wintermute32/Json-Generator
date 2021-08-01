using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;
using JsonValidator.JsonConverters;
using JsonValidator.StoreConfigUpdate;

namespace JsonValidator
{
    public class NewRootGeneration
    {
        private FormatBoxString fbs = new FormatBoxString();
        public void GenerateMyJson(Form1 form)
        {
            var finalRoot = GenerateNewRoot(form);
            var isEventBox = UpdateBoxType(finalRoot);
            var jsonOutput = FormatNewJson(finalRoot, isEventBox);
            var complete = GetTestFilePath();

            File.WriteAllText(complete, jsonOutput);
            System.Diagnostics.Process.Start(complete);
        }
        private string FormatNewJson(NewRoot finalRoot, bool isEventBox)
        {
            var jsonOutput = fbs.TestFormatString(SerializeJson(finalRoot));
           
            if (isEventBox)
            {
                jsonOutput = jsonOutput.TrimEnd() + ','; //removing white space and adding comma
                finalRoot.LastChanceBoxPrizes = null;
                jsonOutput += '\n' + fbs.TestFormatString(SerializeJson(finalRoot));
                return jsonOutput;
            }
            jsonOutput = jsonOutput.TrimEnd() + ',';         
            return jsonOutput;
        }
        public NewRoot GenerateNewRoot(Form1 form)
        {
            AppearanceConverter appearance = new AppearanceConverter(form);
            PrizesConverter prizesConverter = new PrizesConverter();
            TierConverter tierConverter = new TierConverter();

            var comboBoxes = form.Controls.OfType<ComboBox>().ToList();
            var flowBoxes = form.Controls.OfType<FlowLayoutPanel>().ToList();
            var dateTimePicker = form.Controls.OfType<DateTimePicker>().ToList();
            var checkBoxes = form.Controls.OfType<CheckBox>().ToList();
            var textBoxes = form.Controls.OfType<TextBox>().ToList();
            var boxTypeDict = GetBoxTypeDict(form);

            NewRoot finalRoot = new NewRoot()
            {
                BoxID = AmmendBoxID(comboBoxes, textBoxes, boxTypeDict),
                EventNumber = textBoxes.Find(x => x.Name == "eventNumBox").Text,
                FandomID = textBoxes.Find(x => x.Name == "fandomIdCB").Text,
                StartDate = dateTimePicker.Find(x => x.Name == "startDatePicker").Text,
                EndDate = dateTimePicker.Find(x => x.Name == "endDatePicker").Text,
                Appearance = appearance.GenerateAppearance(),
                BehaviorType = comboBoxes.Find(x => x.Name == "behaviorCB").Text,
                FeaturedPopIdList = GetFeaturedPops(flowBoxes),
                Prizes = prizesConverter.GeneratePrizeList(flowBoxes),
                Tiers = tierConverter.GenerateTierList(flowBoxes),
                LastChanceBoxPrizes = GetLastChanceList(flowBoxes),
            };

            finalRoot.FixDates(finalRoot.StartDate);

            return finalRoot;
        }
        private Dictionary<string, bool> GetBoxTypeDict(Form form)
        {
            var checkBoxes = form.Controls.OfType<CheckBox>().ToList();
            Dictionary<string, bool> boxTypeDict = new Dictionary<string, bool>()
            {
                {"isEventBox", checkBoxes.Find(x => x.Name == "isEventCheck").Checked},
                {"isOEDBox", checkBoxes.Find(x => x.Name == "oedBoxCheck").Checked},
                {"isOtherBox", checkBoxes.Find(x => x.Name == "IsOtherCheck").Checked}
            };
            return boxTypeDict;
        }
        private bool UpdateBoxType(NewRoot finalRoot)
        {
            if (finalRoot.Appearance.IsEventBox)
            {
                finalRoot.BoxReplacesID = finalRoot.BoxID;
                finalRoot.BoxID = finalRoot.BoxID.Replace("VIP0", "VIP1");
                finalRoot.LastChanceBoxPrizes = null;
                return true;
            }
            return false;
        }
        public string GetTestFilePath()
        {
            var systemPath = System.Environment.
                             GetFolderPath(
                                 Environment.SpecialFolder.CommonApplicationData
                             );
            
            var complete = Path.Combine(systemPath, "TestJson.Json");

            return complete;
        }
        public List<LastChanceBoxPrize> GetLastChanceList(List<FlowLayoutPanel> flowlist)
        {
            var popLists = flowlist.Find(x => x.Name == "lastChanceBoxPanel").Controls.OfType<GroupBox>().ToList();
            List<LastChanceBoxPrize> lastChanceList = new List<LastChanceBoxPrize>();

            foreach (var x in popLists)
            {
                var combos = x.Controls.OfType<ComboBox>().ToList();
                LastChanceBoxPrize lastCBox = new LastChanceBoxPrize();
                lastCBox.RewardType = combos[0].Text;
                lastCBox.RewardID = combos[1].Text;
                lastCBox.Amount = Convert.ToInt32(combos[2].Text);
                lastCBox.Instances = Convert.ToInt32(combos[3].Text);
                lastChanceList.Add(lastCBox);
            }
            return lastChanceList;
        }
        public List<string> GetFeaturedPops(List<FlowLayoutPanel> flowList)
        {
            var popLists = flowList.Find(x => x.Name == "storePopsPanel").Controls.OfType<ComboBox>().ToList();
            List<string> _popIds = new List<string>();

            foreach (var x in popLists)
            {
                _popIds.Add(x.Text);
            }
            return _popIds;
        }     
        public string SerializeJson(NewRoot newRoot)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            };

            settings.Converters.Add(new MyConverter());
           
            string json = JsonConvert.SerializeObject(newRoot, settings);
            return json;
        }
        public string AmmendBoxID(List<ComboBox> comboBoxes, List<TextBox> textBoxes, Dictionary<string, bool> boxTypeDict)
        {
            var formBoxId = comboBoxes.Find(x => x.Name == "boxIdCB").Text; ;
            var formBoxNum = textBoxes.Find(x => x.Name == "eventNumBox").Text;
     
            string boxId = formBoxId;

            if (boxTypeDict["isEventBox"])
                 boxId = "e" + formBoxNum + "_bxtFE_VIP0_" + formBoxId.Substring(formBoxId.IndexOf('_') + 1);

            if (boxTypeDict["isOEDBox"])
                 boxId = "e" + formBoxNum + "_bxtOED_VIP0_" + formBoxId.Substring(formBoxId.IndexOf('_') + 1);

            if (boxTypeDict["isOtherBox"])
                boxId = "e" + formBoxNum + "_bxtOther_VIP0_" + formBoxId.Substring(formBoxId.IndexOf('_') + 1);

            if (boxId.Contains("mard"))
                boxId = boxId.Replace("_mardFE", "");

            if (boxId.Contains("marc"))
                boxId = boxId.Replace("_marcFE", "");

            return boxId;
        }
    }
} 
