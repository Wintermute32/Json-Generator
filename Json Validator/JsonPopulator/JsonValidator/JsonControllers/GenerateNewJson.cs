using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;
using JsonValidator.JsonConverters;
using JsonValidator.StoreConfigUpdate;

namespace JsonValidator.JsonControllers
{
   public class GenerateNewJson
    {
        private FormatBoxString fbs = new FormatBoxString();
        public void GenerateMyJson(Form1 form)
        {
            NewRootGeneration nrg = new NewRootGeneration();

            var finalRoot = nrg.GenerateNewRoot(form);
            var isEventBox = nrg.UpdateBoxType(finalRoot);
            var jsonOutput = FormatNewJson(finalRoot, isEventBox);
            var complete = GetTestFilePath();

            File.WriteAllText(complete, jsonOutput);
            System.Diagnostics.Process.Start(complete);
        }
        private string FormatNewJson(NewRoot finalRoot, bool isEventBox)
        {
            var jsonOutput = fbs.TestFormatString(SerializeJson(finalRoot));

            if (isEventBox)
            {
                jsonOutput = jsonOutput.TrimEnd() + ','; //removing white space and adding comma
                finalRoot.LastChanceBoxPrizes = null;
                jsonOutput += '\n' + fbs.TestFormatString(SerializeJson(finalRoot));
                return jsonOutput;
            }
            jsonOutput = jsonOutput.TrimEnd() + ',';
            return jsonOutput;
        }
        public string GetTestFilePath()
        {
            var systemPath = System.Environment.
                             GetFolderPath(
                                 Environment.SpecialFolder.CommonApplicationData);
            
            var complete = Path.Combine(systemPath, "TestJson.Json");

            return complete;
        }
        public string SerializeJson(NewRoot newRoot)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            };

            settings.Converters.Add(new MyConverter());
            string json = JsonConvert.SerializeObject(newRoot, settings);
            return json;
        }
    }
}
