using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JsonValidator
{
    //custom class derived from Newtonsoft JsonConvert to give us more control over the way we format
    //and generate our Json file. 
    class MyConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) //controlling what types are affected with formatting, indented or none
        {
            return objectType == typeof(string[]) || objectType == typeof(Tier) || objectType == typeof(Prize) 
                || objectType == typeof(List<string>) || objectType == typeof(LastChanceBoxPrize);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
           
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }

    }

} 
