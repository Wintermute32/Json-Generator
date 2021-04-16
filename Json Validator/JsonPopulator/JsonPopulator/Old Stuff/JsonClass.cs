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
    public class MainHubAppearance
    {
        public bool canShowInCarousel { get; set; }
        public string style { get; set; }
        public string titleLocalizationKey { get; set; }
        public string subtitleLocalizationKey { get; set; }
        public List<string> popIds { get; set; }
    }
    public class Guarantee
    {
        [Name("Item Label")]
        public string SpecificPopId { get; set; }
        [Name("Box Pulls")]
        public string specificPopAmount { get; set; }
        
        [Name("Box Guarantee")]
        public string LuckyPopPrize { get; set; }
    }

    public class LastChanceBoxPrize
    {
        public string rewardType { get; set; }
        public string rewardId { get; set; }
        public int amount { get; set; }
        public int instances { get; set; }
    }

}
