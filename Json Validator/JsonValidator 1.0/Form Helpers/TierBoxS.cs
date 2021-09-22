using System;
using System.Windows.Forms;

namespace JsonValidator
{
    public class TierBoxS : ITierBox
    {
        TextBox textBoxOne;
        ComboBox comboB2;

        TierBoxS(FlowLayoutPanel flowPanel, string databasePath, Prize prize)
        {
            GeneratePrizeLine(flowPanel, databasePath, prize);
        }

        public void GeneratePrizeLine(FlowLayoutPanel flowPanel, string databasePath, Prize prize)
        {
            GroupBox newGroupB = new GroupBox()
            {
                Name = "TierGroupBox",
                Size = new System.Drawing.Size(369, 54),
                TabIndex = 56,
                TabStop = false,
                Margin = new Padding(0)
            };

            TextBox txtB1 = new TextBox()
            {
                Location = new System.Drawing.Point(9, 19),
                Size = new System.Drawing.Size(51, 20),
                TabIndex = 0
            };

            this.textBoxOne = txtB1;

            ComboBox combo2 = new ComboBox()
            {
                FormattingEnabled = true,
                Name = "amtBox",
                Size = new System.Drawing.Size(38, 21),
                TabIndex = 1
            };

            this.comboB2 = combo2;

            newGroupB.Controls.Add(textBoxOne);
            newGroupB.Controls.Add(comboB2);
            flowPanel.Controls.Add(newGroupB);
        }

    }
}
