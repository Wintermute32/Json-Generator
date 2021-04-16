using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;


//I SHOULD BE USING A FUCKING CSV HELPER. IT WILL AUTO GENERATE OBJECTS ACCORDING TO THE CSV HEADERS!
namespace JsonPopulator
{

/*
    public class GachaParser
    {
        public List<Prize> EventPopsPrize = new List<Prize>();
        string[] rarityArray = new string[4] { "common", "epic", "rare", "legendary" };

        //may want to rewrite to return full line without substrings. Parse into substrings in individual methods below
        public List<string> ParseEventSheet(string path)
        {
            string[] allLines = File.ReadAllLines(path);

            List<string> parsedLines = new List<string>();

            for (int i = 0; i < allLines.Length; i++)
            {
                bool containsRarity = false;

                foreach (var x in rarityArray)
                    if (allLines[i].ToLower().Contains(x))
                        containsRarity = true;

                if (char.IsDigit(allLines[i][0]) && allLines[i].ToLower().Contains("event")
                    && containsRarity)
                {
                    int index = allLines[i].ToLower().LastIndexOf("event");
                    string passOne = allLines[i].Substring(index);
                    int indexTwo = passOne.IndexOf('%');
                    string inputMe = passOne.Substring(0, indexTwo);
                    Console.WriteLine(inputMe);
                    parsedLines.Add(inputMe);
                }
            }
            return parsedLines;
        }
        public List<Prize> RetPopPrizeLine(List<string> parsedLines)
        {
            List<Prize> boxPrizeList = new List<Prize>();
            PopDatabase popDatabase = new PopDatabase(@"C:\Users\pdnud\OneDrive\Documents\Repos\Json Generator\Json Validator\JsonPopulator\Docs\[1.5.0] Pop_Database - pop_database.csv");

            var iD = "ID NADA";
            var amount = "AMOUNT NADA";
            var instances = "INSTANCES NADA";

            for (int i = 0; i < parsedLines.Count; i++)
            {
             var lineSplit = parsedLines[i].Split(",");
              
               if( popDatabase.CheckPopIds(lineSplit[2]))
                    iD = lineSplit[2];

              amount = lineSplit[3];
              instances = lineSplit[4];
              Prize eventReward = new Prize(iD, amount, instances);
              boxPrizeList.Add(eventReward);
                     
            }
            return boxPrizeList;
        }

        public List<Tier> GetRewardTiers(List<string> parsedLines)
        {
            parsedLines.RemoveRange(0, 1);
            parsedLines.RemoveRange(12, parsedLines.Count - 12);

            for (int i = 0; i < parsedLines.Count; i++)
            {
                parsedLines[i].Split("");
            }

            return null;
        }

    }
*/

}
