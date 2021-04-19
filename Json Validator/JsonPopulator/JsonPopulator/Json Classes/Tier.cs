using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper.Configuration.Attributes;
using JsonPopulator.CSV;

namespace JsonPopulator
{
    public class Tier
    {
        public string cost { get; set; } //needs to go back to int
        public int numPulls { get; set; }

        [JsonIgnore]
        public bool isGuarantee { get; set; }

        [JsonIgnore]
        public bool isLuckyPopPrize { get; set; } //will get passed to Guarantee Object

        [JsonIgnore]
        public string boxGuarantee { get; set; } //will get passed to Guarantee Object
        
        [JsonIgnore]
        public string amount { get; set; }
        public Guarantee guarantee { get; set; }

        public List<Tier> GenerateTierList(List<Gacha> gachaList)
        {
            List<Tier> tierList = new List<Tier>();

            for (int i = 0; i < gachaList.Count; i++)
            {
                Tier tier = new Tier();
                tier.boxGuarantee = gachaList[i].guarantee;
                tier.amount = gachaList[i].amount;
                tier.cost = gachaList[i].cost; //convoluted. Need to get rid of "";
                tier.numPulls = Convert.ToInt32(gachaList[i].boxPulls);

                Console.WriteLine("The tier obejct guarantee field should be: " + gachaList[i].guarantee);

                if (gachaList[i].guarantee != null)
                    tier.isGuarantee = true;

                if (Convert.ToInt32(gachaList[i].tier) < 12) //Our conventions in the Json don't match our conventions in the Event Sheet
                    tierList.Add(tier);
            }
            return tierList;
        }

        public List<Tier> AssignGuarantee(List<Tier> tierList, Dictionary<string, string> popDict) //this might not belong here
        {
            List<string> rarities = new List<string> { "common", "rare", "epic", "legendary", "exclusive" };
          
                for (int i = 0; i < tierList.Count; i++)
                {
                    if (tierList[i].isGuarantee == true)
                    {
                        Guarantee guarantee = new Guarantee();

                        foreach (var x in rarities)
                            if (tierList[i].boxGuarantee.ToLower().Contains(x))
                            {
                                foreach (KeyValuePair<string, string> entry in popDict)
                                    if (entry.Value == x)
                                        guarantee.SpecificPopId = entry.Key;
                            }

                        guarantee.specificPopAmount = tierList[i].amount;

                     
                    if (tierList[i].boxGuarantee == "Random Event Pop")
                        guarantee.LuckyPopPrize = true;
                   

                        tierList[i].guarantee = guarantee;
                    }
                }
                return tierList;
        }

    }

}
