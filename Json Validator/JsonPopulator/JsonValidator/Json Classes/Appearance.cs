using Newtonsoft.Json;
using System.Linq;
using System.Windows.Forms;


namespace JsonValidator
{
    public class Appearance : NewRoot
    {
        [JsonIgnore]
        public bool IsOEDBox { get; set; } 
        [JsonIgnore]
        public bool IsOtherBox { get; set; }
        [JsonIgnore]
        public bool IsVIPBox { get; set; }
        public bool isEventBox { get; set; } //not ignoring since 
        public string mysteryBoxType { get; set; }
        public string theme { get; set; }
        public StoreButtonAppearance storeButtonAppearance { get; set; }
        public PurchaseScreenAppearance purchaseScreenAppearance { get; set; }
        public MainHubAppearance mainHubAppearance { get; set; }
        public Appearance(){ }
        public Appearance(StoreButtonAppearance sbA, PurchaseScreenAppearance psA, MainHubAppearance mhA)
        {
            isEventBox = true;
            mysteryBoxType = "LuckyMystery";
            theme = "";
            storeButtonAppearance = sbA;
            purchaseScreenAppearance = psA;
            mainHubAppearance = mhA;
        }
        public Appearance(Form1 form)
        {
            //takes completed form to make Json
            var comboBoxes = form.Controls.OfType<ComboBox>().ToList();
            var checkBoxes = form.Controls.OfType<CheckBox>().ToList();

            isEventBox = checkBoxes.Find(x => x.Name == "isEventCheck").Checked;
            IsVIPBox = checkBoxes.Find(x => x.Name == "isVipBox").Checked;
            IsOEDBox = checkBoxes.Find(x => x.Name == "oedBoxCheck").Checked;
            IsOtherBox = checkBoxes.Find(x => x.Name == "OtherBoxCheck").Checked;
            mysteryBoxType = comboBoxes.Find(x => x.Name == "MysteryBoxCB").Text;

            theme = comboBoxes.Find(x => x.Name == "themeCB").Text;
            storeButtonAppearance = new StoreButtonAppearance(form);
            purchaseScreenAppearance = new PurchaseScreenAppearance(form);
            mainHubAppearance = new MainHubAppearance(form);

        }
    }
}
