using IMP_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var defaultChannelFactory = new DuplexChannelFactory<IServerContract>(new ClientServiceProvider());
            IServerContract defaultChannel = defaultChannelFactory.CreateChannel();
        }
    }
}
