﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JsonValidator.CSV;

namespace JsonValidator
{
    public class FormControls
    {
        ComboBox ComboB;
        List<ComboBox> comboList = new List<ComboBox>();
        public void GenerateRuntimePopPanels(NewRoot eventObject)
        {
            var layoutPanelDict = GetLayoutPanelDict();
            AssignToPopPanels(eventObject, layoutPanelDict);
        }
        private Dictionary<string, FlowLayoutPanel> GetLayoutPanelDict()
        {
            string[] panelNames = new[] { "tierPanel", "mainHubPanel", "purchasePopsPanel", 
                "storePopsPanel", "featuredPopPanel", "prizePanel", "lastChanceBoxPanel" };
            
            Dictionary<string, FlowLayoutPanel> flowPanelDict = new Dictionary<string, FlowLayoutPanel>();
            
            var formControls = Application.OpenForms["Form1"].Controls;
            
            foreach (var x in panelNames)
                flowPanelDict.Add(x, formControls[x] as FlowLayoutPanel);
  
            return flowPanelDict;  
        }
        private void AssignToPopPanels(NewRoot newRoot, Dictionary<string, FlowLayoutPanel> layoutPanels)
        {
            var appearance = newRoot.appearance;

            foreach (var popId in appearance.storeButtonAppearance.popIds)
                GeneratePopSelector(popId, layoutPanels["storePopsPanel"]);

            foreach (var popId in appearance.purchaseScreenAppearance.popIds)
                GeneratePopSelector(popId, layoutPanels["purchasePopsPanel"]);

            foreach (var popId in appearance.mainHubAppearance.popIds)
                GeneratePopSelector(popId, layoutPanels["mainHubPanel"]);

            foreach (var popId in newRoot.featuredPopIds)
                GeneratePopSelector(popId, layoutPanels["featuredPopPanel"]);

            foreach (var x in newRoot.prizes)
                new PrizeBox(layoutPanels["prizePanel"], x);

            foreach (var x in newRoot.lastChanceBoxPrizes)
                new PrizeBox(layoutPanels["lastChanceBoxPanel"], x);

            foreach (var x in newRoot.tiers)
            {
                if (x.IsGuarantee == true && x.guarantee.LuckyPopPrize == null)
                    new TierBoxL(layoutPanels["tierPanel"], x);

                else if (x.IsGuarantee == true && x.guarantee.LuckyPopPrize != null)
                    new TierBoxM(layoutPanels["tierPanel"], x);

                else if (x.IsGuarantee != true)
                    new TierBoxS(layoutPanels["tierPanel"], x);
            }
        }

        public void ResetFormControls(Form1 form1)
        {
            foreach (Control item in form1.Controls.OfType<FlowLayoutPanel>())
                item.Controls.Clear();

            //should be able to Upcast this and save a third foreach loop
            foreach (Control item in form1.Controls.OfType<CheckBox>())
                item.Controls.Clear();

            foreach (CheckBox item in form1.Controls.OfType<CheckBox>())
                item.Checked = false;
        }
        public void GeneratePopSelector(string popName, FlowLayoutPanel flowPanel)
        {

            ComboB = new ComboBox()
            {
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.ListItems,
                //DataSource = Database.popIDList
            };

            if (Database.PopIDList != null)
                foreach (var x in Database.PopIDList)
                    ComboB.Items.Add(x);

            flowPanel.Controls.Add(ComboB);               
            
            if (popName != "")
                ComboB.Text = popName;
        }
        public void RemoveCustomControls(FlowLayoutPanel panel)
        {
            var controlList = panel.Controls.OfType<Control>().ToList();
            if (controlList.Count > 0)
                panel.Controls.Remove(controlList[controlList.Count - 1]);
        }
        public string AmendBoxId(string eventID)
        {
            eventID = eventID.Substring(eventID.LastIndexOf('_') + 1);
            Debug.WriteLine("Amended event ID " + eventID);
            return eventID;
        }
    }
} 