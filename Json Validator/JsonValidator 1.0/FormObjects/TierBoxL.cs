using System.Collections.Generic;
using System.Windows.Forms;
using JsonValidator.CSV;
using JsonValidator.FormObjects;

namespace JsonValidator
{
    public class TierBoxL : CustomControls
    {
        public TierBoxL(FlowLayoutPanel flowPanel, Tier tier)
        {
            var newGroupB = base.GenerateControlsPanel(tier);
            var tierLGroup = GenerateLTierLine(newGroupB);
            AddToFlowPanel(flowPanel, tierLGroup);
            AssignComboBoxText(tier, tierLGroup);
        }
        private GroupBox GenerateLTierLine(GroupBox newGroupB)
        {
            _comboB3 = new ComboBox()
            {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(122, 19),
                Name = "popIdBox",
                Size = new System.Drawing.Size(86, 21),
                TabIndex = 2,
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems,
                //DataSource = popIDList
            };

            _comboB4 = new ComboBox()
            {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(214, 19),
                Name = "comboBox4",
                Size = new System.Drawing.Size(38, 21),
                TabIndex = 3,
                DataSource = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }

            };

            AddPopIDToCBDatabase(_comboB3);
            newGroupB.Controls.Add(_comboB3);
            newGroupB.Controls.Add(_comboB4);
            
            return newGroupB;
        }
        public override Control.ControlCollection AssignComboBoxText(Tier tier, GroupBox newGroup)
        {
            var controls = base.AssignComboBoxText(tier, newGroup);

            if (tier.guarantee != null)
            {
                controls[2].Text = tier.guarantee.SpecificPopId;
                controls[3].Text = tier.guarantee.Amount;
            }

            return controls;
        }

    }
}

