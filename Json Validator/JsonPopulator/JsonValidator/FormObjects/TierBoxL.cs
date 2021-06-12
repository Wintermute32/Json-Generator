using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using JsonValidator.CSV;


namespace JsonValidator
{

    //You might not need 3 tier classes sicne Newtonsoft Json will automatically exclude null values
    public class TierBoxL : ITierBox
    {
        TextBox textBoxOne;
        ComboBox comboB2;
        ComboBox comboB3;
        ComboBox comboB4;

        public TierBoxL(FlowLayoutPanel flowPanel, string databasePath, Tier tier)
        {
            GeneratePrizeLine(flowPanel, databasePath, tier);
            Debug.WriteLine("Generated LTier cost is " + tier.cost);
        }

        public void GeneratePrizeLine(FlowLayoutPanel flowPanel, string databasePath, Tier tier)
        {
            Database database = new Database();

            GroupBox newGroupB = new GroupBox()
            {
                Name = "TierGroupBox",
                Location = new System.Drawing.Point(122, 1015),
                Size = new System.Drawing.Size(257, 46),
                TabIndex = 57,
                TabStop = false,
            };

            TextBox txtB1 = new TextBox()
            {
                Location = new System.Drawing.Point(7, 20),
                Size = new System.Drawing.Size(51, 20),
                TabIndex = 0
            };

            ComboBox combo2 = new ComboBox()
            {
                FormattingEnabled = true,
                Name = "amtBox",
                Size = new System.Drawing.Size(38, 21),
                Location = new System.Drawing.Point(64, 19),
                TabIndex = 1
            };


            ComboBox combo3 = new ComboBox()
            {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(122, 19),
                Name = "popIdBox",
                Size = new System.Drawing.Size(86, 21),
                TabIndex = 2,
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems,
            };

            ComboBox combo4 = new ComboBox()
            {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(214, 19),
                Name = "comboBox4",
                Size = new System.Drawing.Size(38, 21),
                TabIndex = 3,
           };

            combo2.DataSource = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            combo3.DataSource = database.GetAllPopID(databasePath);
            combo4.DataSource = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

            this.textBoxOne = txtB1;
            this.comboB2 = combo2;
            this.comboB3 = combo3;
            this.comboB4 = combo4;

            newGroupB.Controls.Add(textBoxOne);
            newGroupB.Controls.Add(comboB2);
            newGroupB.Controls.Add(comboB3);
            newGroupB.Controls.Add(comboB4);
            flowPanel.Controls.Add(newGroupB);

            if (tier.guarantee != null) //ensuring we dont crash when using + UI button
            {
                textBoxOne.Text = tier.cost.ToString(); //need to fix in Json Classes Tier
                comboB2.Text = tier.numPulls.ToString();
                comboB3.Text = tier.guarantee.SpecificPopId;
                comboB4.Text = tier.guarantee.specificPopAmount;
            }
        }
    }

}
