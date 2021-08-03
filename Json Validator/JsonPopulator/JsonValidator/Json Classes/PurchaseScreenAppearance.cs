using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JsonValidator
{
    public class PurchaseScreenAppearance
    {
        public string TitleLocKey { get; set; }
        public List<string> PopIds { get; set; }
        public PurchaseScreenAppearance() { }
        public PurchaseScreenAppearance(string fandomName, Dictionary<string, string> pops)
        {
            List<string> purchaseRarities = new List<string>() {"common", "rare", "epic", "legendary"};
            List<string> addToMePopId = new List<string>();

            foreach (var x in purchaseRarities)
                foreach (KeyValuePair<string, string> entry in pops)
                    if (x == entry.Value)
                        addToMePopId.Add(entry.Key);

            PopIds = addToMePopId;
            TitleLocKey = fandomName + "BoxTitle";
        }
        public static PurchaseScreenAppearance GeneratePurchaseScreenApeparance(Form1 form)
        {
            var flowBoxes = form.Controls.OfType<FlowLayoutPanel>().ToList();
            var textBoxes = form.Controls.OfType<TextBox>().ToList();

            var popLists = flowBoxes.Find(x => x.Name == "purchasePopsPanel").Controls.OfType<ComboBox>().ToList();
            List<string> _popIds = new List<string>();

            foreach (var x in popLists) //break me into a new class with store appearance
            {
                _popIds.Add(x.Text);
            }
            PurchaseScreenAppearance purchaseScreenAppearance = new PurchaseScreenAppearance()
            {
                TitleLocKey = textBoxes.Find(x => x.Name == "purTitleLocKey").Text,
                PopIds = _popIds
            };

            return purchaseScreenAppearance;
        }
    }

}
