﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JsonValidator.JsonControllers;
using System.Diagnostics;
using JsonValidator.StoreConfigUpdate;
using JsonValidator.CSV;

namespace JsonValidator
{
    public partial class Form1 : Form
    {
        //path values assign by drag-drop events below
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
            FormControls formControls = new FormControls(databasePath, playbookPath);
        }
        
        private void InitializeFormComponents(string eventID)
        {
            //takes completed NewRoot Object and populates forum UI values
            eventObject = Program.GetJsonObject(databasePath, playbookPath, gachaPath, eventID);
            eventNumBox.Text = eventObject.EventNumber;
            fandomIdCB.Text = eventObject.FandomID;
            startDatePicker.Value = DateTime.Parse(eventObject.StartDate);
            endDatePicker.Value = DateTime.Parse(eventObject.EndDate);
            isEventCheck.Checked = eventObject.Appearance.IsEventBox;
            MysteryBoxCB.Text = eventObject.Appearance.MysteryBoxType;
            themeCB.Text = eventObject.Appearance.Theme;
            styleCB.Text = eventObject.Appearance.StoreButtonAppearance.Style;
            ribbonLocKeyCB.Text = eventObject.Appearance.StoreButtonAppearance.RibbonLocKey;
            titleLocCB.Text = eventObject.Appearance.StoreButtonAppearance.TitleLocKey;
            subLocCB.Text = eventObject.Appearance.StoreButtonAppearance.SubtitleLocKey;
            orderCB.Text = eventObject.Appearance.StoreButtonAppearance.Order.ToString();
            discountCB.Text = eventObject.Appearance.StoreButtonAppearance.Discount.ToString();
            purTitleLocKey.Text = eventObject.Appearance.PurchaseScreenAppearance.TitleLocKey;
            canShowCarouselBox.Checked = eventObject.Appearance.MainHubAppearance.CanShowInCarousel;
            behaviorCB.Text = eventObject.BehaviorType;
            style2CB.Text = eventObject.Appearance.MainHubAppearance.Style; //might not be populating Right
            titleLocKeyCB.Text = eventObject.Appearance.MainHubAppearance.TitleLocKey;
            mainhubSubLocKey.Text = eventObject.Appearance.MainHubAppearance.SubtitleLocKey;

            formControls.GenerateRuntimePopPanels(eventObject, databasePath);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //program halts instead of crashes if custom ID is used
            if (boxIdCB.SelectedItem != null)
            {
               formControls.RemoveRuntimeComboBoxes(this);
               string eventID = formControls.AmendBoxId(boxIdCB.SelectedItem.ToString());
               InitializeFormComponents(eventID);
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
            TierBoxM tierBox = new TierBoxM(tierPanel, new Tier());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TierBoxS tierBox = new TierBoxS(tierPanel, new Tier());
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
                GenerateNewJson jGen = new GenerateNewJson();
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

            boxIDs = Playbook.GetBoxIds(playbookPath);
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

            if (formControls == null)
                formControls = new FormControls(dragDropBoxData.Text, playbookPath);
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
    

