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

namespace JsonPopulator
{
    public class LastChanceBoxPrize
    {
        public string rewardType { get; set; }
        public string rewardId { get; set; }
        public int amount { get; set; }
        public int instances { get; set; }

        public LastChanceBoxPrize()
        {
            rewardType = "pop";

        }
    
          public List<LastChanceBoxPrize> AssignBoxValues(Dictionary<string, string> popDict)
          {
            List<LastChanceBoxPrize> lcbpList = new List<LastChanceBoxPrize>();

            foreach (var x in popDict)
            {
                LastChanceBoxPrize lastChanceP = new LastChanceBoxPrize();
                lastChanceP.rewardId = x.Key;
                lcbpList.Add(lastChanceP);
            }

            lcbpList.Reverse();

            for (int i = 0; i < lcbpList.Count; i++)
            {
                switch (i)
                {
                    case 0 : 
                        lcbpList[i].amount = 1; lcbpList[i].instances = 3; break;
                    case 1 :
                        lcbpList[i].amount = 2; lcbpList[i].instances = 2; break;
                    case 2 :
                        lcbpList[i].amount = 3; lcbpList[i].instances = 2; break;
                    case 3:
                        lcbpList[i].amount = 6; lcbpList[i].instances = 1; break;

                    case 4:
                        lcbpList[i].amount = 6; lcbpList[i].instances = 1; break;
                }

            }
            return lcbpList;
          }
    }
}
        