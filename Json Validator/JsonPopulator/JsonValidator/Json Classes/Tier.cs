using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using JsonValidator.CSV;

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
    }

}
