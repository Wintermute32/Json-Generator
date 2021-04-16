using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;

namespace JsonPopulator
{
    class PopDatabase
    {
       public string popID { get; set; }
       public string rarity { get; set; }
       public string releaseDate { get; set; }
       public string eventExclusive { get; set; }
       

        public string FixStartDate(string startDate)
        {
            return DateTime.Parse(startDate).ToString("M/d/yyyy");
        }
      

    }

}
