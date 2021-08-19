using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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

		List<string> boxIDs;
		NewRoot eventObject;
		FormControls formControls;
		StoreConfig sCU;
		public Form1()
		{
			InitializeComponent();
			this.AutoScroll = true;
			formControls = new FormControls(databasePath, playbookPath);
			sCU = new StoreConfig();
			boxIDs = new List<string>();
		}
		
		private void InitializeFormComponents(string eventID)
		{
			//takes completed NewRoot Object and populates forum UI values
			//This should be in a different class(not the Program class either);
			eventObject = Program.GetJsonObject(databasePath, playbookPath, gachaPath, eventID);
			eventNumBox.Text = eventObject.EventNumber;
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
			behaviorCB.Text = eventObject.behaviourType;
			style2CB.Text = eventObject.appearance.mainHubAppearance.style; //might not be populating Right
			mainHubTitleLocKeyCB.Text = eventObject.appearance.mainHubAppearance.titleLocalizationKey;
			mainhubSubLocKey.Text = eventObject.appearance.mainHubAppearance.subtitleLocalizationKey;
			formControls.GenerateRuntimePopPanels(eventObject, databasePath);
		}
		private void button1_Click(object sender, EventArgs e)
		{
			//program halts instead of crashes if custom ID is used
			if (boxIdCB.SelectedItem != null && File.Exists(dragDropBoxData.Text))
			{
				formControls.ResetFormControls(this);
				string eventID = formControls.AmendBoxId(boxIdCB.SelectedItem.ToString());
				InitializeFormComponents(eventID);
			}
			else
				MessageBox.Show("Something's wrong. You're either missing a file or this box name doesn't exist in the playbook", "Try Again");
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
		private void fileDirectoryTextBox_DragDrop_1(object sender, DragEventArgs e)
		{
			DragDropBehavior(fileDirectoryTextBox, e);
			directoryPath = fileDirectoryTextBox.Text;
		}

		private void fileDirectoryTextBox_DragOver_1(object sender, DragEventArgs e)
		{
			DragOverBehavior(e);
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

			if (isVipBox.Checked == true)
				isVipBox.Checked = false;

			UpdateFormBasedOnBoxType();
		}

		private void oedBoxCheck_Click(object sender, EventArgs e)
		{
			if (isEventCheck.Checked == true)
				isEventCheck.Checked = false;

			if (OtherBoxCheck.Checked == true)
				OtherBoxCheck.Checked = false;

			if (isVipBox.Checked == true)
				isVipBox.Checked = false;

			UpdateFormBasedOnBoxType();
		}

		private void OtherBoxCheck_Click(object sender, EventArgs e)
		{
			if (isEventCheck.Checked == true)
				isEventCheck.Checked = false;

			if (oedBoxCheck.Checked == true)
				oedBoxCheck.Checked = false;

			if (isVipBox.Checked == true)
				isVipBox.Checked = false;

			UpdateFormBasedOnBoxType();
		}

		private void isVipBox_Click(object sender, EventArgs e)
		{
			//if (isEventCheck.Checked == true)
			//    isEventCheck.Checked = false;

			//if (oedBoxCheck.Checked == true)
			//    oedBoxCheck.Checked = false;

			//if (OtherBoxCheck.Checked == true)
			//    OtherBoxCheck.Checked = false;

			UpdateFormBasedOnBoxType();
		}

		public void UpdateFormBasedOnBoxType()
		{

			if (isEventCheck.Checked == true)
			{
				styleCB.Text = "LargePink";
				ribbonLocKeyCB.Text = "EventBoxRibbon";
				string boxTitle = formControls.AmendBoxId(boxIdCB.Text) + "BoxTitle";
				titleLocCB.Text = boxTitle;      
				purTitleLocKey.Text = boxTitle;
				mainHubTitleLocKeyCB.Text = boxTitle;
				mainhubSubLocKey.Text = "EventCarouselitemSubtitle";
				style2CB.Text = "Pink";

			}
		  
			if (OtherBoxCheck.Checked == true)
			{
				isEventCheck.Checked = false;
				styleCB.Text = "MediumPurple";
				this.ribbonLocKeyCB.Text = "GeneralBoxRibbon";
				string boxTitle = formControls.AmendBoxId(boxIdCB.Text).Replace("Box", "") + "BoxTitle";
				this.titleLocCB.Text = boxTitle;
				this.purTitleLocKey.Text = boxTitle;
				this.mainHubTitleLocKeyCB.Text = boxTitle;
				mainhubSubLocKey.Text = fandomIdCB.Text.Replace("Fandom", "").Replace("Box", "") + "BoxSubtitle";
				style2CB.Text = "Purple";
			}

			if (oedBoxCheck.Checked == true)
			{
				styleCB.Text = "LargePink";
				this.ribbonLocKeyCB.Text = "EventBoxRibbon";
				string boxTitle = formControls.AmendBoxId(boxIdCB.Text).Replace("Box", "") + "BoxTitle";
				this.titleLocCB.Text = boxTitle;
				this.purTitleLocKey.Text = boxTitle;
				this.mainHubTitleLocKeyCB.Text = boxTitle;
				style2CB.Text = "Pink";
			}

			if (isVipBox.Checked == true)
			{
				styleCB.Text = "VIP";
				ribbonLocKeyCB.Text = "VIPEventBoxRibbon";
				string vipBoxTitle = formControls.AmendBoxId(boxIdCB.Text).Replace("Box", "") + "VIPBoxTitle";
				titleLocCB.Text = vipBoxTitle;
				purTitleLocKey.Text = vipBoxTitle;
				mainHubTitleLocKeyCB.Text = vipBoxTitle;
				mainhubSubLocKey.Text = "EventCarouselitemSubtitle";
				style2CB.Text = "VIP";
			}
		}

	}
}
   