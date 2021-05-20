using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using JsonValidator.CSV;

namespace JsonValidator
{
    public partial class Form1 : Form
    {
        string databasePath = @"C:\Users\pdnud\OneDrive\Desktop\Json Validator\[1.6.0] Pop_Database - pop_database.csv";
        string playbookPath = @"C:\Users\pdnud\OneDrive\Desktop\Json Validator\Live Playbook.csv";

        List<string> boxIDs = new List<string>();
        Database database = new Database();
        NewRoot eventObject;
        ComboBox comboB;

        List<ComboBox> comboList = new List<ComboBox>();

        public Form1()
        {
            InitializeComponent();
            Converters converter = new Converters();
            boxIDs = converter.GetBoxIds(playbookPath);
            Debug.WriteLine("box ID count is :" + boxIDs.Count);
            boxIDcomboBox.DataSource = boxIDs;
            this.AutoScroll = true;
        }

        public List<string> ParsePopDictionary(Dictionary<string, string> popDict)
        {
            List<string> popIDs = new List<string>();
            foreach (var x in popDict)
            {
                if (x.Key != null)
                    popIDs.Add(x.Key);
            }
            return popIDs;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RemoveRuntimeComboBoxes();
            var eventID = boxIDcomboBox.SelectedItem.ToString(); //fix this
            eventID = eventID.Substring(eventID.IndexOf('_') + 1);

            if (eventID != null)
            {
                eventObject = Program.GetJsonObject(databasePath, playbookPath, eventID);
                textBox1.Text = eventObject.fandomId;

                dateTimePicker1.Value = DateTime.Parse(eventObject.startDate);
                dateTimePicker2.Value = DateTime.Parse(eventObject.endDate);
                checkBox1.Checked = eventObject.appearance.isEventBox;
                MysteryBoxTypeCB.Text = eventObject.appearance.mysteryBoxType;
                ThemeCB.Text = eventObject.appearance.theme;
                StyleCB.Text = eventObject.appearance.storeButtonAppearance.style;
                RibbonLocKeyCB.Text = eventObject.appearance.storeButtonAppearance.ribbonLocalizationKey;
                TitleLocKeyCB.Text = eventObject.appearance.storeButtonAppearance.titleLocalizationKey;
                SubLocKeyCB.Text = eventObject.appearance.storeButtonAppearance.subtitleLocalizationKey;

                foreach (var x in eventObject.appearance.storeButtonAppearance.popIds)
                    GeneratePopSelector(x, StorePopsPanel);

                orderCB.Text = eventObject.appearance.storeButtonAppearance.order.ToString();
                discountCB.Text = eventObject.appearance.storeButtonAppearance.discount.ToString();

                PSTitleLocKey.Text = eventObject.appearance.purchaseScreenAppearance.titleLocalizationKey;

                foreach (var x in eventObject.appearance.purchaseScreenAppearance.popIds)
                    GeneratePopSelector(x, PurchasePopsPanel);

                canShowCarouselBox.Checked = eventObject.appearance.mainHubAppearance.canShowInCarousel;
                style2CB.Text = eventObject.appearance.mainHubAppearance.style; //might not be populating Right
                TitleLocKeyCB.Text = eventObject.appearance.mainHubAppearance.titleLocalizationKey;
                mainhubSubLocKey.Text = eventObject.appearance.mainHubAppearance.subtitleLocalizationKey;

                foreach (var x in eventObject.appearance.mainHubAppearance.popIds)
                    GeneratePopSelector(x, mainHubPanel);

                behaviorCB.Text = eventObject.behaviourType;

                foreach (var x in eventObject.featuredPopIdsList)
                    GeneratePopSelector(x, featuredPopPanel);

                foreach (var x in eventObject.prizes)
                {
                    PrizeBox prizeBox = new PrizeBox(prizePanel, databasePath, x);
                    Debug.WriteLine("reward ID should be: " + x.rewardId);
                }
            }

        }

        public void PopulateTierBoxes(NewRoot eventObject, IList<ITierBox> boxes) //How to specify which tier box we wanna use?
        {
            foreach (var x in eventObject.tiers)
            {
                
            }


        }

        public void RemoveRuntimeComboBoxes()
        {
            foreach (Control item in StorePopsPanel.Controls.OfType<ComboBox>())
                StorePopsPanel.Controls.Clear();


            foreach (Control item in PurchasePopsPanel.Controls.OfType<ComboBox>())
                PurchasePopsPanel.Controls.Clear();

            foreach (Control item in mainHubPanel.Controls.OfType<ComboBox>())
                mainHubPanel.Controls.Clear();

            foreach (Control item in featuredPopPanel.Controls.OfType<ComboBox>())
                featuredPopPanel.Controls.Clear();
        }
        public void GeneratePopSelector(string popName, FlowLayoutPanel flowPanel)
        {
            comboB = new ComboBox();
            comboB.DataSource = database.GetAllPopID(databasePath);
            flowPanel.Controls.Add(comboB);

            if (popName != "")
                comboB.Text = popName;

        }

        public void ResetAllControls(Control form)
        {
            ComboBox ctrlInQ;
            foreach (Control control in form.Controls)
            {
                if (control is ComboBox)
                {
                    ctrlInQ = (ComboBox)control;
                    ctrlInQ.SelectedIndex = 0;
                }

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            GeneratePopSelector("", StorePopsPanel);


        }



        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void PSTitleLocKey_TextChanged(object sender, EventArgs e)
        {

        }

        private void MainHubLbl_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }


        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label19_Click_1(object sender, EventArgs e)
        {

        }

        private void prizePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void tierslbl_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }
    }

    public class PrizeBox
    {
        ComboBox comboB1;
        ComboBox comboB2;
        ComboBox comboB3;
        ComboBox comboB4;
        public PrizeBox(FlowLayoutPanel flowPanel, string databasePath, Prize prize)
        {
            GeneratePrizeLine(flowPanel, databasePath, prize);
        }

        public void GeneratePrizeLine(FlowLayoutPanel flowPanel, string databasePath, Prize prize)
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

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

    }

    public interface ITierBox
    {
        void GeneratePrizeLine(FlowLayoutPanel flowPanel, string databasePath, Prize prize);
    }

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

    public class TierBoxM : ITierBox
    {
        TextBox textBoxOne;
        ComboBox comboB2;
        ComboBox comboB3;

        TierBoxM(FlowLayoutPanel flowPanel, string databasePath, Prize prize)
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

            newGroupB.Controls.Add(textBoxOne);
            newGroupB.Controls.Add(comboB2);
            newGroupB.Controls.Add(comboB3);
            flowPanel.Controls.Add(newGroupB);
        }

    }
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
