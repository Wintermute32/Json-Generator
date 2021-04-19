using JsonPopulator.CSV;
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


namespace JsonPopulator
{
    public class Guarantee
    {
        public string SpecificPopId { get; set; }
        public string specificPopAmount { get; set; } 
        public bool LuckyPopPrize { get; set; }
    }

}
