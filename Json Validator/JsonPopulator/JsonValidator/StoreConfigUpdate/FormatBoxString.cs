using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace JsonValidator.StoreConfigUpdate
{
    class FormatBoxString
    {
        //this will be a combo of Regex to pick specifc lines
        //and string 'padding' to control the amount of indentation
        public string Indent(int count, string input)
        {
            //Regex rx = new Regex(@"^\s*", RegexOptions.Multiline);
            var newString = input.PadLeft(input.Length + count);

            return newString;
        }
        public string FormatString(string jsonString)
        {
            Regex rx = new Regex(@"^\s*", RegexOptions.Multiline);
            MatchCollection whiteSpaceMatches;
            List<string> spitMeout = new List<string>();
            string output = "";
            string[] lines = jsonString.Split(new[] { Environment.NewLine },StringSplitOptions.None);

            foreach (var x in lines)
            {
                whiteSpaceMatches = rx.Matches(x);
                Debug.WriteLine("White space matches" + whiteSpaceMatches);
                spitMeout.Add(Indent(whiteSpaceMatches.Count, x) + '\n');
            }
           
            foreach (var x in spitMeout)
                output += x;

            return output;
        }
        public string TestFormatString(string Json)
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
            {
                Debug.WriteLine(x);
                output += (x + '\n');
            }

            return output;
        }
    }
}
