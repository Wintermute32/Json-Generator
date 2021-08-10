using System.Collections.Generic;
using System.Windows.Forms;
using JsonValidator.CSV;

namespace JsonValidator
{
    public class TierBoxL : PrizeBox
    {
        public TierBoxL(FlowLayoutPanel flowPanel, string databasePath, Tier tier)
        {
            var newGroupB = GeneratePrizeLine(flowPanel, databasePath);
            AssignComboBoxText(tier, newGroupB);
        }
        public GroupBox GeneratePrizeLine(FlowLayoutPanel flowPanel, string databasePath)
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
                Size = new System.Drawing.Size(38, 21),
                Location = new System.Drawing.Point(64, 19),
                TabIndex = 1,
                DataSource = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }
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
                DataSource = Database.GetAllPopID(databasePath)
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

            List<Control> controlsList = new List<Control>() { TextBox, _comboB2, _comboB3, _comboB4 };

            foreach (var x in controlsList)
                newGroupB.Controls.Add(x);

            flowPanel.Controls.Add(newGroupB);

            return newGroupB;
        }   
        private void AssignComboBoxText(Tier tier, GroupBox newGroup)
        {
            if (tier.guarantee != null) //ensuring we dont crash when using + UI button
            {
                var controls = newGroup.Controls;
                controls[0].Text = tier.cost.ToString();
                controls[1].Text = tier.numPulls.ToString();
                controls[2].Text = tier.guarantee.SpecificPopId;
                controls[3].Text = tier.guarantee.Amount;
            }
        }
        
    }
}
