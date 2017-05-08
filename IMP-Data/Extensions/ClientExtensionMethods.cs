using IMP_Data.Models;
using IMP_Data.Repositories;
using IMP_Lib.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IMP_Data
{
    public static class ClientExtensionMethods
    {
        static JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
        //Sets the IsOnline property for all clients in a List<Client>
        public static async Task<List<Client>> SetOnlineStatuses(this List<Client> clients)
        {
            List<Session> ActiveSessions = await SessionRepository.GetActiveSessions();
            clients.ForEach(f => f.IsOnline = ActiveSessions.Any(x => x.ClientID == f.ClientId));
            return clients;
        }

        static ClientExtensionMethods()
        {
            jsonSettings.Converters.Add(new StringEnumConverter());
            jsonSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }



        public static string Serialize(this Client client)
        {
            return JsonConvert.SerializeObject(client, jsonSettings);
        }

        public static string Serialize(this List<Client> clients)
        {
            return JsonConvert.SerializeObject(clients, jsonSettings);
        }
    }
}