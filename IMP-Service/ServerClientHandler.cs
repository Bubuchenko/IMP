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

        public static string GenerateClientID(SystemInfo systemInfo)
        {
            return string.Format("{0}-{1}", systemInfo.MachineName, systemInfo.MachineSID);
        }
        public static string GenerateClientID(string MachineName, string MachineSID)
        {
            return string.Format("{0}-{1}", MachineName, MachineSID);
        }
    }
}