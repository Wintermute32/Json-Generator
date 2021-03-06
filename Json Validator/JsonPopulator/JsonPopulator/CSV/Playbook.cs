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
    class Playbook
    {
        [Name("Event IP")]
        public string boxID { get; set; }
        [Name("Event Num")]
        public string eventNumber { get; set; }
        [Name("Collection ID **")]
        public string fandomName { get; set; }
        
        [Name("Start Time (UTC)")]
        public string startDate { get; set; }
        [Name("End Time \n(UTC)")]
        public string endDate { get; set; }

        public string startDateAlternate { get; set; }

        public void FixStartDate(string startDate)
        {
            this.startDateAlternate = DateTime.Parse(startDate).ToString("M/d/yyyy");
        }
    }
}
