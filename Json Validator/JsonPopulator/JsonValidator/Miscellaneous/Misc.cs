using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonValidator.Miscellaneous
{
    class Misc
    {
        public List<string> ParsePopDictionary(Dictionary<string, string> popDict)
        {
            List<string> popIDs = new List<string>();
            foreach (var x in popDict)
            {
                if (x.Key != null)
                    popIDs.Add(x.Key);
            }
            return popIDs;
        }

    }
}
