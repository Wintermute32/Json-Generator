using Newtonsoft.Json;
using System.IO;
using System;
using JsonValidator.StoreConfigUpdate;

namespace JsonValidator.JsonControllers
{
   public class GenerateNewJson
    {
        public void GenerateMyJson(Form1 form)
        {
            FormatBoxString fbs = new FormatBoxString();
            NewRootGeneration nrg = new NewRootGeneration();

            var finalRoot = nrg.GenerateNewRoot(form);
            nrg.UpdateBoxType(finalRoot);
            var jsonOutput = fbs.FormatNewJson(finalRoot);
            var complete = GetTestFilePath();

            File.WriteAllText(complete, jsonOutput);
            System.Diagnostics.Process.Start(complete);
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
