using System;

namespace PopOwnership
{
    class Program
    {
        static void Main(string[] args)
        {
            Converters converter = new Converters();
            var databaseList = converter.databaseOwnerGen(@"C:\Users\pdnud\OneDrive\Desktop\databaseTest.csv");
            var ownersList = converter.PopOwnerForms(@"C:\Users\pdnud\OneDrive\Desktop\ownersTest.csv");
            var collectionsList = converter.PopCollectionsGenerator(@"C:\Users\pdnud\OneDrive\Desktop\collectionsTest.csv");
            var finalList = converter.CombineObjects(databaseList, ownersList, collectionsList);
            converter.GenerateNewCSV(finalList);

        }
    }
}
