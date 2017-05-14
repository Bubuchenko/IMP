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

        public delegate void ClientConnected(Client client);
        public static event ClientConnected OnClientConnect;

        public delegate void ClientDisconnected(Client client);
        public static event ClientDisconnected OnClientDisconnect;


        bool IsDisconnected = false;
        public MainService()
        {
            OperationContext.Current.Channel.Faulted += new EventHandler(OnClientDisconnected);
            OperationContext.Current.Channel.Closed += new EventHandler(OnClientDisconnected);
        }

        private async void OnClientDisconnected(object sender, EventArgs e)
        {
            //Faulted & Closed are often called twice, so make sure our event only gets called once
            if (IsDisconnected)
                return;
            IsDisconnected = true;

            string SessionID = ((IContextChannel)sender).SessionId;
            Client client = await SessionRepository.GetClientBySessionID(SessionID);
            await ServerClientHandler.CloseClientConnection(client.ClientId, SessionID);

            OnClientDisconnect.Invoke(client);
        }

        public async Task<ConnectResult> Connect(string MachineName, string MachineSID)
        {
            string ClientId = ServerClientHandler.GenerateClientID(MachineName, MachineSID);
            string SessionID = OperationContext.Current.SessionId;

            //Client not registered yet
            if (!await ClientRepository.IsClientRegistered(ClientId))
                return ConnectResult.NotRegistered;

            //Client already connected
            if (await SessionRepository.ClientHasActiveSession(ClientId))
                return ConnectResult.AlreadyConnected;
            
            await ServerClientHandler.AcceptClientConnection(ClientId, OperationContext.Current);

            Client client = await ClientRepository.GetClient(ClientId);

            OnClientConnect.Invoke(client);
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
