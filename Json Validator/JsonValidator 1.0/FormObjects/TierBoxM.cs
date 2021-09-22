using System.Collections.Generic;
using System.Windows.Forms;
using JsonValidator.FormObjects;

namespace JsonValidator
{
    public class TierBoxM : CustomControls
    {
        public TierBoxM(FlowLayoutPanel flowPanel, Tier tier)
        {
            var newGroupB = GenerateControlsPanel(tier);
            AddToFlowPanel(flowPanel, newGroupB);
            AssignComboBoxText(tier, newGroupB);
        }
        public override GroupBox GenerateControlsPanel(Tier tier)
        {
            var newGroupB = base.GenerateControlsPanel(tier);
           
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
            
            newGroupB.Controls.Add(_comboB3);   
            return newGroupB;
        }
        public override Control.ControlCollection AssignComboBoxText(Tier tier, GroupBox newGroup)
        {
            var controls =  base.AssignComboBoxText(tier, newGroup);
            if (tier.guarantee != null)
                controls[2].Text = tier.guarantee.LuckyPopPrize.ToString();

            return controls;
        }
    }
}
