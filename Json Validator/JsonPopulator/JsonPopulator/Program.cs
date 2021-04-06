using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

//Create objects using JsonClass Object. 
//Parse Relevant Documents to pull out string data.
//Assign that data based on logic to properties in appropriate Class.
//Combine those Objects into one single Json.
namespace JsonPopulator
{

    class Program
    {

        public static void Main(string[] args)
        {
            Program program = new Program();
            var eventID = program.GetUserInput();
            LivePlaybook livePlay = new LivePlaybook(@"C:\Users\pdnud\OneDrive\Desktop\Json Validator\Live Playbook.csv", eventID);

            Root root = new Root(eventID, livePlay);

            PopDatabase popData = new PopDatabase(@"C:\Users\pdnud\OneDrive\Documents\Repos\Json Generator\Json Validator\JsonPopulator\Docs\[1.5.0] Pop_Database - pop_database.csv", root);

            var popDict = popData.GetPopDict(popData.startDate);
            
            foreach (var x in popDict)
                Console.WriteLine(x.Key + ":" + x.Value);

            root.featuredPopIdsList = root.SetFeaturedPopIds(popDict); 

           //root.SetFeaturedPopIds(popData.GetPopDict(popData.startDate));

           string output = JsonConvert.SerializeObject(root);

            Console.WriteLine(output);
        }

  
        public string GetUserInput()
        {
            Console.WriteLine("Type the Event Name, no spaces with the set number IE: \'TheOffice3\'");
            return Console.ReadLine();
        }

    }

}


