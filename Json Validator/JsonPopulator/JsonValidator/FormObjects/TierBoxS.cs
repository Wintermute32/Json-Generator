using System;
using System.Windows.Forms;

namespace JsonValidator
{
    public class TierBoxS : ITierBox
    {
        TextBox textBoxOne;
        ComboBox comboB2;

        public TierBoxS(FlowLayoutPanel flowPanel, string databasePath, Tier tier)
        {
            GeneratePrizeLine(flowPanel, databasePath, tier);
        }

        public void GeneratePrizeLine(FlowLayoutPanel flowPanel, string databasePath, Tier tier)
        {
            GroupBox newGroupB = new GroupBox()
            {
                Name = "TierGroupBox",
                Location = new System.Drawing.Point(122, 1015),
                Size = new System.Drawing.Size(257, 46),
                TabIndex = 57,
                TabStop = false,
                //Margin = new Padding(0)
            };

            TextBox txtB1 = new TextBox()
            {
                Location = new System.Drawing.Point(7, 20),
                Size = new System.Drawing.Size(51, 20),
                TabIndex = 0
            };

            this.textBoxOne = txtB1;

            ComboBox combo2 = new ComboBox()
            {
                FormattingEnabled = true,
                Name = "amtBox",
                Location = new System.Drawing.Point(64, 19),
                Size = new System.Drawing.Size(38, 21),
                TabIndex = 1
            };

            this.comboB2 = combo2;

            newGroupB.Controls.Add(textBoxOne);
            newGroupB.Controls.Add(comboB2);
            flowPanel.Controls.Add(newGroupB);

            textBoxOne.Text = tier.cost.ToString(); //need to fix in Json Classes Tier
            comboB2.Text = tier.numPulls.ToString();
        }

    }
}
