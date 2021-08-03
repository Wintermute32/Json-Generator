﻿using System;
using System.IO;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
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
                    addMePrize.RewardID = gachaList[i].PopID;
                    addMePrize.RewardType = gachaList[i].RewardType;
                    addMePrize.Amount = Convert.ToInt32(gachaList[i].Amount);
                    addMePrize.Instances = Convert.ToInt32(gachaList[i].Instances);
                }
                if (addMePrize.RewardType != null)
                    prizeList.Add(addMePrize);
            }
            return prizeList;
        }
        public List<Gacha> GachaPopulator(string gachaPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.HeaderValidated = null;
            config.MissingFieldFound = null;
            config.IgnoreBlankLines = true;

            try
            {
                var reader = new StreamReader(gachaPath);
                reader.ReadLine();

                var csv = new CsvReader(reader, config);
                var gachaData = csv.GetRecords<Gacha>().ToList();
                gachaData.RemoveRange(21, gachaData.Count - 21);

                if (gachaData.Count == 0)
                {
                    Console.WriteLine("Gacha's Not Found");
                }

                return gachaData;
            }
            catch
            {
                return new List<Gacha>();
            }

        }
    }
}
