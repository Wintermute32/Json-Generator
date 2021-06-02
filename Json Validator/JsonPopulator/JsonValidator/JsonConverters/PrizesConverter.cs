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
    public class PrizesConverter
    {
        public List<Prize> GeneratePrizeList(List<FlowLayoutPanel> flowList)
        {
            var popLists = flowList.Find(x => x.Name == "prizePanel").Controls.OfType<GroupBox>().ToList();
            List<Prize> prizePopList = new List<Prize>();

            foreach (var x in popLists)
            {
                Prize prize = new Prize();
                var comboB = x.Controls.OfType<ComboBox>().ToList();
                prize.rewardType = comboB[0].Text;
                prize.rewardId = comboB[1].Text;
                prize.amount = Convert.ToInt32(comboB[2].Text);
                prize.instances = Convert.ToInt32(comboB[3].Text);
                prizePopList.Add(prize);
            }

            return prizePopList;
        }
    }
}