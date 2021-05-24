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

        private void button1_Click(object sender, EventArgs e)
        {
            testing.RemoveRuntimeComboBoxes(this);

            var eventID = boxIDcomboBox.SelectedItem.ToString(); //fix this
            eventID = eventID.Substring(eventID.IndexOf('_') + 1);

            if (eventID != null)
            {
                InitializeFormComponents(eventID);
            }
        }

        private void InitializeFormComponents(string eventID)
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
            orderCB.Text = eventObject.appearance.storeButtonAppearance.order.ToString();
            discountCB.Text = eventObject.appearance.storeButtonAppearance.discount.ToString();
            PSTitleLocKey.Text = eventObject.appearance.purchaseScreenAppearance.titleLocalizationKey;
            canShowCarouselBox.Checked = eventObject.appearance.mainHubAppearance.canShowInCarousel;
            style2CB.Text = eventObject.appearance.mainHubAppearance.style; //might not be populating Right
            TitleLocKeyCB.Text = eventObject.appearance.mainHubAppearance.titleLocalizationKey;
            mainhubSubLocKey.Text = eventObject.appearance.mainHubAppearance.subtitleLocalizationKey;

            TierGenerator();

            GenerateRuntimePopPanels();
        }

        private void GenerateRuntimePopPanels()
        {
            foreach (var x in eventObject.appearance.storeButtonAppearance.popIds)
                testing.GeneratePopSelector(x, StorePopsPanel);

            foreach (var x in eventObject.appearance.purchaseScreenAppearance.popIds)
                testing.GeneratePopSelector(x, PurchasePopsPanel);

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
                    tierBox = new TierBoxL(flowLayoutPanel1, databasePath, x);  

                if (x.isGuarantee == true && x.guarantee.LuckyPopPrize != null)
                    tierBox = new TierBoxM(flowLayoutPanel1, databasePath, x);

                if (x.isGuarantee != true)
                    tierBox = new TierBoxS(flowLayoutPanel1, databasePath, x);
            }
        }
        public IEnumerable<ComboBox> CallControlPanel()
        {
            return StorePopsPanel.Controls.OfType<ComboBox>();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            testing.GeneratePopSelector("", StorePopsPanel);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            testing.GeneratePopSelector("", PurchasePopsPanel);
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
           TierBoxL tierBox = new TierBoxL(flowLayoutPanel1, databasePath, new Tier());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TierBoxM tierBox = new TierBoxM(flowLayoutPanel1, databasePath, new Tier());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TierBoxS tierBox = new TierBoxS(flowLayoutPanel1, databasePath, new Tier());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PrizeBox newPrizeLine = new PrizeBox(lastChanceBoxPanel, databasePath, new Prize());
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
