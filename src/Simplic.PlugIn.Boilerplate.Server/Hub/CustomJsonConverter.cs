using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.PlugIn.Boilerplate.Server
{
    public class CustomJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var token = JToken.FromObject(value);
            writer.WriteStartObject();
            foreach (var rootToken in token)
            {
                if (rootToken.Type == JTokenType.Property)
                {
                    ConvertProperties(rootToken);
                }

                rootToken.WriteTo(writer);
            }
        }

        private static void ConvertProperties(JToken rootToken)
        {
            if (rootToken is JProperty)
            {
                var jProperty = rootToken as JProperty;
                if (jProperty.Value.Type == JTokenType.Array)
                {
                    ConvertArray(jProperty.Value);
                }
                else if (jProperty.Value.Type == JTokenType.Object)
                {
                    ConvertProperties(jProperty.Value);
                }
                else
                {
                    ConvertProperty(jProperty);
                }
            }
            else if(rootToken is JObject)
            {
                var jObject = rootToken as JObject;
                foreach (var item in jObject.Children())
                {
                    ConvertProperties(item);
                }
            }
        }

        private static void ConvertArray(JToken token)
        {
            foreach (var value in token.Values())
            {
                if (value is JProperty)
                {
                    var jProperty = value as JProperty;
                    ConvertProperties(jProperty);
                }
            }
        }

        private static void ConvertProperty(JProperty jProperty)
        {
            var ob = new JObject(
                new JProperty("id", "0"),
                new JProperty("user", "SuperUser"),
                new JProperty("value", jProperty.Value)
                );

            jProperty.Value.Replace(new JArray(ob));
        }
    }
}
