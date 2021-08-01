using System.Collections.Generic;
using System.Windows.Forms;

namespace JsonValidator
{
    public class TierBoxM : PrizeBox
    {
        public TierBoxM(FlowLayoutPanel flowPanel, Tier tier)
        {
            var newGroupB = GeneratePrizeLine(flowPanel);
            AssignComboBoxText(tier, newGroupB);
        }
        public GroupBox GeneratePrizeLine(FlowLayoutPanel flowPanel)
        {
            GroupBox newGroupB = new GroupBox()
            {
                Name = "TierGroupBox",
                Location = new System.Drawing.Point(122, 1015),
                Size = new System.Drawing.Size(257, 46),
                TabIndex = 57,
                TabStop = false,
            };

             TextBox = new TextBox()
            {
                Location = new System.Drawing.Point(7, 20),
                Size = new System.Drawing.Size(51, 20),
                TabIndex = 0
            };

             _comboB2 = new ComboBox()
            {
                FormattingEnabled = true,
                Name = "amtBox",
                Location = new System.Drawing.Point(64, 19),
                Size = new System.Drawing.Size(38, 21),
                TabIndex = 1
            };

             _comboB3 = new ComboBox()
            {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(122, 19),
                Name = "popIdBox",
                Size = new System.Drawing.Size(86, 21),
                TabIndex = 2,
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems,
                DataSource = new List<bool>() { true, false }
            };

            List<Control> controlsList = new List<Control>() { TextBox, _comboB2, _comboB3};

            foreach (var x in controlsList)
                newGroupB.Controls.Add(x);

            flowPanel.Controls.Add(newGroupB);

            return newGroupB;
        }
        private void AssignComboBoxText(Tier tier, GroupBox newGroup)
        {
            if (tier.Guarantee != null)
            {
                var controls = newGroup.Controls;
                controls[0].Text = tier.Cost.ToString();
                controls[1].Text = tier.NumPulls.ToString();
                controls[2].Text = tier.Guarantee.LuckyPopPrize.ToString();
            }
        }
    }
}
