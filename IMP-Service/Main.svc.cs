using IMP_Lib;
using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace IMP_Service
{
    public class MainService : IServerContract
    {
        public bool Connect(Client client)
        {
            ServerState.ConnectedClients.Add(client);

            return true;
        }
    }
}
