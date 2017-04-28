using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMP_Service
{
    public static class ServerState
    {
        public static DateTime ServerOnlineSince { get; set; }
        public static List<Client> ConnectedClients = new List<Client>();
    }
}