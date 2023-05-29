using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Chat.Language.Model;
using Newtonsoft.Json;

namespace Chat.Language.Languages
{
    public class fa_IR
    {
        public static List<Resource> GetList()
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            var data = File.ReadAllText(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Languages", "fa-IR.json")
                , Encoding.UTF8);
            return JsonConvert.DeserializeObject<List<Resource>>(data, jsonSerializerSettings);
        }
    }
}