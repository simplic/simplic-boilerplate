using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Simplic.PlugIn.Boilerplate.Server
{
    public class JsonConvertWrapper
    {
        const string removeTag = "RemoveThis";

        public static T DeserializeObject<T>(string value, string user, string baseUser)
        {
            var token = JToken.Parse(value);

            foreach (var rootItem in token.Values())
            {
                ConvertBackProperties(rootItem, user, baseUser);
            }

            return JsonConvert.DeserializeObject<T>(token.ToString());
        }

        private static void ConvertBackProperties(JToken token, string user, string baseUser)
        {
            if (token is JProperty)
            {
                var jProperty = token as JProperty;
                if (jProperty.Value.Type == JTokenType.Array)
                {
                    ConvertBackArray(jProperty.Value, user, baseUser);
                }
                else if (jProperty.Value.Type == JTokenType.Object)
                {
                    ConvertBackProperties(jProperty.Value, user, baseUser);
                }
            }
            else if (token is JObject)
            {
                var jObject = token as JObject;
                foreach (var item in jObject.Children())
                {
                    ConvertBackProperties(item, user, baseUser);
                }
            }
            else if (token is JArray)
            {
                var jArray = token as JArray;
                ConvertBackArray(jArray, user, baseUser);
            }
        }

        private static void ConvertBackArray(JToken value, string user, string baseUser)
        {
            var jArray = value as JArray;
            if (jArray.Children().Any(c => c.Type == JTokenType.Array))
            {
                for (int i = 0; i < jArray.Count; i++)
                {
                    ConvertBackProperties(jArray[i], user, baseUser);
                }

                var tokensToRemove = jArray.Where(x => x.Value<string>() == removeTag).ToList();
                foreach (var toRemove in tokensToRemove)
                {
                    jArray.Remove(toRemove);
                }
            }
            // Is a changeset array?
            else if(jArray.Children().All(c => !string.IsNullOrEmpty(c.Value<string>("value"))))
            {
                ConvertBackChangeset(value, user, baseUser);
            }
            // Go deeper.
            else
            {
                for (int i = 0; i < jArray.Count; i++)
                {
                    ConvertBackProperties(jArray[i], user, baseUser);
                }
            }
        }

        private static void ConvertBackChangeset(JToken value, string user, string baseUser)
        {
            var jArray = value as JArray;
            var token = jArray.FirstOrDefault(a => a.Value<string>("user") == user);
            if (token is null)
            {
                token = jArray.FirstOrDefault(a => a.Value<string>("user") == baseUser);
            }

            if (token.Value<string>("state") == "-1")
            {
                value.Replace(removeTag);
            }
            else if (!(token is null))
            {
                var resultValue = token.Value<string>("value");

                value.Replace(resultValue);
            }
        }
    }

    public class CustomJsonConverter : JsonConverter
    {
        public CustomJsonConverter(string baseUser)
        {
            BaseUser = baseUser;
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        /// <summary>
        /// Reads the object to json.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Writes the object to json.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
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
        private void ConvertProperties(JToken rootToken)
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
            else if (rootToken is JValue)
            {
                var jValue = rootToken as JValue;
                ConvertValue(jValue);
            }
        }

        /// <summary>
        /// Converts given array and nested tokens into change set tree.
        /// </summary>
        /// <param name="token">Should be of type <see cref="JArray"/>.</param>
        private void ConvertArray(JToken token)
        {
            var jArray = token as JArray;
            for (int i = 0; i < jArray.Count; i++)
            {
                ConvertProperties(jArray[i]);
            }
        }

        /// <summary>
        /// Converts a <see cref="JProperty"/> into a change set leaf.
        /// </summary>
        /// <param name="jProperty">Property to convert.</param>
        private void ConvertProperty(JProperty jProperty)
        {
            var ob = new JObject(
                new JProperty("id", "0"),
                new JProperty("user", BaseUser),
                new JProperty("state", "0"),
                new JProperty("value", jProperty.Value)
                );

            jProperty.Value.Replace(new JArray(ob));
        }

        /// <summary>
        /// Converts a <see cref="JValue"/> into a change set leaf.
        /// </summary>
        /// <param name="jValue">Value to convert.</param>
        private void ConvertValue(JValue jValue)
        {
            var ob = new JObject(
                new JProperty("id", "0"),
                new JProperty("user", BaseUser),
                new JProperty("state", "0"),
                new JProperty("value", jValue)
                );

            jValue.Replace(new JArray(ob));
        }

        /// <summary>
        /// Converts a <see cref="JObject"/> into a change set leaf.
        /// </summary>
        /// <param name="jObject">Object to convert.</param>
        private void ConvertObject(JObject jObject)
        {
            var ob = new JObject(
                new JProperty("id", "0"),
                new JProperty("user", BaseUser),
                new JProperty("state", "0"),
                new JProperty("value", jObject)
                );

            jObject.Replace(new JArray(ob));
        }

        public string BaseUser { get; set; }
    }
}
