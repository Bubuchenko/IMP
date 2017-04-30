using IMP_Data.Repositories;
using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Web;

namespace IMP_Service
{
    public static class ServerClientHandler
    {
        public static async Task<bool> AcceptClientConnection(Client client, OperationContext connectionContext)
        {
            //Client already connected
            if(ServerState.ConnectedClients.Where(f => f.ClientId == client.ClientId).Any())
            {
                return false;
            }

            //Set connection info
            client.ConnectionInfo = new ConnectionInfo
            {
                SessionID = connectionContext.SessionId,
                IPAddress = IPAddress.Parse((OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty).Address),
                ConnectedSince = DateTime.Now
            };

            await ClientRepository.SetLastSeenDate(client.ClientId, client.ConnectionInfo.ConnectedSince);

            ServerState.ConnectedClients.Add(client);

            return true;
        }

        public static async Task CloseClientConnection(string SessionID)
        {
            Client disconnectedClient = ServerState.ConnectedClients.FirstOrDefault(f => f.ConnectionInfo.SessionID == SessionID);

            ServerState.ConnectedClients.RemoveAll(f => f.ClientId == disconnectedClient.ClientId);

            await ClientRepository.SetLastSeenDate(disconnectedClient.ClientId, DateTime.Now);
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