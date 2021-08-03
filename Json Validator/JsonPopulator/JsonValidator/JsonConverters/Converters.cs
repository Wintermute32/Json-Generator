using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Diagnostics;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Linq;
using JsonValidator.CSV;

//namespace jsonvalidator
//{
//    class converters
//    {
//        converter class to xfer data in csv objects to json class objects
//        public playbook playbookpopulator(string playbookpath, string eventid)
//        {
//            var playbookrecords = getplaybooklist(playbookpath);

//            foreach (var x in playbookrecords)
//                if (x.eventid.tolower().contains(eventid.replace(" ", "").tolower()))
//                {
//                    console.writeline("playbook found!");
//                    return x;
//                }
//            console.writeline("event name not found");
//            return null;
//        }

//        public list<database> databasepopulator(string databasepath, string startdate)
//        {
//            list<database> popdata = new list<database>();

//            var config = new csvconfiguration(cultureinfo.invariantculture);
//            config.headervalidated = null;
//            config.missingfieldfound = null;
//            var reader = new streamreader(databasepath);

//            var csv = new csvreader(reader, config);
//            var database = csv.getrecords<database>().tolist();

//            foreach (var x in database)
//                if (x.releasedate.contains(startdate))
//                {
//                    console.writeline("database found!");
//                    popdata.add(x);
//                }

//            if (popdata.count == 0)
//                console.writeline("event name not found");

//            return popdata;
//        }

//        public list<gacha> gachapopulator(string gachapath)
//        {

//            var config = new csvconfiguration(cultureinfo.invariantculture);
//            config.headervalidated = null;
//            config.missingfieldfound = null;
//            config.ignoreblanklines = true;

//            try
//            {

//                var reader = new streamreader(gachapath);
//                reader.readline();

//                var csv = new csvreader(reader, config);
//                var gachadata = csv.getrecords<gacha>().tolist();
//                gachadata.removerange(21, gachadata.count - 21);

//                if (gachadata.count == 0)
//                {
//                    console.writeline("gacha's not found");
//                }

//                return gachadata;
//            }
//            catch
//            {
//                return new list<gacha>();
//            }
//        }
//        public static list<string> getboxids(string playbookpath)
//        {
//            list<string> boxids = new list<string>();

//            var playbooklist = getplaybooklist(playbookpath);

//            foreach (var x in playbooklist)
//            {
//                if (x.eventid != null && x.eventid != "")
//                {
//                    boxids.add(x.eventid);
//                }
//            }
//            return boxids;
//        }
//        public static list<playbook> getplaybooklist(string playbookpath)
//        {
//            var config = new csvconfiguration(cultureinfo.invariantculture);

//            config.headervalidated = null;
//            config.missingfieldfound = null;
//            var reader = new streamreader(playbookpath);
//            reader.readline();
//            reader.readline(); //reassigns header as third line from top of csv

//            debug.writeline("playbook being read");

//            var csv = new csvreader(reader, config);
//            var playbookrecords = csv.getrecords<playbook>().tolist();
//            return playbookrecords;
//        }
//        public list<lastchanceboxprize> assignboxvalues(dictionary<string, string> popdict)
//        {
//            list<lastchanceboxprize> lcbplist = new list<lastchanceboxprize>();

//            foreach (var x in popdict)
//            {
//                lastchanceboxprize lastchancep = new lastchanceboxprize();
//                lastchancep.rewardid = x.key;
//                lcbplist.add(lastchancep);
//            }

//            lcbplist.reverse();

//            for (int i = 0; i < lcbplist.count; i++)
//            {
//                switch (i)
//                {
//                    case 0:
//                        lcbplist[i].amount = 1; lcbplist[i].instances = 3; break;
//                    case 1:
//                        lcbplist[i].amount = 2; lcbplist[i].instances = 2; break;
//                    case 2:
//                        lcbplist[i].amount = 3; lcbplist[i].instances = 2; break;
//                    case 3:
//                        lcbplist[i].amount = 6; lcbplist[i].instances = 1; break;

//                    case 4:
//                        lcbplist[i].amount = 6; lcbplist[i].instances = 1; break;
//                }

//            }
//            return lcbplist;
//        }
//    }
//}
