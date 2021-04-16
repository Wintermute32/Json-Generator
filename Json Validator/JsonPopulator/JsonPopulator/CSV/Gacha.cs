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

namespace JsonPopulator.CSV
{
    class Gacha
    {
     [Name("Tier")]
    public string tier { get; set; }

    [Name("Cost (Coins)")]
        public string cost { get; set; }

    [Name("Box Pulls")]
        public string boxPulls { get; set; }

    [Name("Box Guarantee")]
        public string guarantee { get; set; }

    [Name("Rarity")]
        public string rarity { get; set; }

    [Name("Item Label")]
        public string popID { get; set; }

    [Name("Shard Count")]
        public string amount { get; set; }

    [Name("Item Quantity")]
        public string instances { get; set; }

        public string rewardType = "pop";
    }
}
