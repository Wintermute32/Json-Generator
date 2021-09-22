using System;
using System.Windows.Forms;

namespace JsonValidator
{
    public class TierBoxL : ITierBox
    {
        TextBox textBoxOne;
        ComboBox comboB2;
        ComboBox comboB3;
        ComboBox comboB4;

        TierBoxL(FlowLayoutPanel flowPanel, string databasePath, Prize prize)
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


            ComboBox combo3 = new ComboBox()
            {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(239, 19),
                Name = "popIdBox",
                Size = new System.Drawing.Size(86, 21),
                TabIndex = 2,
            };

            this.comboB3 = combo3;

            ComboBox combo4 = new ComboBox()
            {
                FormattingEnabled = true,
                Name = "comboBox4",
                Size = new System.Drawing.Size(38, 21),
                TabIndex = 3,
           };

            this.comboB4 = combo4;

            newGroupB.Controls.Add(textBoxOne);
            newGroupB.Controls.Add(comboB2);
            newGroupB.Controls.Add(comboB3);
            newGroupB.Controls.Add(comboB4);
            flowPanel.Controls.Add(newGroupB);
        }

    }
}
