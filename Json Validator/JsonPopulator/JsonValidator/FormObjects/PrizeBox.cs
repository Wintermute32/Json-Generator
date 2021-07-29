using System.Windows.Forms;
using System.Collections.Generic;
using JsonValidator.CSV;
//Last Chance and Prize panel use same format of form controls.
//The form population is started by the constructor
namespace JsonValidator
{
  public class PrizeBox
    {
        public ComboBox _comboB1, _comboB2, _comboB3, _comboB4;
        public TextBox TextBox;
        public PrizeBox() { }
        public PrizeBox(FlowLayoutPanel flowPanel, string databasePath, IPrizeBox prize)
        {
            GroupBox newGroupB = GeneratePrizePops(flowPanel, databasePath);
            AssignComboBoxText(prize, newGroupB);
        }
        private GroupBox GeneratePrizePops(FlowLayoutPanel flowPanel, string databasePath)
        {           
            GroupBox newGroupB = new GroupBox()
            {
                Name = "PrizeGroupBox",
                Size = new System.Drawing.Size(369, 54),
                TabIndex = 56,
                TabStop = false,
                Margin = new Padding(0)
            };

             _comboB1 = new ComboBox()
             {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(9, 19),
                Name = "comboBox1",
                Size = new System.Drawing.Size(70, 21),
                TabIndex = 1,
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems,
                DataSource = new List<string>() { "pop", "" },
             };

            _comboB2 = new ComboBox()
            {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(85, 19),
                Name = "comboBox2",
                Size = new System.Drawing.Size(148, 21),
                TabIndex = 5,
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems,
                DataSource = Database.GetAllPopID(databasePath)
            };

            _comboB3 = new ComboBox()
            {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(239, 19),
                Name = "comboBox3",
                Size = new System.Drawing.Size(52, 21),
                TabIndex = 3,
                DataSource = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }
            };

            _comboB4 = new ComboBox()
            {
                FormattingEnabled = true,
                Location = new System.Drawing.Point(297, 19),
                Name = "comboBox4",
                Size = new System.Drawing.Size(51, 21),
                TabIndex = 6,
                DataSource = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }
            };
            
            List<ComboBox> comboBList = new List<ComboBox>() { _comboB1, _comboB2, _comboB3, _comboB4 };
            
            foreach (var x in comboBList)
                newGroupB.Controls.Add(x);

            flowPanel.Controls.Add(newGroupB);

            return newGroupB;
        }
        private void AssignComboBoxText(IPrizeBox prize, GroupBox newGroup)
        {
            var controls = newGroup.Controls;
            controls[0].Text = prize.RewardType;
            controls[1].Text = prize.RewardID;
            controls[2].Text = prize.Amount.ToString();
            controls[3].Text = prize.Instances.ToString();
        }
    }
}
