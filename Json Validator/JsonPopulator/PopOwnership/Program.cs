using System;
using System.Diagnostics;

namespace PopOwnership
{
    class Program
    {
        static void Main(string[] args)
        {
            Converters converter = new Converters();
            var databaseList = converter.databaseOwnerGen(@"C:\Users\pdnud\OneDrive\Desktop\database.csv");
            var ownersList = converter.PopOwnerForms(@"C:\Users\pdnud\OneDrive\Desktop\ownership.csv");
            var collectionsList = converter.PopCollectionsGenerator(@"C:\Users\pdnud\OneDrive\Desktop\collections.csv");

            var finalList = converter.CombineObjects(databaseList, ownersList, collectionsList);
            converter.GenerateNewCSV(finalList);

        }
    }
}
