using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

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
            var tokens = JToken.FromObject(value);
            writer.WriteStartObject();
            foreach (var rootToken in tokens)
            {
                if (rootToken.Type == JTokenType.Property)
                {
                    ConvertProperties(rootToken);
                }

                rootToken.WriteTo(writer);
            }
        }

        /// <summary>
        /// Converts <see cref="JProperty"/> and nested <see cref="JToken"/> into change set tree.
        /// <para>
        /// Is used recursively!
        /// </para>
        /// </summary>
        /// <param name="rootToken">Should be of type <see cref="JProperty"/>.</param>
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
            else if (rootToken is JObject)
            {
                var jObject = rootToken as JObject;
                foreach (var item in jObject.Children())
                {
                    ConvertProperties(item);
                }
            }
        }

        /// <summary>
        /// Converts given array and nested tokens into change set tree.
        /// </summary>
        /// <param name="token">Should be of type <see cref="JArray"/>.</param>
        private static void ConvertArray(JToken token)
        {
            var jArray = token as JArray;
            for (int i = 0; i < jArray.Count; i++)
            {
                if (jArray[i] is JProperty)
                {
                    var jProperty = jArray[i] as JProperty;
                    ConvertProperty(jProperty);
                }
                else if (jArray[i] is JValue)
                {
                    var jValue = jArray[i] as JValue;
                    ConvertValue(jValue);
                }
                else if (jArray[i] is JObject)
                {
                    var jObject = jArray[i] as JObject;
                    ConvertObject(jObject);
                }
            }

            var ob = new JObject(
                new JProperty("_new", new JArray()),
                new JProperty("_removed", new JArray()),
                new JProperty("_items", new JArray(token.Values())));

            token.Replace(ob);
        }

        /// <summary>
        /// Converts a <see cref="JProperty"/> into a change set leaf.
        /// </summary>
        /// <param name="jProperty">Property to convert.</param>
        private static void ConvertProperty(JProperty jProperty)
        {
            var ob = new JObject(
                new JProperty("id", "0"),
                new JProperty("user", "SuperUser"),
                new JProperty("value", jProperty.Value)
                );

            jProperty.Value.Replace(new JArray(ob));
        }

        /// <summary>
        /// Converts a <see cref="JValue"/> into a change set leaf.
        /// </summary>
        /// <param name="jValue">Value to convert.</param>
        private static void ConvertValue(JValue jValue)
        {
            var ob = new JObject(
                new JProperty("id", "0"),
                new JProperty("user", "SuperUser"),
                new JProperty("value", jValue)
                );

            jValue.Replace(new JArray(ob));
        }

        /// <summary>
        /// Converts a <see cref="JObject"/> into a change set leaf.
        /// </summary>
        /// <param name="jObject">Object to convert.</param>
        private static void ConvertObject(JObject jObject)
        {
            var ob = new JObject(
                new JProperty("id", "0"),
                new JProperty("user", "SuperUser"),
                new JProperty("value", jObject)
                );

            jObject.Replace(new JArray(ob));
        }
    }
}
