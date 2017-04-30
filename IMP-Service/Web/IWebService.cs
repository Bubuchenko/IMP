using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace IMP_Service
{
    [ServiceContract]
    public interface IWebService
    {
        [OperationContract]
        List<Client> GetOnlineClients();
    }
}
