using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using JsonValidator.Properties;
using JsonValidator.CSV;
using JsonValidator.Form_Helpers;

namespace JsonValidator.Form_Helpers
{
    class FormHelpers
    {
        Form1 form1 = new Form1();
        public void RemoveRuntimeComboBoxes()
        {
            foreach (Control item in form1.StorePopsPanel.Controls.OfType<ComboBox>())
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
