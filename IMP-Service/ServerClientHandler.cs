using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IMP_Service
{
    public static class ServerClientHandler
    {
        public static async Task<bool> AcceptClientConnection(Client client)
        {
            ServerState.ConnectedClients.Add(client);

            return true;
        }
    }
}