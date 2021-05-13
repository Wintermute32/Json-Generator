using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System.Globalization;
using System.Linq;


namespace PopOwnership
{
    class Converters
    {

        public List<Database> databaseOwnerGen(string databasePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.HeaderValidated = null;
            config.MissingFieldFound = null;
            var reader = new StreamReader(databasePath);

            var csv = new CsvReader(reader, config);
            var dataBase = csv.GetRecords<Database>().ToList();

            return dataBase;
        }


        public List<PopOwnerForm> PopOwnerForms(string formPath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.HeaderValidated = null;
            config.MissingFieldFound = null;
            var reader = new StreamReader(formPath);

            var csv = new CsvReader(reader, config);
            var ownerFormList = csv.GetRecords<PopOwnerForm>().ToList();

            return ownerFormList;
        }

        public List<PopCollections> PopCollectionsGenerator(string collectionsPath)
        {

            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.HeaderValidated = null;
            config.MissingFieldFound = null;
            var reader = new StreamReader(collectionsPath);

            var csv = new CsvReader(reader, config);
            var collectionsList = csv.GetRecords<PopCollections>().ToList();

            foreach (var x in collectionsList)
                x.allPops = x.CombineAllPops();

            return collectionsList;
        }

        public List<PopOwnerForm> CombineObjects(List<Database> dataList, List<PopOwnerForm> ownersFormList, List<PopCollections> popCollectionsList)
        {
            foreach (var x in ownersFormList)
                foreach (var y in dataList)
                    if (x.popName == y.popID)
                        x.rarity = y.rarity;


            foreach (var x in ownersFormList)
                foreach (var y in popCollectionsList)
                    if (y.allPops.Contains(x.popName))
                        x.collection = y.collection;

            return ownersFormList;
                
        }

        public void GenerateNewCSV(List<PopOwnerForm> ownersList)
        {
            using (var writer = new StreamWriter(@"C:\Users\pdnud\OneDrive\Desktop\testCSV.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(ownersList);
            }
        }

    }

    public class PopOwnerForm
    {
        [Name("PopName")]
        public string popName { get; set; }

        [Name("PlayerCount")]
        public string playerCount { get; set; }

        [Name("TotalActiveUsers")]
        public string totalActUsers { get; set; }

        [Name("ShareOfPlayers")]
        public string playerShare { get; set; }

        [Name("Rarity")]
        public string rarity { get; set; }

        [Name("PopCollection")]
        public string collection { get; set; }

    }

    public class PopCollections
    {

        [Name("CollectionId")]
        public string collection { get; set; }

        [Name("Pop1")]
        public string pop1 { get; set; }

        [Name("Pop2")]
        public string pop2 { get; set; }

        [Name("Pop3")]
        public string pop3 { get; set; }

        [Name("Pop4")]
        public string pop4 { get; set; }

        [Name("Pop5")]
        public string pop5 { get; set; }

        public string allPops { get; set; }

        
        public string CombineAllPops()
        {
           return pop1 + pop2 + pop3 + pop4 + pop5;
        }

    }
}
