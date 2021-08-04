using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using JsonValidator.CSV;
using System.Linq;
using System.Windows.Forms;

namespace JsonValidator
{
    public class Tier
    {
        public int Cost { get; set; }
        public int NumPulls { get; set; }
        [JsonIgnore]
        public bool IsGuarantee { get; set; }
        [JsonIgnore]
        public bool IsLuckyPopPrize { get; set; } //will get passed to Guarantee Object
        [JsonIgnore]
        public string BoxGuarantee { get; set; } //will get passed to Guarantee Object   
        [JsonIgnore]
        public string Amount { get; set; }
        public Guarantee Guarantee { get; set; }
        public List<Tier> GenerateTierList(List<Gacha> gachaList)
        {
            List<Tier> tierList = new List<Tier>();
           
            for (int i = 0; i < gachaList.Count; i++)
            {
                Tier tier = new Tier();
                tier.Cost = Convert.ToInt32(gachaList[i].Cost.Replace(",", ""));
                tier.NumPulls = Convert.ToInt32(gachaList[i].BoxPulls);
                tier.BoxGuarantee = gachaList[i].Guarantee;

                if (gachaList[i].Guarantee != null) //incase gacha CSV doesn't include Guarantee Header
                    foreach (var x in gachaList[i].Guarantee)
                        if (Char.IsNumber(x))
                            tier.Amount = x.ToString();

                Console.WriteLine("The tier obejct guarantee field should be: " + gachaList[i].Guarantee);

                if (gachaList[i].Guarantee != "" && gachaList[i].Guarantee != null) //account for Json serialization not skipping null values
                    tier.IsGuarantee = true;

                if (Convert.ToInt32(gachaList[i].Tier) < 12)
                    tierList.Add(tier);
            }
            return tierList;
        }
        public List<Tier> AssignGuarantee(List<Tier> tierList, Dictionary<string, string> popDict)
        {
            List<string> rarities = new List<string> { "common", "rare", "epic", "legendary", "exclusive" };
       
                for (int i = 0; i < tierList.Count; i++)
                {
                    if (tierList[i].IsGuarantee == true)
                    {
                        Guarantee guarantee = new Guarantee();

                        foreach (var x in rarities)
                            if (tierList[i].BoxGuarantee != null && tierList[i].BoxGuarantee.ToLower().Contains(x))
                            {
                                foreach (KeyValuePair<string, string> entry in popDict)
                                    if (entry.Value == x)
                                        guarantee.SpecificPopId = entry.Key;
                            }
                        guarantee.specificPopAmount = tierList[i].Amount;
                  
                     if (tierList[i].BoxGuarantee == "Random Event Pop")
                        guarantee.LuckyPopPrize = true;
                        
                     else
                        guarantee.LuckyPopPrize = null;
                   
                     tierList[i].Guarantee = guarantee;
                    }
                }
           return tierList;
        }
        public static List<Tier> GenerateTierListConverter(List<FlowLayoutPanel> flowlist)
        {
            var popLists = flowlist.Find(x => x.Name == "tierPanel").Controls.OfType<GroupBox>().ToList();
            List<Tier> tierpoplist = new List<Tier>();

            foreach (var x in popLists)
            {
                Tier tier = new Tier();
                var combos = x.Controls.OfType<ComboBox>().ToList();
                var texts = x.Controls.OfType<TextBox>().ToList();

                int comboCount = combos.Count;

                switch (comboCount)
                {
                    case 3:
                        {
                            try
                            {
                                tier.Cost = Convert.ToInt32(texts[0].Text);
                                tier.NumPulls = Convert.ToInt32(combos[0].Text);
                                tier.Guarantee = new Guarantee()
                                {
                                    SpecificPopId = combos[1].Text,
                                    specificPopAmount = combos[2].Text
                                };
                            }
                            catch
                            {
                                tier.Cost = 0;
                                tier.NumPulls = 0;
                                tier.Guarantee = new Guarantee()
                                {
                                    SpecificPopId = "You didn't fill",
                                    specificPopAmount = "this out right",
                                };
                            }
                            break;
                        }

                    case 2:
                        {
                            try
                            {
                                tier.Cost = Convert.ToInt32(texts[0].Text);
                                tier.NumPulls = Convert.ToInt32(combos[0].Text);
                                tier.Guarantee = new Guarantee() { LuckyPopPrize = Convert.ToBoolean(combos[1].Text) };
                            }

                            catch
                            {
                                tier.Cost = 0;
                                tier.NumPulls = 0;
                                tier.Guarantee = new Guarantee() { LuckyPopPrize = false };
                            }
                            break;
                        }

                    case 1:
                        {
                            try
                            {
                                tier.Cost = Convert.ToInt32(texts[0].Text);
                                tier.NumPulls = Convert.ToInt32(combos[0].Text);
                            }
                            catch (Exception)
                            {
                                tier.NumPulls = 0;
                                tier.Cost = 0;
                            }
                            break;
                        }

                }
                tierpoplist.Add(tier);
            }
            return tierpoplist;
        }
    }

}
