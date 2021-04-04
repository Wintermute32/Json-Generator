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


