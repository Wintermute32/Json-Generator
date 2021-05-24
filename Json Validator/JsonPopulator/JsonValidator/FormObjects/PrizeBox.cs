using System;
using System.Collections.Generic;
using System.Windows.Forms;
using JsonValidator.CSV;

namespace JsonValidator
{
    public class PrizeBox
    {
        ComboBox comboB1;
        ComboBox comboB2;
        ComboBox comboB3;
        ComboBox comboB4;
        public PrizeBox(FlowLayoutPanel flowPanel, string databasePath, IPrizeBox prize)
        {
            GeneratePrizeLine(flowPanel, databasePath, prize);
        }

        public void GeneratePrizeLine(FlowLayoutPanel flowPanel, string databasePath, IPrizeBox prize)
        {
            Database database = new Database();

            GroupBox newGroupB = new GroupBox()
            {
                Name = "PrizeGroupBox",
                Size = new System.Drawing.Size(369, 54),
                TabIndex = 56,
                TabStop = false,
                Margin = new Padding(0)
            };

            ComboBox combo1 = new ComboBox()
            {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(9, 19),
                Name = "comboBox1",
                Size = new System.Drawing.Size(70, 21),
                TabIndex = 1,
            };

            this.comboB1 = combo1;

            ComboBox combo2 = new ComboBox()
            {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(85, 19),
                Name = "comboBox2",
                Size = new System.Drawing.Size(148, 21),
                TabIndex = 5,
            };

            this.comboB2 = combo2;


            ComboBox combo3 = new ComboBox()
            {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(239, 19),
                Name = "comboBox3",
                Size = new System.Drawing.Size(52, 21),
                TabIndex = 3,
            };

            this.comboB3 = combo3;

            ComboBox combo4 = new ComboBox()
            {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(297, 19),
                Name = "comboBox4",
                Size = new System.Drawing.Size(51, 21),
                TabIndex = 6,
            };

            this.comboB4 = combo4;

            combo1.DataSource = new List<string>() { "pop", "" };
            combo2.DataSource = database.GetAllPopID(databasePath);
            combo3.DataSource = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            combo4.DataSource = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

            newGroupB.Controls.Add(comboB1);
            newGroupB.Controls.Add(comboB2);
            newGroupB.Controls.Add(comboB3);
            newGroupB.Controls.Add(comboB4);
            flowPanel.Controls.Add(newGroupB);

            comboB1.Text = prize.rewardType;
            comboB2.Text = prize.rewardId;
            comboB3.Text = prize.amount.ToString();
            comboB4.Text = prize.instances.ToString();
        }

    }
}
