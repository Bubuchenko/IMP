using IMP_Lib.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMP_Service.Hubs
{
    public static class HubEventBindings
    {
        static HubEventBindings()
        {
            MainService.OnClientConnect += MainService_OnClientConnect;
            MainService.OnClientDisconnect += MainService_OnClientDisconnect;
        }

        private static void MainService_OnClientConnect(Client client)
        {
            List<IHubContext> Hubs = new List<IHubContext>
            {
                GlobalHost.ConnectionManager.GetHubContext<IMPHub>(),
                GlobalHost.ConnectionManager.GetHubContext<DashboardHub>(),
                GlobalHost.ConnectionManager.GetHubContext<ClientControlHub>(),
                GlobalHost.ConnectionManager.GetHubContext<CmdHub>()
            };

            Hubs.ForEach(f => f.Clients.All.clientConnected(client));
        }

        private static void MainService_OnClientDisconnect(Client client)
        {
            List<IHubContext> Hubs = new List<IHubContext>
            {
                GlobalHost.ConnectionManager.GetHubContext<IMPHub>(),
                GlobalHost.ConnectionManager.GetHubContext<DashboardHub>(),
                GlobalHost.ConnectionManager.GetHubContext<ClientControlHub>(),
                GlobalHost.ConnectionManager.GetHubContext<CmdHub>()
            };

            Hubs.ForEach(f => f.Clients.All.clientConnected(client));
        }
    }
}