using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using CsvHelper;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration.Attributes;

namespace JsonValidator.CSV
{
    public class Gacha
    {
     [Name("Tier")]
    public string Tier { get; set; }

    [Name("Cost (Coins)")]
        public string Cost { get; set; }

    [Name("Box Pulls")]
        public string BoxPulls { get; set; }

    [Name("Box Guarantee")]
        public string Guarantee { get; set; }

    [Name("Rarity")]
        public string Rarity { get; set; }

    [Name("Item Label")]
        public string PopID { get; set; }

    [Name("Shard Count")]
        public string Amount { get; set; }

    [Name("Item Quantity")]
        public string Instances { get; set; }

        public string RewardType = "pop";
         
        public List<Prize> PrizeList(List<Gacha> gachaList)
        {
            List<Prize> prizeList = new List<Prize>();

            for (int i = 0; i < gachaList.Count; i++)
            {
                Prize addMePrize = new Prize();

                if (gachaList[i].Amount != "" && gachaList[i].Instances != "")
                {
                    addMePrize.rewardId = gachaList[i].PopID;
                    addMePrize.rewardType = gachaList[i].RewardType;
                    addMePrize.amount = Convert.ToInt32(gachaList[i].Amount);
                    addMePrize.instances = Convert.ToInt32(gachaList[i].Instances);
                }
                if (addMePrize.rewardType != null)
                    prizeList.Add(addMePrize);
            }
            return prizeList;
        }
    }

   
}
