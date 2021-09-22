using System.Windows.Forms;
using JsonValidator.CSV;
using System.Collections.Generic;

namespace JsonValidator.FormObjects
{
	public class CustomControls
	{
		public ComboBox _comboB1, comboB2, _comboB3, _comboB4;
		public TextBox textBox;
		public List<string> popIDList;
		public CustomControls()
		{
			popIDList = Database.PopIDList;
		}

		public virtual GroupBox GenerateControlsPanel(Tier tier)
		{
			GroupBox newGroupB = new GroupBox()
			{
				Name = "TierGroupBox",
				Location = new System.Drawing.Point(122, 1015),
				Size = new System.Drawing.Size(257, 46),
				TabIndex = 57,
				TabStop = false,
			};

			textBox = new TextBox()
			{
				Location = new System.Drawing.Point(7, 20),
				Size = new System.Drawing.Size(51, 20),
				TabIndex = 0,
				Text = tier.cost.ToString()
			};

			comboB2 = new ComboBox()
			{
				FormattingEnabled = true,
				Name = "amtBox",
				Location = new System.Drawing.Point(64, 19),
				Size = new System.Drawing.Size(38, 21),
				TabIndex = 1,
				Text = tier.numPulls.ToString(),
			};

				newGroupB.Controls.Add(textBox);
				newGroupB.Controls.Add(comboB2);

			return newGroupB;
		}
		public virtual void AddPopIDToCBDatabase(ComboBox comboB)
		{
			if (popIDList != null)
				foreach (var x in popIDList)
					comboB.Items.Add(x);
		}
		public virtual void AddToFlowPanel(FlowLayoutPanel flowPanel, GroupBox groupBox)
		{
			flowPanel.Controls.Add(groupBox);
		}
		public virtual Control.ControlCollection AssignComboBoxText(Tier tier, GroupBox newGroup)
		{
			var controls = newGroup.Controls;

			if (tier.guarantee != null)
			{
				controls[0].Text = tier.cost.ToString();
				controls[1].Text = tier.numPulls.ToString();

			}
			return controls;
		}
	}
}
