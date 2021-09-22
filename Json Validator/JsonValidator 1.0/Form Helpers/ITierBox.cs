using System.Windows.Forms;

namespace JsonValidator
{
    public interface ITierBox
    {
        void GeneratePrizeLine(FlowLayoutPanel flowPanel, string databasePath, Prize prize);
    }
}
