using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;
using JsonValidator.CSV;

namespace JsonValidator
{
    public class Prize : IPrizeBox
    {
        public string RewardType { get; set; }
        public string RewardID { get; set; }
        public int Amount { get; set; }
        public int Instances { get; set; }

     
    }
}