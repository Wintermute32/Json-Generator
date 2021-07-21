using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace JsonValidator.StoreConfigUpdate
{
    //Get list of just box ID's per each MysteryStoreConfig File (5x files)
    //add newBoxID to list and sort alphabetically (works because of ID Number value)
    //For each MysterBoxConfig ID list, get the boxID directly after the newBoxID
    //return list of ID's that come right after newBoxID (done to ensure insert still works despite non-parity between BoxConfig files)
    //Convert boxfiles to List<string> and find line containing the 'justAfterID'.
    //get index of that line, and insert newbox string there for each boxconfig file.
    public class StoreConfig
    {
        JsonGeneration jGenObj = new JsonGeneration();
        public void AddToMysteryBoxConfig()
        {
            List<string> boxFile;
            string directoryPath = Application.OpenForms["Form1"].Controls["fileDirectoryTextBox"].Text; //way to access desigern controls w/o changing access modifier
            
            string[] storeConfigPaths = new string[0];
            
            if (Directory.Exists(directoryPath))
                storeConfigPaths = Directory.GetFiles(directoryPath,  "*" + "MysteryBoxesConfig" + "*.*", SearchOption.AllDirectories);

            //Below path is for testing purposes. used to generate list of Index positions for new box file per-variant file.

            var testFilePath = jGenObj.GetTestFilePath();

            List<string> insertAboveID = GetInsertID(storeConfigPaths, testFilePath);


           string newBox = File.ReadAllText(testFilePath);

            for (int i = 0; i < storeConfigPaths.Length; i++) 
            {
                boxFile = File.ReadAllLines(storeConfigPaths[i]).ToList(); 
                //use insertAboveID value to find exisitng box file
                //and insert newbox above it.
                foreach (var x in boxFile)
                {
                    if (x.Contains(insertAboveID[i]))
                    {
                        //would like to find a way to dynamically get index of the above {
                        //to protect against extra spaces from improperly formatted code in the box file
                        
                        //var index = boxFile.IndexOf(x);
                       int insertPos = GetInsertPos(x, boxFile);
                       boxFile.Insert(insertPos, newBox); 
                       break;
                    }
                }
                File.WriteAllLines(storeConfigPaths[i], boxFile);
                System.Diagnostics.Process.Start(storeConfigPaths[i]);
            }
        }
        public int GetInsertPos(string boxLine, List<string> boxFile)
        {
            //ensuring formatting mistakes btwn existing boxes 
            //don't throw off insert pos
            var index = boxFile.IndexOf(boxLine) - 1; //-1 to bump index to { line

            if (!boxFile[index].Contains('{'))
            {
                int i = 0;
                while (!boxFile[index - i].Contains('{') && !boxFile[index - i].Contains("},"))
                {
                    i++;
                }

                return index - i; //aligning 
            }
            else
                return index;         
        }
        private List<string> GetInsertID(string[] storeConfigFilePaths, string newBoxFilePath)
        {
            //returns a list of existing box Id's for each MysteryBoxConfig.Json
            //directly beneath our new Box Ids
            //incase variant box files are not in Parity, therefore require their
            //own idsToListAbove for each boxFile.

            List<List<string>> boxIDsPerVariant = new List<List<string>>();
            List<string> idsToListAbove = new List<string>();

            string newBoxIds = GetBoxID(newBoxFilePath)[0]; //get the first non-VIP box

            for (int i = 0; i < storeConfigFilePaths.Length; i++)
            {
                boxIDsPerVariant.Add(GetBoxID(storeConfigFilePaths[i])); //For each Variant Box File, Add List of Box Id's
                boxIDsPerVariant[i].Add(newBoxIds);//add our NewBox ID
                boxIDsPerVariant[i].Sort(); 
            }

            for (int i = 0; i < boxIDsPerVariant.Count; i++)
            {
                var listAboveId = boxIDsPerVariant[i].IndexOf(newBoxIds) + 1; 
                idsToListAbove.Add(boxIDsPerVariant[i][listAboveId]);
                Debug.WriteLine("ID being added to idsToListAbove :" + boxIDsPerVariant[i][listAboveId]);

            }
            return idsToListAbove;
        }

       private List<string> GetBoxID(string newBoxPath) //using path so changes to the saved Json will load.
        {
            List<string> boxIds = new List<string>();
            string boxFileText = File.ReadAllText(newBoxPath);
  
            Regex rx = new Regex(@"[a-z][0-9]{4}[\w]{1,}"); //all box titles but 'Starter' and 'Infinite'. etc
            MatchCollection matches = rx.Matches(boxFileText);

            foreach (Match match in matches)
                boxIds.Add(match.Value);
            return boxIds;
         }
    }
        
}