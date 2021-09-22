using System;
using System.IO;
using System.Collections.Generic;
using CsvHelper;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Linq;
using CsvHelper.Configuration.Attributes;
using CsvHelper.Configuration;


namespace JsonValidator.CSV
{
	public class Playbook
	{
		[Name("Event IP")]
		public string BoxID { get; set; }
		[Name("Event Num")]
		public string EventNumber { get; set; }
		[Name("Collection ID **")]
		public string FandomName { get; set; }
		[Name("Start Time (UTC)")]
		public string StartDate { get; set; }
		[Name("End Time (UTC)")]
		public string EndDate { get; set; }    
		[Name("Event ID **")]
		public string EventID { get; set; }
		public string StartDateAlternative { get; set; }

		public List<string> BoxIDList = new List<string>();
		public static Playbook UpdatePlaybookObj(string playbookPath, string eventID, Playbook eventPlaybook)
		{
			eventPlaybook = eventPlaybook.PlaybookPopulator(playbookPath, eventID);
			eventPlaybook.FixStartDate(eventPlaybook.StartDate);
			return eventPlaybook;
		}

		public static string ParseEventNumber(string fullEventID)
		{
			Regex regEx = new Regex(@"\d+");
			return regEx.Match(fullEventID).Value;
		}
		public static string AmmendBoxID(string boxId)
		{
			if (boxId.Contains("mard"))
				boxId = boxId.Replace("_mardFE", "");

			if (boxId.Contains("marc"))
				boxId = boxId.Replace("_marcFE", "");

			if (boxId.Contains("bgod"))
				boxId = boxId.Replace("_bgodFE", "");

			return boxId;
		}
		private void FixStartDate(string startDate)
		{
			this.StartDateAlternative = DateTime.Parse(startDate).ToString("M/d/yyyy");
		}
		private Playbook PlaybookPopulator(string playbookPath, string eventID)
		{
			var playbookRecords = GetPlayBookList(playbookPath);

			foreach (var x in playbookRecords)
				if (x.EventID.ToLower().Contains(eventID.Replace(" ", "").ToLower()))
				{
					Console.WriteLine("Playbook found!");
					return x;
				}

			Console.WriteLine("Event Name Not Found");
			return new Playbook();
		}
		public static List<string> GetBoxIds(string playbookPath)
		{
			List<string> boxIDs = new List<string>();

			var playbookList = GetPlayBookList(playbookPath);

			foreach (var x in playbookList)
			{
				if (x.EventID != null && x.EventID != "" && !x.EventID.Contains(" "))
				{
					boxIDs.Add(x.EventID);
					//Debug.WriteLine("Adding :" + x.EventID);
				}
			}

			return boxIDs;
		}
		public static List<Playbook> GetPlayBookList(string playbookPath)
		{
			TextReader reader;
			List<Playbook> playbookRecords;

			if (File.Exists(playbookPath))
				reader = new StreamReader(playbookPath) as StreamReader;
			else
				reader = new StringReader(playbookPath);

			var config = new CsvConfiguration(CultureInfo.InvariantCulture);
			config.HeaderValidated = null;
			config.MissingFieldFound = null;
			config.IgnoreBlankLines = true;
			config.BadDataFound = null;

			//reassigns header as third line from top of CSV
			reader.ReadLine();
			reader.ReadLine(); 

			var csv = new CsvReader(reader, config);
			try
			{
				 playbookRecords = csv.GetRecords<Playbook>().ToList();
			}
			catch
			{
				MessageBox.Show("Are you sure this was a playbook file?");
				playbookRecords = new List<Playbook>();
			}

			return playbookRecords;

		}
	}
}
