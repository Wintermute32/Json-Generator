using System;

namespace JsonValidator
{
    public class LastChanceBoxPrize : IPrizeBox
    {
        public string rewardType { get; set; }
        public string rewardId { get; set; }
        public int amount { get; set; }
        public int instances { get; set; }
        public LastChanceBoxPrize()
        {
            rewardType = "pop";
        }
    }
}
        