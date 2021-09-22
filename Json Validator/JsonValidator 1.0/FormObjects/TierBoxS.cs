using System.Collections.Generic;
using System.Windows.Forms;
using JsonValidator.FormObjects;

namespace JsonValidator
{
    public class TierBoxS : CustomControls
    {
        public TierBoxS(FlowLayoutPanel flowPanel,Tier tier)
        {
           var newGroupB = GenerateControlsPanel(tier);
           AddToFlowPanel(flowPanel, newGroupB);
           AssignComboBoxText(tier, newGroupB);
        }
    }
}
