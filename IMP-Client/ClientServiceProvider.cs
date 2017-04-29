using IMP_Lib.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMP_Lib.Models;
using System.ServiceModel;

namespace IMP_Client
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant, UseSynchronizationContext = false)]
    class ClientServiceProvider : IClientContract
    {
        public void Test()
        {
            throw new NotImplementedException();
        }
    }
}
