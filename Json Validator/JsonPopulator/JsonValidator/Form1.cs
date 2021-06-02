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
        NewRoot eventObject;
        Testing testing;

        public Form1()
        {
            InitializeComponent();
            testing = new Testing(databasePath, playbookPath);
            Converters converter = new Converters();
            boxIDs = converter.GetBoxIds(playbookPath);
            Debug.WriteLine("box ID count is :" + boxIDs.Count);
            boxIdCB.DataSource = boxIDs;
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

        private void button1_Click(object sender, EventArgs e)
        {
            testing.RemoveRuntimeComboBoxes(this);

            var eventID = boxIdCB.SelectedItem.ToString(); //fix this
            eventID = eventID.Substring(eventID.IndexOf('_') + 1);

            if (eventID != null)
            {
                InitializeFormComponents(eventID);
            }
        }

        private void InitializeFormComponents(string eventID)
        {


            eventObject = Program.GetJsonObject(databasePath, playbookPath, eventID);
            fandomIdCB.Text = eventObject.fandomId;
            startDatePicker.Value = DateTime.Parse(eventObject.startDate);
            endDatePicker.Value = DateTime.Parse(eventObject.endDate);
            isEventCheck.Checked = eventObject.appearance.isEventBox;
            MysteryBoxCB.Text = eventObject.appearance.mysteryBoxType;
            themeCB.Text = eventObject.appearance.theme;
            styleCB.Text = eventObject.appearance.storeButtonAppearance.style;
            ribbonLocKeyCB.Text = eventObject.appearance.storeButtonAppearance.ribbonLocalizationKey;
            titleLocKeyCB.Text = eventObject.appearance.storeButtonAppearance.titleLocalizationKey;
            subLocKeyCB.Text = eventObject.appearance.storeButtonAppearance.subtitleLocalizationKey;
            orderCB.Text = eventObject.appearance.storeButtonAppearance.order.ToString();
            discountCB.Text = eventObject.appearance.storeButtonAppearance.discount.ToString();
            purTitleLocKey.Text = eventObject.appearance.purchaseScreenAppearance.titleLocalizationKey;
            canShowCarouselBox.Checked = eventObject.appearance.mainHubAppearance.canShowInCarousel;
            style2CB.Text = eventObject.appearance.mainHubAppearance.style; //might not be populating Right
            titleLocKeyCB.Text = eventObject.appearance.mainHubAppearance.titleLocalizationKey;
            mainhubSubLocKey.Text = eventObject.appearance.mainHubAppearance.subtitleLocalizationKey;

            TierGenerator();

            GenerateRuntimePopPanels();
        }

        private void GenerateRuntimePopPanels()
        {
            foreach (var x in eventObject.appearance.storeButtonAppearance.popIds)
                testing.GeneratePopSelector(x, storePopsPanel);

            foreach (var x in eventObject.appearance.purchaseScreenAppearance.popIds)
                testing.GeneratePopSelector(x, purchasePopsPanel);

            foreach (var x in eventObject.appearance.mainHubAppearance.popIds)
                testing.GeneratePopSelector(x, mainHubPanel);

            foreach (var x in eventObject.featuredPopIdsList)
                testing.GeneratePopSelector(x, featuredPopPanel);

            foreach (var x in eventObject.prizes)
            {
                PrizeBox prizeBox = new PrizeBox(prizePanel, databasePath, x);
            }

            foreach (var x in eventObject.lastChanceBoxPrizes)
            {
                PrizeBox lastChanceBox = new PrizeBox(lastChanceBoxPanel, databasePath, x);
            }
        }

        public void TierGenerator()
        {
            ITierBox tierBox;

            foreach (var x in eventObject.tiers)
            {
                if (x.isGuarantee == true && x.guarantee.LuckyPopPrize == null)
                    tierBox = new TierBoxL(tierPanel, databasePath, x);

                if (x.isGuarantee == true && x.guarantee.LuckyPopPrize != null)
                    tierBox = new TierBoxM(tierPanel, databasePath, x);

                if (x.isGuarantee != true)
                    tierBox = new TierBoxS(tierPanel, databasePath, x);
            }
        }

        public Form1 GetFormControlList()
        {
            Form1 form1 = new Form1();
            form1.boxIdCB.Name = this.boxIdCB.Name;
            return form1;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            testing.GeneratePopSelector("", storePopsPanel);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            testing.GeneratePopSelector("", purchasePopsPanel);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            testing.GeneratePopSelector("", mainHubPanel);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            testing.GeneratePopSelector("", featuredPopPanel);

        }
        private void button5_Click(object sender, EventArgs e)
        {
            PrizeBox newPrizeLine = new PrizeBox(prizePanel, databasePath, new Prize());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TierBoxL tierBox = new TierBoxL(tierPanel, databasePath, new Tier());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TierBoxM tierBox = new TierBoxM(tierPanel, databasePath, new Tier());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TierBoxS tierBox = new TierBoxS(tierPanel, databasePath, new Tier());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PrizeBox newPrizeLine = new PrizeBox(lastChanceBoxPanel, databasePath, new Prize());
        }

        private void storeSubB_Click(object sender, EventArgs e)
        {
            testing.RemoveCustomControls(storePopsPanel);
        }

        private void purSubB_Click(object sender, EventArgs e)
        {
            testing.RemoveCustomControls(purchasePopsPanel);
        }

        private void mainSubB_Click(object sender, EventArgs e)
        {
            testing.RemoveCustomControls(mainHubPanel);
        }

        private void featuredSubB_Click(object sender, EventArgs e)
        {
            testing.RemoveCustomControls(featuredPopPanel);
        }

        private void prizeSubB_Click(object sender, EventArgs e)
        {
            testing.RemoveCustomControls(prizePanel);
        }

        private void tierSub1_Click(object sender, EventArgs e)
        {
            testing.RemoveCustomControls(tierPanel);
        }

        private void lastCSubB_Click(object sender, EventArgs e)
        {
            testing.RemoveCustomControls(lastChanceBoxPanel);
        }

        private void genJsonBtn_Click(object sender, EventArgs e)
        {
            JsonGeneration jGen = new JsonGeneration();
            jGen.GenerateMyJson(this);
        }

        private void boxIDcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tierPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    public class Testing
    {
        Database database = new Database();
        ComboBox comboB;
        string dataBPath;
        string playBPath;

        List<ComboBox> comboList = new List<ComboBox>();

        public Testing(string databasePath, string playbookPath)
        {
            this.dataBPath = databasePath;
            this.playBPath = playbookPath;
        }

        public void RemoveRuntimeComboBoxes(Form1 form1)
        {
            foreach (Control item in form1.Controls.OfType<FlowLayoutPanel>())
                item.Controls.Clear();
        }

        public void GeneratePopSelector(string popName, FlowLayoutPanel flowPanel)
        {
            comboB = new ComboBox();
            comboB.DataSource = database.GetAllPopID(dataBPath);
            flowPanel.Controls.Add(comboB);

            if (popName != "")
                comboB.Text = popName;

        }

        public void RemoveCustomControls(FlowLayoutPanel panel)
        {
            var controlList = panel.Controls.OfType<Control>().ToList();
            if (controlList.Count > 0)
                panel.Controls.Remove(controlList[controlList.Count - 1]);
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
    }
} 
