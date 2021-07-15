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
using JsonValidator.StoreConfigUpdate;

namespace JsonValidator
{
    public partial class Form1 : Form
    {
        string databasePath;
        string playbookPath;
        string gachaPath;
        string directoryPath;

        List<string> boxIDs = new List<string>();
        NewRoot eventObject;
        FormControls formControls;
        StoreConfig sCU = new StoreConfig();

        public Form1()
        {
            InitializeComponent();
            this.AutoScroll = true;
        }

        private void InitializeFormComponents(string eventID)
        {
            //takes completed NewRoot Object and populates forum UI values
            eventObject = Program.GetJsonObject(databasePath, playbookPath, gachaPath, eventID);
            eventNumBox.Text = eventObject.evetnNumber;
            fandomIdCB.Text = eventObject.fandomId;
            startDatePicker.Value = DateTime.Parse(eventObject.startDate);
            endDatePicker.Value = DateTime.Parse(eventObject.endDate);
            isEventCheck.Checked = eventObject.appearance.isEventBox;
            MysteryBoxCB.Text = eventObject.appearance.mysteryBoxType;
            themeCB.Text = eventObject.appearance.theme;
            styleCB.Text = eventObject.appearance.storeButtonAppearance.style;
            ribbonLocKeyCB.Text = eventObject.appearance.storeButtonAppearance.ribbonLocalizationKey;
            titleLocCB.Text = eventObject.appearance.storeButtonAppearance.titleLocalizationKey;
            subLocCB.Text = eventObject.appearance.storeButtonAppearance.subtitleLocalizationKey;
            orderCB.Text = eventObject.appearance.storeButtonAppearance.order.ToString();
            discountCB.Text = eventObject.appearance.storeButtonAppearance.discount.ToString();
            purTitleLocKey.Text = eventObject.appearance.purchaseScreenAppearance.titleLocalizationKey;
            canShowCarouselBox.Checked = eventObject.appearance.mainHubAppearance.canShowInCarousel;
            style2CB.Text = eventObject.appearance.mainHubAppearance.style; //might not be populating Right
            titleLocKeyCB.Text = eventObject.appearance.mainHubAppearance.titleLocalizationKey;
            mainhubSubLocKey.Text = eventObject.appearance.mainHubAppearance.subtitleLocalizationKey;

            formControls.GenerateRuntimePopPanels(eventObject, databasePath);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //program halts instead of crashes if custom ID is used
            if (boxIdCB.SelectedItem != null)
            {
                formControls.RemoveRuntimeComboBoxes(this);

                var eventID = boxIdCB.SelectedItem.ToString(); //fix this
                eventID = eventID.Substring(eventID.IndexOf('_') + 1);

                Debug.WriteLine("Event ID for this before initialize is " + eventID);

                if (eventID != null)
                {
                    InitializeFormComponents(eventID);
                }
            }
           
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            formControls.GeneratePopSelector("", storePopsPanel);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formControls.GeneratePopSelector("", purchasePopsPanel);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formControls.GeneratePopSelector("", mainHubPanel);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            formControls.GeneratePopSelector("", featuredPopPanel);

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
            formControls.RemoveCustomControls(storePopsPanel);
        }

        private void purSubB_Click(object sender, EventArgs e)
        {
            formControls.RemoveCustomControls(purchasePopsPanel);
        }

        private void mainSubB_Click(object sender, EventArgs e)
        {
            formControls.RemoveCustomControls(mainHubPanel);
        }

        private void featuredSubB_Click(object sender, EventArgs e)
        {
            formControls.RemoveCustomControls(featuredPopPanel);
        }

        private void prizeSubB_Click(object sender, EventArgs e)
        {
            formControls.RemoveCustomControls(prizePanel);
        }

        private void tierSub1_Click(object sender, EventArgs e)
        {
            formControls.RemoveCustomControls(tierPanel);
        }

        private void lastCSubB_Click(object sender, EventArgs e)
        {
            formControls.RemoveCustomControls(lastChanceBoxPanel);
        }

        private void genJsonBtn_Click(object sender, EventArgs e)
        {
            try
            {
                JsonGeneration jGen = new JsonGeneration();
                jGen.GenerateMyJson(this);
            }
            catch
            {
                return;
            }
        }
        private void dragDropBoxPlaybook_DragOver(object sender, DragEventArgs e)
        {
            DragOverBehavior(e);
        }
        private void dragDropBoxPlaybook_DragDrop(object sender, DragEventArgs e)
        {
            DragDropBehavior(dragDropBoxPlaybook, e);
            playbookPath = dragDropBoxPlaybook.Text;
            Debug.WriteLine(playbookPath);
            formControls = new FormControls(dragDropBoxData.Text, playbookPath);
            Converters converter = new Converters();
            boxIDs = converter.GetBoxIds(playbookPath);
            boxIdCB.DataSource = boxIDs;
        }
        private void dragDropBoxGacha_DragDrop(object sender, DragEventArgs e)
        {
            DragDropBehavior(dragDropBoxGacha, e);
            gachaPath = dragDropBoxGacha.Text;
        }

        private void dragDropBoxGacha_DragOver(object sender, DragEventArgs e)
        {
            DragOverBehavior(e);
        }

        public void DragOverBehavior(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }
        public void DragDropBehavior(TextBox textBox, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];   
            if (files != null && files.Any())
                textBox.Text = files.First(); 
        }

        private void dragDropBoxData_DragDrop(object sender, DragEventArgs e)
        {
            DragDropBehavior(dragDropBoxData, e);
            databasePath = dragDropBoxData.Text;
        }

        private void dragDropBoxData_DragOver(object sender, DragEventArgs e)
        {
            DragOverBehavior(e);
        }

        private void fileDirectoryTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void fileDirectoryTextBox_DragDrop_1(object sender, DragEventArgs e)
        {
            DragDropBehavior(fileDirectoryTextBox, e);
            directoryPath = fileDirectoryTextBox.Text;
        }

        private void fileDirectoryTextBox_DragOver_1(object sender, DragEventArgs e)
        {
            DragOverBehavior(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            sCU.AddToMysteryBoxConfig();
        }

        private void isEventCheck_Click(object sender, EventArgs e)
        {
            if (oedBoxCheck.Checked == true)
                oedBoxCheck.Checked = false;

            if (OtherBoxCheck.Checked == true)
                OtherBoxCheck.Checked = false;
        }

        private void oedBoxCheck_Click(object sender, EventArgs e)
        {
            if (isEventCheck.Checked == true)
                isEventCheck.Checked = false;

            if (OtherBoxCheck.Checked == true)
                OtherBoxCheck.Checked = false;
        }

        private void OtherBoxCheck_Click(object sender, EventArgs e)
        {
            if (isEventCheck.Checked == true)
                isEventCheck.Checked = false;

            if (oedBoxCheck.Checked == true)
                oedBoxCheck.Checked = false;
        }
    }
}
    

