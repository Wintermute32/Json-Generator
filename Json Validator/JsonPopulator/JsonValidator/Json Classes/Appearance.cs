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
        public bool IsHolidayBox { get; set; }
        [JsonIgnore]
        public bool IsVIPBox { get; set; }
        public bool IsEventBox { get; set; } //not ignoring since this is a field on the Json
        public string MysteryBoxType { get; set; }
        public string Theme { get; set; }
        public StoreButtonAppearance StoreButtonAppearance { get; set; }
        public PurchaseScreenAppearance PurchaseScreenAppearance { get; set; }
        public MainHubAppearance MainHubAppearance { get; set; }
        public Appearance(){ }
        public Appearance(StoreButtonAppearance sbA, PurchaseScreenAppearance psA, MainHubAppearance mhA)
        {
            IsEventBox = true;
            MysteryBoxType = "LuckyMystery";
            Theme = "";
            StoreButtonAppearance = sbA;
            PurchaseScreenAppearance = psA;
            MainHubAppearance = mhA;
        }
        public static Appearance GenerateAppearance(Form1 form)
        {
           //StoreButtonConverter sBA = new StoreButtonConverter(form);
            //PurchaseScreenConverter pSA = new PurchaseScreenConverter(form);
            //MainHubConverter mHA = new MainHubConverter(form);

            var comboBoxes = form.Controls.OfType<ComboBox>().ToList();
            var checkBoxes = form.Controls.OfType<CheckBox>().ToList();

            Appearance appearance = new Appearance()
            {
                IsEventBox = checkBoxes.Find(x => x.Name == "isEventCheck").Checked,
                MysteryBoxType = comboBoxes.Find(x => x.Name == "MysteryBoxCB").Text,
                IsOEDBox = checkBoxes.Find(x => x.Name == "oedBoxCheck").Checked, //do i need this?

                Theme = comboBoxes.Find(x => x.Name == "themeCB").Text,
                StoreButtonAppearance = StoreButtonAppearance.GenerateStoreBA(form),
                PurchaseScreenAppearance = PurchaseScreenAppearance.GeneratePurchaseScreenApeparance(form),
                MainHubAppearance = MainHubAppearance.GenerateHubAppearance(form),
            };

            return appearance;
        }
    }
}
