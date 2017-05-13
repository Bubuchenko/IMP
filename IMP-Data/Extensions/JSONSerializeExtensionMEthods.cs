using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Data
{
    public static class JSONSerializeExtensionMethods
    {
        static JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
        //Sets the IsOnline property for all clients in a List<Client>

        static JSONSerializeExtensionMethods()
        {
            jsonSettings.Converters.Add(new StringEnumConverter());
            jsonSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        public static string Serialize<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj, jsonSettings);
        }
    }
}
