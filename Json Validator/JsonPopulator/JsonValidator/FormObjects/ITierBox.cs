using System.Windows.Forms;

namespace JsonValidator
{

    public interface IPrizeBox
    {
        string RewardType { get; set; }
        string RewardID { get; set; }
        int Amount { get; set; }
        int Instances { get; set; }
    };
}
