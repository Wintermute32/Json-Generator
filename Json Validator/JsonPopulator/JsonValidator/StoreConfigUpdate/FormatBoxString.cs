using System;
using System.Collections.Generic;
using System.Linq;
using JsonValidator.JsonControllers;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace JsonValidator.StoreConfigUpdate
{
    class FormatBoxString
    {
        public string FormatNewJson(NewRoot finalRoot)
        {
            GenerateNewJson genNewJson = new GenerateNewJson();
            var jsonOutput = TestFormatString(genNewJson.SerializeJson(finalRoot));

            //if (isEventBox)
            //{
            //    jsonOutput = jsonOutput.TrimEnd() + ','; //removing white space and adding comma
            //    finalRoot.LastChanceBoxPrizes = null;
            //    jsonOutput += '\n' + TestFormatString(genNewJson.SerializeJson(finalRoot)).TrimEnd()+',';
            //    return jsonOutput;
            //}

            jsonOutput = jsonOutput.TrimEnd() + ',';
            return jsonOutput;
        }
        private string TestFormatString(string Json)
        { 
            string[] lines = Json.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            List<string> outputString = new List<string>();
            string output = "";

            int indentAmt = 8;
            
            for (int i = 0; i < lines.Length; i++)
            {
              var trimmed = lines[i].Trim();
                
              if (trimmed.Contains("}"))
                 indentAmt -= 4;

              if ((trimmed.Contains("}") && trimmed.Contains("{")))
                    indentAmt = 16;

              if ( trimmed == "]," || trimmed.Contains("lastChanceBox") 
                || trimmed.Contains("tiers") || trimmed == "]")
                    indentAmt = 12;

                outputString.Add(Indent(indentAmt, trimmed));

                if (trimmed.Contains("{"))
                 indentAmt += 4;
            }

            foreach (var x in outputString)
                output += (x + '\n');

            return output;
        }
        private string Indent(int count, string input)
        {
            var newString = input.PadLeft(input.Length + (count/4),'\t');
            return newString;
        }
    }
}
