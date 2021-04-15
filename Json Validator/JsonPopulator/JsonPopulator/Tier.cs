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
    public class Tier
    {
        [Name("Cost (Coins)")]
        public int cost { get; set; }
        [Name("Box Pulls")]
        public int numPulls { get; set; }
        public Guarantee guarantee { get; set; }

    }

}
