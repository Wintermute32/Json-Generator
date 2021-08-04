using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace JsonValidator
{
    public class Prize : IPrizeBox
    {
        public string rewardType { get; set; }
        public string rewardId { get; set; }
        public int amount { get; set; }
        public int instances { get; set; }
        public static List<Prize> GeneratePrizeList(List<FlowLayoutPanel> flowList)
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