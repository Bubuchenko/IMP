using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using IMP_Lib.Models;

namespace IMP_Service
{
    public class WebService : IWebService
    {
        public List<Client> GetOnlineClients()
        {
            return ServerState.ConnectedClients;
        }
    }
}
