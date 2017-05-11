using IMP_Data.Models;
using IMP_Data.Repositories;
using IMP_Lib.Contracts;
using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
        public static async Task AcceptClientConnection(string ClientId, OperationContext connectionContext)
        {
            //Create session
            Session session = new Session
            {
                SessionID = connectionContext.SessionId,
                SessionStart = DateTime.Now,
                ClientID = ClientId
            };

            WCFServer.Connections.Add(ClientId, connectionContext.GetCallbackChannel<IClientContract>());
            await SessionRepository.CreateSession(session);
        }

        public static async Task CloseClientConnection(string SessionID)
        {
            await SessionRepository.EndSession(SessionID);
            Client Client = await SessionRepository.GetClientBySessionID(SessionID);
            WCFServer.Connections.Remove(Client.ClientId);
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