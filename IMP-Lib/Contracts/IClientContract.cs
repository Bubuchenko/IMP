﻿using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Lib.Contracts
{
    [ServiceContract]
    public interface IClientContract
    {
        [OperationContract]
        Task<string> CMDCommand(string command);
        [OperationContract]
        Task<string> Upload(string filePath, string ConnectionID, string ClientID);
    }
}
