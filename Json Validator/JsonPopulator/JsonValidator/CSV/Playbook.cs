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
    public class Playbook
    {
        [Name("Event IP")]
        public string BoxID { get; set; }
        [Name("Event Num")]
        public string EventNumber { get; set; }
        [Name("Collection ID **")]
        public string FandomName { get; set; }
        [Name("Start Time (UTC)")]
        public string StartDate { get; set; }
        [Name("End Time \n(UTC)")]
        public string EndDate { get; set; }    
        [Name("Event ID **")]
        public string EventID { get; set; }
        public string StartDateAlternative { get; set; }

        public List<string> BoxIDList = new List<string>();
        public void FixStartDate(string startDate)
        {
            this.StartDateAlternative = DateTime.Parse(startDate).ToString("M/d/yyyy");
        }
    }
}
