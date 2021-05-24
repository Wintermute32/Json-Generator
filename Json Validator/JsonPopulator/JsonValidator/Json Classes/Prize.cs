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
        public string rewardType { get; set; }
        public string rewardId { get; set; }
        public int amount { get; set; }
        public int instances { get; set; }

     
    }
}