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

            PopDatabase popData = new PopDatabase(@"/Users/piercenudd/Projects/Json Generator/Json Validator/JsonPopulator/Docs/[1.5.0] Pop_Database - pop_database.csv", root);

           var dataDict = popData.GetPopDict(popData.startDate);

            foreach (KeyValuePair<string, string> entry in dataDict)
                Console.WriteLine(entry.Key + " Equals " + entry.Value);


           string output = JsonConvert.SerializeObject(root);

            Console.WriteLine("\n" + output);
        }

  
        public string GetUserInput()
        {
            Console.WriteLine("Type the Event Name, no spaces with the set number IE: \'TheOffice3\'");
            return Console.ReadLine();
        }

    }

}


