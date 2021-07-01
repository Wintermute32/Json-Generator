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
        public void AddToMysteryBoxConfig()
        {
            List<string> boxFile;
            string directoryPath = Application.OpenForms["Form1"].Controls["fileDirectoryTextBox"].Text; //way to access desigern controls w/o changing access modifier
            string[] storeConfigPaths = Directory.GetFiles(directoryPath, "*" + "MysteryBoxesConfig" + "*.*", SearchOption.AllDirectories);
            
            //Below path is for testing purposes. used to generate list of Index positions for new box file per-variant file.       
            List<string> insertAboveID = GetInsertPosition(storeConfigPaths, @"C:\Users\pdnud\OneDrive\Desktop\Json Validator\[1.5.0] mystery_boxes_config - Default.json");          
            
            string newBox = File.ReadAllText(@"C:\Users\pdnud\OneDrive\Desktop\Json Validator\[1.5.0] mystery_boxes_config - Default.json");

            for (int i = 0; i < storeConfigPaths.Length; i++) 
            {
                boxFile = File.ReadAllLines(storeConfigPaths[i]).ToList(); 
                //use insertAboveID value to find exisitng box file
                //and insert newbox above it.
                foreach (var x in boxFile)
                {
                    if (x.Contains(insertAboveID[i]))
                    {
                        boxFile.Insert(boxFile.IndexOf(x) - 1, newBox); // - 1 to account for existing box spacing.
                        break;
                    }
                }
                File.WriteAllLines(storeConfigPaths[i], boxFile);
                System.Diagnostics.Process.Start(storeConfigPaths[i]);
            }
             
        }
        private List<string> GetInsertPosition(string[] storeConfigFilePaths, string newBoxFilePath)
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