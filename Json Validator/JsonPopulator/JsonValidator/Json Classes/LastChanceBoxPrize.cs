using System;

namespace JsonValidator
{
    public class LastChanceBoxPrize : IPrizeBox
    {
        public string RewardType { get; set; }
        public string RewardID { get; set; }
        public int Amount { get; set; }
        public int Instances { get; set; }
        public LastChanceBoxPrize()
        {
            RewardType = "pop";
        }
    }
}
        