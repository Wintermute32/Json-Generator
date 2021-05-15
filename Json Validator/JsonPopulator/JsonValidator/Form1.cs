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

        List<ComboBox> comboBList = new List<ComboBox>();
        List<string> boxIDs = new List<string>();

        Database database = new Database();
        
        NewRoot eventObject;

        Label rewardType = new Label();
        Label rewardID = new Label();
        Label amount = new Label();
        Label instances = new Label();

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
                    GeneratePrizeRow(x, prizePanel);

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

        public void GeneratePrizeRow(Prize prizeObj, FlowLayoutPanel flowPanel)
        {
            Label rewardType = new Label();
            rewardType.Text = "rewardType";
            rewardType.Margin = new Padding(0);
            rewardType.Size = new Size(30, 20);

            TextBox rewards = new TextBox();
            rewards.Text = prizeObj.rewardType;
            rewards.Margin = new Padding(0);
            rewards.Size = new Size(30, 20);


            Label rewardID = new Label();
            rewardID.Text = "rewardID";
            rewardID.Margin = new Padding(0);
            rewards.Size = new Size(30, 20);

            ComboBox ID = new ComboBox();
            ID.DataSource = database.GetAllPopID(databasePath);
            ID.Text = prizeObj.rewardId;
            ID.Margin = new Padding(0);
            ID.Size = new Size(30, 20);


            Label amount = new Label();
            amount.Text = "amount";
            amount.Margin = new Padding(0);
            amount.Size = new Size(30, 70);

            TextBox popAmount = new TextBox();
            popAmount.Text = prizeObj.amount.ToString();
            popAmount.Margin = new Padding(0);

            Label instances = new Label();
            instances.Text = "instances";

            TextBox instanceCount = new TextBox();
            instanceCount.Text = prizeObj.instances.ToString();
            
            flowPanel.Controls.Add(rewardType);
            flowPanel.Controls.Add(rewards);
            flowPanel.Controls.Add(rewardID);
            flowPanel.Controls.Add(ID);
            flowPanel.Controls.Add(amount);
            flowPanel.Controls.Add(popAmount);
            flowPanel.Controls.Add(instances);
            flowPanel.Controls.Add(instanceCount);

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
    }
}
