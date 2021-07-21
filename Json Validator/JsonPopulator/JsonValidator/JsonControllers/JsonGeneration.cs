using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;
using JsonValidator.JsonConverters;
using JsonValidator.StoreConfigUpdate;

namespace JsonValidator
{
    public class JsonGeneration
    {
        //all members for generating the Json File
        //Add functionality for popup: look at the test file. Edit test file as necessary,
        //then click final UI button to add to existing MysteryBoxFiles. 
        public string JsonTestPath {get;set;}
        public void GenerateMyJson(Form1 form)
        {

            //refactor this
            var comboBoxes = form.Controls.OfType<ComboBox>().ToList();
            var flowBoxes = form.Controls.OfType<FlowLayoutPanel>().ToList();
            var dateTimePicker = form.Controls.OfType<DateTimePicker>().ToList();
            var checkBoxes = form.Controls.OfType<CheckBox>().ToList();
            var textBoxes = form.Controls.OfType<TextBox>().ToList();

            //classes for assigning form values to NewRoot object properties
            AppearanceConverter appearance = new AppearanceConverter(form);
            PrizesConverter prizesConverter = new PrizesConverter();
            TierConverter tierConverter = new TierConverter();
            FormatBoxString fbs = new FormatBoxString();

            bool isEventBox = checkBoxes.Find(x => x.Name == "isEventCheck").Checked;
            bool isOEDBox = checkBoxes.Find(x => x.Name == "oedBoxCheck").Checked;
            bool isOtherBox = checkBoxes.Find(x => x.Name == "OtherBoxCheck").Checked;

            Dictionary<string, bool> boxTypeDict = new Dictionary<string, bool>()
            { {"isEventBox", isEventBox },{"isOEDBox", isOEDBox },{"isOtherBox", isOtherBox } };

            NewRoot finalRoot = new NewRoot()
            {
                boxId = AmmendBoxID(comboBoxes, textBoxes, boxTypeDict),
                evetnNumber = textBoxes.Find(x => x.Name == "eventNumBox").Text,
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
            
            finalRoot.FixDates(finalRoot.startDate, finalRoot.endDate);

            var jsonOutput = fbs.TestFormatString(SerializeJson(finalRoot));

            if (isEventBox)
            {
                finalRoot.boxReplacesID = finalRoot.boxId;
                finalRoot.boxId = finalRoot.boxId.Replace("VIP0", "VIP1");
                jsonOutput = jsonOutput.TrimEnd() + ','; //removing white space and adding comma
                finalRoot.lastChanceBoxPrizes = null;
                jsonOutput += '\n' + fbs.TestFormatString(SerializeJson(finalRoot));
            }

            jsonOutput = jsonOutput.TrimEnd() + ',';

            var complete = GetTestFilePath();

            File.WriteAllText(complete, jsonOutput);
            System.Diagnostics.Process.Start(complete);
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
