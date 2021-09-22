using System.Windows.Forms;

namespace JsonValidator
{
	public interface IJsonOBject
	{
		void CSVtoJsonPopulate();
		void GenerateNewJson(); 
	}
	public interface IPrizeBox
	{
		string rewardType { get; set; }
		string rewardId { get; set; }
		int amount { get; set; }
		int instances { get; set; }
	};
}
