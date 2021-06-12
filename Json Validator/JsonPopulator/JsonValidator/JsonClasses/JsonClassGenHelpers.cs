using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonValidator.CSV;

namespace JsonValidator.Json_Classes
{
    public class JsonClassGenHelpers
    {
        public string AmmendBoxId(Playbook playbook, bool isEventBox)
        {
            string trimStr;

            if (isEventBox)
                trimStr = playbook.eventNumber + "_bxtFE_VIP0_" + playbook.fandomName.Replace(" ", "");

            else
                trimStr = playbook.eventNumber + "_bxtFE_VIP1_" + playbook.fandomName.Replace(" ", "");

            return trimStr;

        }
    }
}
