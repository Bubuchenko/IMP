using IMP_Lib;
using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using IMP_Lib.Enums;
using System.Threading.Tasks;
using IMP_Data.Repositories;
using System.ServiceModel.Channels;
using System.Net;

namespace IMP_Service
{
    public class MainService : IServerContract
    {
        public MainService()
        {
            OperationContext.Current.Channel.Faulted += new EventHandler(OnClientDisconnect);
            OperationContext.Current.Channel.Closed += new EventHandler(OnClientDisconnect);
        }

        /// <summary>
        /// Event handler called when a client disconnects from the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClientDisconnect(object sender, EventArgs e)
        {
            string SessionID = ((IContextChannel)sender).SessionId;
            Task.Run(() => ServerClientHandler.CloseClientConnection(SessionID));
        }

        /// <summary>
        /// Called by the client when a client connects to the server.
        /// </summary>
        /// <param name="ClientId">The clients ClientId to determine their identity</param>
        /// <returns>A result that specifies whether the connection succeeded</returns>
        public async Task<ConnectResult> Connect(string MachineName, string MachineSID)
        {
            string ClientId = ServerClientHandler.GenerateClientID(MachineName, MachineSID);
            string SessionID = OperationContext.Current.SessionId;

            if (!await ClientRepository.IsClientRegistered(ClientId))
                return ConnectResult.NotRegistered;

            Client client = await ClientRepository.GetClient(ClientId);

            if (!await ServerClientHandler.AcceptClientConnection(client, OperationContext.Current))
                return ConnectResult.AlreadyConnected;

            return ConnectResult.Successful;
        }

        /// <summary>
        /// Registers a client to the database
        /// </summary>
        /// <param name="client">The client information to be stored in the database</param>
        /// <returns>A result that specifies whether the registration succeeded</returns>
        public async Task<RegisterResult> Register(Client client)
        {
            if (await ClientRepository.IsClientRegistered(client.ClientId))
                return RegisterResult.AlreadyExists;

            client.ClientId = ServerClientHandler.GenerateClientID(client.SystemInfo);

            if (!await ClientRepository.RegisterClient(client))
                return RegisterResult.Failed;

            return RegisterResult.Successful;
        }
    }
}
