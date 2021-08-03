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
        public NewRoot GenerateNewRoot(Form1 form)
        {
            //AppearanceConverter appearance = new AppearanceConverter(form);
            //PrizesConverter prizesConverter = new PrizesConverter();
            //TierConverter tierConverter = new TierConverter();

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
                Appearance = Appearance.GenerateAppearance(form),
                BehaviorType = comboBoxes.Find(x => x.Name == "behaviorCB").Text,
                FeaturedPopIdList = GetFeaturedPops(flowBoxes),
                Prizes = Prize.GeneratePrizeList(flowBoxes),
                Tiers = Tier.GenerateTierListConverter(flowBoxes),
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
                {"isOtherBox", checkBoxes.Find(x => x.Name == "OtherBoxCheck").Checked}
            };

            return boxTypeDict;
        }
        public bool UpdateBoxType(NewRoot finalRoot)
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
        private List<LastChanceBoxPrize> GetLastChanceList(List<FlowLayoutPanel> flowlist)
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
        private List<string> GetFeaturedPops(List<FlowLayoutPanel> flowList)
        {
            var popLists = flowList.Find(x => x.Name == "storePopsPanel").Controls.OfType<ComboBox>().ToList();
            List<string> _popIds = new List<string>();

            foreach (var x in popLists)
            {
                _popIds.Add(x.Text);
            }
            return _popIds;
        }     
        private string AmmendBoxID(List<ComboBox> comboBoxes, List<TextBox> textBoxes, Dictionary<string, bool> boxTypeDict)
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
        public static List<LastChanceBoxPrize> AssignBoxValues(Dictionary<string, string> popDict)
        {
            List<LastChanceBoxPrize> lcbpList = new List<LastChanceBoxPrize>();

            foreach (var x in popDict)
            {
                LastChanceBoxPrize lastChanceP = new LastChanceBoxPrize();
                lastChanceP.RewardID = x.Key;
                lcbpList.Add(lastChanceP);
            }

            lcbpList.Reverse();

            for (int i = 0; i < lcbpList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        lcbpList[i].Amount = 1; lcbpList[i].Instances = 3; break;
                    case 1:
                        lcbpList[i].Amount = 2; lcbpList[i].Instances = 2; break;
                    case 2:
                        lcbpList[i].Amount = 3; lcbpList[i].Instances = 2; break;
                    case 3:
                        lcbpList[i].Amount = 6; lcbpList[i].Instances = 1; break;

                    case 4:
                        lcbpList[i].Amount = 6; lcbpList[i].Instances = 1; break;
                }

            }
            return lcbpList;
        }
    }
}
