using System.Linq;
using System.Windows.Forms;
using System;
using System.Collections.Generic;

namespace JsonValidator
{
    public class NewRootGeneration
    {
        //private FormatBoxString fbs = new FormatBoxString();    
        public NewRoot GenerateNewRoot(Form1 form)
        {
            var comboBoxes = form.Controls.OfType<ComboBox>().ToList();
            var flowBoxes = form.Controls.OfType<FlowLayoutPanel>().ToList();
            var dateTimePicker = form.Controls.OfType<DateTimePicker>().ToList();
            var checkBoxes = form.Controls.OfType<CheckBox>().ToList();
            var textBoxes = form.Controls.OfType<TextBox>().ToList();
            var boxTypeDict = GetBoxTypeDict(form);

            NewRoot finalRoot = new NewRoot()
            {
                boxId = AmmendBoxID(comboBoxes, textBoxes, boxTypeDict),
                EventNumber = textBoxes.Find(x => x.Name == "eventNumBox").Text,
                fandomId = textBoxes.Find(x => x.Name == "fandomIdCB").Text,
                startDate = dateTimePicker.Find(x => x.Name == "startDatePicker").Text,
                endDate = dateTimePicker.Find(x => x.Name == "endDatePicker").Text,
                appearance = Appearance.GenerateAppearance(form),
                behaviourType = comboBoxes.Find(x => x.Name == "behaviorCB").Text,
                featuredPopIds = GetFeaturedPops(flowBoxes),
                prizes = Prize.GeneratePrizeList(flowBoxes),
                tiers = Tier.GenerateTierListConverter(flowBoxes),
                lastChanceBoxPrizes = GetLastChanceList(flowBoxes),
            };

            finalRoot.FixDates(finalRoot);

            return finalRoot;
        }
        private Dictionary<string, bool> GetBoxTypeDict(Form form)
        {
            var checkBoxes = form.Controls.OfType<CheckBox>().ToList();
            
            Dictionary<string, bool> boxTypeDict = new Dictionary<string, bool>()
            {
                {"isEventBox", checkBoxes.Find(x => x.Name == "isEventCheck").Checked},
                {"isOEDBox", checkBoxes.Find(x => x.Name == "oedBoxCheck").Checked},
                {"isOtherBox", checkBoxes.Find(x => x.Name == "OtherBoxCheck").Checked},
                {"isVIPBox", checkBoxes.Find(x => x.Name == "isVipBox").Checked}
            };
            return boxTypeDict;
        }
        public void UpdateBoxType(NewRoot finalRoot)
        {
            if (finalRoot.appearance.isEventBox)
            {
                finalRoot.appearance.isEventBox = true;
            }
           
            if (finalRoot.appearance.IsVIPBox)
            {
                finalRoot.boxVipReplaces = finalRoot.boxId.Replace("VIP1", "VIP0");
                finalRoot.lastChanceBoxPrizes = null;;
            }

            if (finalRoot.appearance.IsOtherBox)
            {
                finalRoot.appearance.isEventBox = false;
                finalRoot.lastChanceBoxPrizes = null;
            }

            if (finalRoot.appearance.IsOEDBox)
            {
                finalRoot.appearance.isEventBox = true;
                finalRoot.lastChanceBoxPrizes = null;
            }
        }

        private List<LastChanceBoxPrize> GetLastChanceList(List<FlowLayoutPanel> flowlist)
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

            //if (boxTypeDict["isVIPBox"])
                //boxId = "e" + formBoxNum + "_bxtFE_VIP1_" + formBoxId.Substring(formBoxId.IndexOf('_') + 1);

            if (boxTypeDict["isOEDBox"])
                 boxId = "e" + formBoxNum + "_bxtOED_VIP0_" + formBoxId.Substring(formBoxId.IndexOf('_') + 1);

            if (boxTypeDict["isOtherBox"])
                boxId = "e" + formBoxNum + "_bxtOther_VIP0_" + formBoxId.Substring(formBoxId.IndexOf('_') + 1);

            if (boxId.Contains("mard"))
                boxId = boxId.Replace("_mardFE", "");

            if (boxId.Contains("marc"))
                boxId = boxId.Replace("_marcFE", "");

            if (boxTypeDict["isVIPBox"])
                boxId = boxId.Replace("VIP0", "VIP1");

            return boxId;
        }
        public static List<LastChanceBoxPrize> AssignBoxValues(Dictionary<string, string> popDict)
        {
            List<LastChanceBoxPrize> lcbpList = new List<LastChanceBoxPrize>();

            foreach (var x in popDict)
            {
                LastChanceBoxPrize lastChanceP = new LastChanceBoxPrize();
                lastChanceP.rewardId = x.Key;
                lcbpList.Add(lastChanceP);
            }

            lcbpList.Reverse();

            for (int i = 0; i < lcbpList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        lcbpList[i].amount = 1; lcbpList[i].instances = 3; break;
                    case 1:
                        lcbpList[i].amount = 2; lcbpList[i].instances = 2; break;
                    case 2:
                        lcbpList[i].amount = 3; lcbpList[i].instances = 2; break;
                    case 3:
                    case 4:
                        lcbpList[i].amount = 6; lcbpList[i].instances = 1; break;
                }

            }
            return lcbpList;
        }
    }
}
