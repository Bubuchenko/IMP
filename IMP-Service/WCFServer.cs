using IMP_Lib.Contracts;
using IMP_Service.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMP_Service
{
    public static class WCFServer
    {
        /// <summary>
        /// Holds all active connections
        /// </summary>
        public static Dictionary<string, IClientContract> Connections = new Dictionary<string, IClientContract>();

        public static List<IHubContext> Hubs { get; set; }

        static WCFServer()
        {
            Hubs = new List<IHubContext>
            {
                GlobalHost.ConnectionManager.GetHubContext<IMPHub>(),
                GlobalHost.ConnectionManager.GetHubContext<DashboardHub>(),
                GlobalHost.ConnectionManager.GetHubContext<ClientControlHub>(),
                GlobalHost.ConnectionManager.GetHubContext<CmdHub>()
            };
        }
    }
}