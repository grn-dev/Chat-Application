using System;
using System.Collections.Generic;
using System.IO;
using Chat.Language.Model;
using Newtonsoft.Json;

namespace Chat.Language.Languages
{
    public static class en_US
    {
        public static List<Resource> GetList()
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;

            var data = File.ReadAllText(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Languages", "en-US.json")
            );
            return JsonConvert.DeserializeObject<List<Resource>>(data, jsonSerializerSettings);
        }
    }
}