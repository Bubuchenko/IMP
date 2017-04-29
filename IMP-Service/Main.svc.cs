using IMP_Lib;
using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using IMP_Lib.Enums;
using System.Threading.Tasks;

namespace IMP_Service
{
    public class MainService : IServerContract
    {
        public Task<StatusResults> Connect(string fingerprint)
        {
            throw new NotImplementedException();
        }
    }
}
