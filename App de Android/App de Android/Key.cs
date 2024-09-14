using System;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Linq;


namespace App_de_Android
{


    public static class Key
    {
        private static string ApiKey;

        static Key()
        {
            // Get the stream for the embedded resource
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("App_de_Android.ApiKey.json"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string jsonString = reader.ReadToEnd();
                    var jsonObject = JObject.Parse(jsonString);
                    ApiKey = jsonObject["apiKey"]?.ToString();
                }
            }
        }

        public static string GetApiKey()
        {
            return ApiKey;
        }
    }
}
