/*
 * Tic Toc Games Copyright 2021
 * This file is subject to the terms and conditions defined in
 * file 'LICENSE.txt', which is part of this source code package.
 */
using System;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

namespace TicToc.GoogleSheets.Tests
{

    [TestFixture]
    public class GoogleSheetsApiTests
    {
        /// <summary>
        /// Check to see if the service can even start up.
        /// </summary>
        [Test]
        public void AuthenticationTest()
        {
#if  UNITY_CLOUD_BUILD
            return;
#else
            // Arrange
            var instance = GoogleSheetsApi.Instance;
            
            // Act
            var wasSuccessful = instance.Authenticate();

            // Assert
            Assert.IsTrue(wasSuccessful);
            Assert.IsTrue(instance.IsAuthenticated);
#endif
        }
        
        /// <summary>
        /// Makes sure we convert to csv as expected.
        /// </summary>
        [Test]
        public void GetAsCsvTest()
        {
#if  UNITY_CLOUD_BUILD
            return;
#else
            // Arrange
            var instance = GoogleSheetsApi.Instance;
            if (!instance.IsAuthenticated) instance.Authenticate();
            const string sheetId = "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms";
            const string range = "Class Data!A1:F";
            
            // Act
            var csv = instance.GetAsCsv(sheetId, range);
            Debug.Log(csv);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(csv));
            Assert.AreEqual(CsvTestExpectedResult.Length, csv.Length);
            Assert.AreEqual(CsvTestExpectedResult, csv);
#endif
        }
        
        /// <summary>
        /// Append to the end of the spreadsheet.
        /// Check the spreadsheet itself to see if it is updated.
        /// </summary>
        [Test]
        public void AppendRowTest()
        {
#if  UNITY_CLOUD_BUILD
            return;
#else
            // Arrange
            var instance = GoogleSheetsApi.Instance;
            if (!instance.IsAuthenticated) instance.Authenticate();
            const string sheetId = "1cHr5kbevQDKXRPrfzzMlgJ7j3Ey97w7AFut1cHRTLGA";
            const string range = "Sheet1!A1:C"; //Always append to the end of spreadsheet no matter the cells selected
            
            // Act
            var data = new List<object>() {"AAA", "BBB", "CCC", "DDD" , "EEE"};
            instance.AppendRow(sheetId, range, data);
            var csv = instance.GetAsCsv(sheetId, range);
            Debug.Log(csv);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(csv));
#endif
        }
        
        /// <summary>
        /// Append a 2D string array to the end of the spreadsheet.
        /// </summary>
        [Test]
        public void Append2DStrArrayTest()
        {
#if  UNITY_CLOUD_BUILD
            return;
#else
            // Arrange
            var instance = GoogleSheetsApi.Instance;
            if (!instance.IsAuthenticated) instance.Authenticate();
            const string sheetId = "1cHr5kbevQDKXRPrfzzMlgJ7j3Ey97w7AFut1cHRTLGA";
            const string range = "Sheet1!A1:C"; //Always append to the end of spreadsheet no matter the cells selected
            
            // Act
            string[,] arr =
            {
                {"111", "222", "333", "444"}, 
                {"555", "666", "777", "888"}
            };
            instance.Append2DStrArray(sheetId, range, arr);
            var csv = instance.GetAsCsv(sheetId, range);
            Debug.Log(csv);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(csv));
#endif
        }
        
        /// <summary>
        /// Update the 5th row of the spreadsheet.
        /// </summary>
        [Test]
        public void UpdateTest()
        {
#if  UNITY_CLOUD_BUILD
            return;
#else
            // Arrange
            var instance = GoogleSheetsApi.Instance;
            if (!instance.IsAuthenticated) instance.Authenticate();
            const string sheetId = "1cHr5kbevQDKXRPrfzzMlgJ7j3Ey97w7AFut1cHRTLGA";
            const string range = "Sheet1!A5:C5";
            
            // Act
            var data = new List<object>() {"555AAA", "555BBB", "555CCC"};
            instance.Update(sheetId, range, data);
            var csv = instance.GetAsCsv(sheetId, "Sheet1!A1:C");
            Debug.Log(csv);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(csv));
#endif
        }
        
        /// <summary>
        /// Delete the 3rd row of the spreadsheet.
        /// </summary>
        [Test]
        public void DeleteRowTest()
        {
#if  UNITY_CLOUD_BUILD
            return;
#else
            // Arrange
            var instance = GoogleSheetsApi.Instance;
            if (!instance.IsAuthenticated) instance.Authenticate();
            const string sheetId = "1cHr5kbevQDKXRPrfzzMlgJ7j3Ey97w7AFut1cHRTLGA";

            // Act
            instance.DeleteRow(sheetId, 3);
            var csv = instance.GetAsCsv(sheetId, "Sheet1!A1:C");
            Debug.Log(csv);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(csv));
#endif
        }

        /// <summary>
        /// Append a dummy lock entry to the end of the spreadsheet.
        /// Check the spreadsheet itself to see if it is updated.
        /// </summary>
        [Test]
        public void AppendLockEntryTest()
        {
#if  UNITY_CLOUD_BUILD
            return;
#else
            // Arrange
            var instance = GoogleSheetsApi.Instance;
            if (!instance.IsAuthenticated) instance.Authenticate();
            const string sheetId = "1cHr5kbevQDKXRPrfzzMlgJ7j3Ey97w7AFut1cHRTLGA";
            const string range = "Sheet1!A1:C"; //Always append to the end of spreadsheet no matter the cells selected
            
            // Act
            var data = new List<object>() { "Some file name.txt", instance.GetUserName(), instance.GetUserEmail(), DateTime.Now};
            instance.AppendRow(sheetId, range, data);
            var csv = instance.GetAsCsv(sheetId, range);
            Debug.Log(csv);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(csv));
#endif
        }

        /// <summary>
        /// I downloaded a csv from an example spreadsheet.
        /// </summary>
        private const string CsvTestExpectedResult =
            @"Student Name,Gender,Class Level,Home State,Major,Extracurricular Activity
Alexandra,Female,4. Senior,CA,English,Drama Club
Andrew,Male,1. Freshman,SD,Math,Lacrosse
Anna,Female,1. Freshman,NC,English,Basketball
Becky,Female,2. Sophomore,SD,Art,Baseball
Benjamin,Male,4. Senior,WI,English,Basketball
Carl,Male,3. Junior,MD,Art,Debate
Carrie,Female,3. Junior,NE,English,Track & Field
Dorothy,Female,4. Senior,MD,Math,Lacrosse
Dylan,Male,1. Freshman,MA,Math,Baseball
Edward,Male,3. Junior,FL,English,Drama Club
Ellen,Female,1. Freshman,WI,Physics,Drama Club
Fiona,Female,1. Freshman,MA,Art,Debate
John,Male,3. Junior,CA,Physics,Basketball
Jonathan,Male,2. Sophomore,SC,Math,Debate
Joseph,Male,1. Freshman,AK,English,Drama Club
Josephine,Female,1. Freshman,NY,Math,Debate
Karen,Female,2. Sophomore,NH,English,Basketball
Kevin,Male,2. Sophomore,NE,Physics,Drama Club
Lisa,Female,3. Junior,SC,Art,Lacrosse
Mary,Female,2. Sophomore,AK,Physics,Track & Field
Maureen,Female,1. Freshman,CA,Physics,Basketball
Nick,Male,4. Senior,NY,Art,Baseball
Olivia,Female,4. Senior,NC,Physics,Track & Field
Pamela,Female,3. Junior,RI,Math,Baseball
Patrick,Male,1. Freshman,NY,Art,Lacrosse
Robert,Male,1. Freshman,CA,English,Track & Field
Sean,Male,1. Freshman,NH,Physics,Track & Field
Stacy,Female,1. Freshman,NY,Math,Baseball
Thomas,Male,2. Sophomore,RI,Art,Lacrosse
Will,Male,4. Senior,FL,Math,Debate";
    }

}