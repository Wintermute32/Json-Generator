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

namespace JsonValidator
{
    public class LastChanceBoxPrize : IPrizeBox
    {
        public string RewardType { get; set; }
        public string RewardID { get; set; }
        public int Amount { get; set; }
        public int Instances { get; set; }

        public LastChanceBoxPrize()
        {
            RewardType = "pop";
        }
    }
}
        