using System;
using System.IO;
using System.Collections.Generic;
using CsvHelper;
using System.Diagnostics;
using System.Windows.Forms;
using CsvHelper.Configuration;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration.Attributes;

namespace JsonValidator.CSV
{
	class Populator
	{

        public List<Gacha> GoogleGachaPopulator(string gachaPath)
        {

            Debug.WriteLine("GATCHA INPUT IS : \n" + gachaPath);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.HeaderValidated = null;
            config.MissingFieldFound = null;
            config.IgnoreBlankLines = true;
            config.BadDataFound = null;

            TextReader reader;

            if (File.Exists(gachaPath))
                reader = new StreamReader(gachaPath);
            else
                reader = new StringReader(gachaPath);

            try
            {
                //var reader = new StringReader(gachaPath);
                reader.ReadLine();

                var csv = new CsvReader(reader, config);
                var gachaData = csv.GetRecords<Gacha>().ToList();
                gachaData.RemoveRange(21, gachaData.Count - 21);

                if (gachaData.Count == 0)
                {
                    MessageBox.Show("Gacha CSV Not Found!", "Incorret File");
                }

                return gachaData;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception is" + e);
                return new List<Gacha>();
            }
        }

        public List<Prize> GeneratePrizeList(List<Gacha> gachaList, List<string> popIDList)
        {
            List<Prize> prizeList = new List<Prize>();

            for (int i = 0; i < gachaList.Count; i++)
            {
                Prize addMePrize = new Prize();

                if (gachaList[i].Amount != "" && gachaList[i].Instances != "")
                {
                    var rawPopID = gachaList[i].PopID.Replace(" ", "");

                    if (popIDList.Contains(rawPopID))
                        addMePrize.rewardId = rawPopID;
                    else
                        addMePrize.rewardId = "Pop ID not found in Database";

                    addMePrize.rewardType = gachaList[i].RewardType;
                    addMePrize.amount = Convert.ToInt32(gachaList[i].Amount);
                    addMePrize.instances = Convert.ToInt32(gachaList[i].Instances);
                }
                if (addMePrize.rewardType != null)
                    prizeList.Add(addMePrize);
            }
            return prizeList;
        }
    }
}
