using IMP_Data.Models;
using IMP_Data.Repositories;
using IMP_Lib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IMP_Data
{
    public static class ClientExtensionMethods
    {
        //Sets the IsOnline property for all clients in a List<Client>
        public static async Task<List<Client>> SetOnlineStatuses(this List<Client> clients)
        {
            List<Session> ActiveSessions = await SessionRepository.GetActiveSessions();
            clients.ForEach(f => f.IsOnline = ActiveSessions.Any(x => x.ClientID == f.ClientId));
            return clients;
        }

        public static string Serialize(this Client client)
        {
            return JsonConvert.SerializeObject(client,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    });
        }

        public static string Serialize(this List<Client> clients)
        {
            return JsonConvert.SerializeObject(clients,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    });
        }
    }
}