using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;

namespace JsonPopulator
{
    public class Prize
    {
        public string rewardType { get; set; }
        public string rewardId { get; set; }
        public int amount { get; set; }
        public int instances { get; set; }

        public Prize(string iD, string amount, string instances)
        {
            rewardType = "pop";
            rewardId = iD;
            this.amount = Convert.ToInt32(amount);
            this.instances = Convert.ToInt32(instances);
        }
    }
}