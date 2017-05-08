using IMP_Data.Models;
using IMP_Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IMP_Service.Hubs
{
    public class ClientControlHub : IMPHub
    {
        public string Test(string ClientID)
        {
            WCFServer.Connections[ClientID].Test();
            return "OK";
        }
    }
}