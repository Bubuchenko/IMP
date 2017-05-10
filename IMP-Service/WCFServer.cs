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


    }
}