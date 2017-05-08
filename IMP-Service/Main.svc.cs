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
using Microsoft.Owin.Hosting;
using System.Configuration;
using Microsoft.AspNet.SignalR;
using IMP_Service.Hubs;
using IMP_Data.Models;

namespace IMP_Service
{
    public class MainService : IServerContract
    {
        //Keep a collection of all hubs
        static List<IHubContext> Hubs;
        bool IsDisconnected = false;

        static MainService()
        {
            Hubs = new List<IHubContext>
            {
                GlobalHost.ConnectionManager.GetHubContext<IMPHub>(),
                GlobalHost.ConnectionManager.GetHubContext<DashboardHub>(),
                GlobalHost.ConnectionManager.GetHubContext<ClientControlHub>()
            };
        }

        public MainService()
        {
            OperationContext.Current.Channel.Faulted += new EventHandler(OnClientDisconnect);
            OperationContext.Current.Channel.Closed += new EventHandler(OnClientDisconnect);
        }

        private async void OnClientDisconnect(object sender, EventArgs e)
        {
            if (IsDisconnected)
                return;
            IsDisconnected = true;

            string SessionID = ((IContextChannel)sender).SessionId;
            await ServerClientHandler.CloseClientConnection(SessionID);

            Client client = await SessionRepository.GetClientBySessionID(SessionID);

            client.IsOnline = false;
            Hubs.ForEach(f => f.Clients.All.clientDisconnected(client));
        }

        public async Task<ConnectResult> Connect(string MachineName, string MachineSID)
        {
            string ClientId = ServerClientHandler.GenerateClientID(MachineName, MachineSID);
            string SessionID = OperationContext.Current.SessionId;


            

            if (!await ClientRepository.IsClientRegistered(ClientId))
                return ConnectResult.NotRegistered;

            Client client = await ClientRepository.GetClient(ClientId);

            if (!await ServerClientHandler.AcceptClientConnection(client, OperationContext.Current))
                return ConnectResult.AlreadyConnected;

            //Notify all hubs
            client.IsOnline = true;
            Hubs.ForEach(f => f.Clients.All.clientConnected(client));

            return ConnectResult.Successful;
        }

        public async Task<RegisterResult> Register(string Username, SystemInfo SystemInformation)
        {
            string ClientId = ServerClientHandler.GenerateClientID(SystemInformation);

            if (await ClientRepository.IsClientRegistered(ClientId))
                return RegisterResult.AlreadyExists;

            string IPaddress = (OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty).Address;

            Client client = new Client()
            {
                Username = Username,
                ClientId = ServerClientHandler.GenerateClientID(SystemInformation),
                PersonalInformation = await GeoTracker.GetPersonalInformationByIPAddress(IPAddress.Parse(IPaddress)),
                IPAddress = IPaddress,
                SystemInfo = SystemInformation
            };
        

            if (!await ClientRepository.RegisterClient(client))
                return RegisterResult.Failed;

            return RegisterResult.Successful;
        }
    }
}
