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
            testing.RemoveRuntimeComboBoxes(StorePopsPanel, mainHubPanel, PurchasePopsPanel, featuredPopPanel);

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
                    testing.GeneratePopSelector(x, StorePopsPanel);

                orderCB.Text = eventObject.appearance.storeButtonAppearance.order.ToString();
                discountCB.Text = eventObject.appearance.storeButtonAppearance.discount.ToString();

                PSTitleLocKey.Text = eventObject.appearance.purchaseScreenAppearance.titleLocalizationKey;

                foreach (var x in eventObject.appearance.purchaseScreenAppearance.popIds)
                    testing.GeneratePopSelector(x, PurchasePopsPanel);

                canShowCarouselBox.Checked = eventObject.appearance.mainHubAppearance.canShowInCarousel;
                style2CB.Text = eventObject.appearance.mainHubAppearance.style; //might not be populating Right
                TitleLocKeyCB.Text = eventObject.appearance.mainHubAppearance.titleLocalizationKey;
                mainhubSubLocKey.Text = eventObject.appearance.mainHubAppearance.subtitleLocalizationKey;

                foreach (var x in eventObject.appearance.mainHubAppearance.popIds)
                    testing.GeneratePopSelector(x, mainHubPanel);

                behaviorCB.Text = eventObject.behaviourType;

                foreach (var x in eventObject.featuredPopIdsList)
                    testing.GeneratePopSelector(x, featuredPopPanel);

                foreach (var x in eventObject.prizes)
                {
                    PrizeBox prizeBox = new PrizeBox(prizePanel, databasePath, x);
                    Debug.WriteLine("reward ID should be: " + x.rewardId);
                }

                TierGenerator();
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
          
        public void RemoveRuntimeComboBoxes(FlowLayoutPanel storePopsPanel, FlowLayoutPanel purchasePopsPanel, FlowLayoutPanel mainHubPanel, FlowLayoutPanel featuredPopsPanel)
        {
            foreach (Control item in storePopsPanel.Controls.OfType<ComboBox>())
                storePopsPanel.Controls.Clear();

            foreach (Control item in purchasePopsPanel.Controls.OfType<ComboBox>())
                purchasePopsPanel.Controls.Clear();

            foreach (Control item in mainHubPanel.Controls.OfType<ComboBox>())
                mainHubPanel.Controls.Clear();

            foreach (Control item in featuredPopsPanel.Controls.OfType<ComboBox>())
                featuredPopsPanel.Controls.Clear();
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
