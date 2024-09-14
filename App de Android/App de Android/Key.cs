using System;
using System.IO;
using Newtonsoft.Json.Linq;


namespace App_de_Android
{


    public static class Key
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        private static readonly string ApiKey = LoadApiKey();

        private static string LoadApiKey()
        {
            // Read the JSON file and parse the API key
            var jsonString = File.ReadAllText(FilePath);
            var jsonObject = JObject.Parse(jsonString);
            return jsonObject["api_key"]?.ToString();
        }

        public static string GetApiKey() => ApiKey;
    }
}
