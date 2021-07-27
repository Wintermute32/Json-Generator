using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JsonValidator.CSV;

namespace JsonValidator
{
    public class FormControls
    {
        Database database = new Database();
        ComboBox comboB;
        string dataBPath;
        string playBPath;

        List<ComboBox> comboList = new List<ComboBox>();
        public FormControls(string databasePath, string playbookPath)
        {
            this.dataBPath = databasePath;
            this.playBPath = playbookPath;
        }

        public void GenerateRuntimePopPanels(NewRoot eventObject, string databasePath)
        {
            ITierBox tierBox;
            FlowLayoutPanel tierPanel = Application.OpenForms["Form1"].Controls["tierPanel"] as FlowLayoutPanel; //way to access desigern controls w/o changing access modifier
            FlowLayoutPanel mainHubPanel = Application.OpenForms["Form1"].Controls["mainHubPanel"] as FlowLayoutPanel;
            FlowLayoutPanel purchasePopsPanel = Application.OpenForms["Form1"].Controls["purchasePopsPanel"] as FlowLayoutPanel;
            FlowLayoutPanel storePopsPanel = Application.OpenForms["Form1"].Controls["storePopsPanel"] as FlowLayoutPanel;
            FlowLayoutPanel featuredPopPanel = Application.OpenForms["Form1"].Controls["featuredPopPanel"] as FlowLayoutPanel;
            FlowLayoutPanel prizePanel = Application.OpenForms["Form1"].Controls["prizePanel"] as FlowLayoutPanel;
            FlowLayoutPanel lastChanceBoxPanel = Application.OpenForms["Form1"].Controls["lastChanceBoxPanel"] as FlowLayoutPanel;

            //Get popIDs once then pass to GeneratePopSelector Methods
            //to avoid calling on each function call
            List<string> popIds = database.GetAllPopID(databasePath);

            foreach (var x in eventObject.appearance.storeButtonAppearance.popIds)
                GeneratePopSelector(x, storePopsPanel, popIds);

            foreach (var x in eventObject.appearance.purchaseScreenAppearance.popIds)
                GeneratePopSelector(x, purchasePopsPanel, popIds);

            foreach (var x in eventObject.appearance.mainHubAppearance.popIds)
                GeneratePopSelector(x, mainHubPanel, popIds);

            foreach (var x in eventObject.featuredPopIdsList)
                GeneratePopSelector(x, featuredPopPanel, popIds);

            foreach (var x in eventObject.prizes)
            {
                //Both prizeBox generates the form's last chance
                //and prize fields by way of its constructors. These instances
                //don't need to be used elsewhere
                new PrizeBox(prizePanel, databasePath, x);
            }

            foreach (var x in eventObject.lastChanceBoxPrizes)
            {
                new PrizeBox(lastChanceBoxPanel, databasePath, x);
            }

            foreach (var x in eventObject.tiers)
            {
                //same as above. TierBoxL/M/S contructors will add appropriate boxes to Tier Panels
                //on the form object instantiation. -- I believe I wrote it this way because
                //popID list wasn't updating correctly (and this was a weird workaround)
                if (x.isGuarantee == true && x.guarantee.LuckyPopPrize == null)
                    new TierBoxL(tierPanel, databasePath, x);

                if (x.isGuarantee == true && x.guarantee.LuckyPopPrize != null)
                    new TierBoxM(tierPanel, databasePath, x);

                if (x.isGuarantee != true)
                    new TierBoxS(tierPanel, databasePath, x);
            }
        }
        public void RemoveRuntimeComboBoxes(Form1 form1)
        {
            foreach (Control item in form1.Controls.OfType<FlowLayoutPanel>())
                item.Controls.Clear();
            foreach (Control item in form1.Controls.OfType<CheckBox>())
                item.Controls.Clear();
        }
        public void GeneratePopSelector(string popName, FlowLayoutPanel flowPanel, List<string> popIds)
        {
            comboB = new ComboBox() 
            {
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems
            };
            
            if (dataBPath != null)
                comboB.DataSource = popIds;
            
            flowPanel.Controls.Add(comboB);

            if (popName != "")
                comboB.Text = popName;
        }
        public void GeneratePopSelector(string popName, FlowLayoutPanel flowPanel)
        {
            comboB = new ComboBox()
            {
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems
            };

            if (dataBPath != null)
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
        public string AmendBoxId(string eventID)
        {
            eventID = eventID.Substring(eventID.LastIndexOf('_') + 1); //this is where we ammend the st
            return eventID;
        }
    }
} 
