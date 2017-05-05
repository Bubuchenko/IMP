using IMP_Lib.Contracts;
using IMP_Lib.Enums;
using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Lib
{
    [ServiceContract(CallbackContract = typeof(IClientContract))]
    public interface IServerContract
    {
        [OperationContract]
        Task<ConnectResult> Connect(string MachineName, string MachineSID);
        [OperationContract]
        Task<RegisterResult> Register(string Username, SystemInfo SystemInformation);
    }

}
