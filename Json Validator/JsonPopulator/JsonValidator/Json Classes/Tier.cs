using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper.Configuration.Attributes;
using JsonValidator.CSV;

namespace JsonValidator
{
    public class Tier
    {
        public int cost { get; set; }
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
                Debug.WriteLine("Cost value is " + gachaList[i].cost.Replace(",",""));
                tier.cost = Convert.ToInt32(gachaList[i].cost.Replace(",", "")); 

                //StringBuilder sb = new StringBuilder(gachaList[i].cost);
                //sb.Replace("\"", "12");

                //tier.cost = Convert.ToInt32(sb.Replace('\"', '2')); 

                tier.boxGuarantee = gachaList[i].guarantee;
               
                foreach (var x in gachaList[i].guarantee)
                    if (Char.IsNumber(x))
                        tier.amount = x.ToString();



                tier.numPulls = Convert.ToInt32(gachaList[i].boxPulls);

                Console.WriteLine("The tier obejct guarantee field should be: " + gachaList[i].guarantee);

                if (gachaList[i].guarantee != "") //added to control for Json serialization not skipping null values
                    tier.isGuarantee = true;

                if (Convert.ToInt32(gachaList[i].tier) < 12)
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
                        
                    else
                        guarantee.LuckyPopPrize = null;
                   

                        tierList[i].guarantee = guarantee;
                    }
                }
                return tierList;
        }

    

    }

}
