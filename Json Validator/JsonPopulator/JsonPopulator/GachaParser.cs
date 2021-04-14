using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;

namespace JsonPopulator
{
    public class GachaParser
    {
        public List<Prize> EventPopsPrize = new List<Prize>();
        string[] rarityArray = new string[4] { "common", "epic", "rare", "legendary" };

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

            var iD = "ID NADA";
            var amount = "AMOUNT NADA";
            var instances = "INSTANCES NADA";

            for (int i = 0; i < parsedLines.Count; i++)
            {
             var lineSplit = parsedLines[i].Split(",");
              
              iD = lineSplit[2];
              amount = lineSplit[3];
              instances = lineSplit[4];
              Prize eventReward = new Prize(iD, amount, instances);
              boxPrizeList.Add(eventReward);
                     
            }
            return boxPrizeList;
        }
         
     }

}
