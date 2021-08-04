using System.Windows.Forms;

namespace JsonValidator
{

    public interface IPrizeBox
    {
        string rewardType { get; set; }
        string rewardId { get; set; }
        int amount { get; set; }
        int instances { get; set; }
    };
}
