using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using IMP_Lib.Models;
using IMP_Data.Models;
using IMP_Data;

namespace IMP_Service.Hubs
{
    public class IMPHub : Hub
    {
        public void ClientConnected(Client client)
        {
            client.IsOnline = true;
            Clients.All.clientConnected(client);
        }

        public void ClientDisconnected(Client client)
        {
            client.IsOnline = false;
            Clients.All.clientDisconnected(client);
        }
    }
}