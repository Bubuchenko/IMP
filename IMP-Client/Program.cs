using IMP_Lib;
using IMP_Lib.Enums;
using IMP_Lib.Models;
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

        static IServerContract defaultChannel;
        static void Main(string[] args)
        {
            var defaultChannelFactory = new DuplexChannelFactory<IServerContract>(new ClientServiceProvider());
            defaultChannel = defaultChannelFactory.CreateChannel();
            StartClient().Wait();            
        }

        static async Task StartClient()
        {

            switch(await defaultChannel.Connect("ABC"))
            {
                case ConnectResult.Successful:
                    //Do nothing?
                    break;

                case ConnectResult.NotRegistered:
                    RegisterClient();
                    break;

                case ConnectResult.AlreadyConnected:
                    //Do nothing? Kick from server and retry?
                    break;

                default:
                    //Do some logging?
                    break;
            }

            if (await defaultChannel.Connect("ABC") == IMP_Lib.Enums.ConnectResult.NotRegistered)
            {

                Client client = new Client()
                {
                    Fingerprint = "ABC"
                };

                if (await defaultChannel.Register(client) == IMP_Lib.Enums.RegisterResult.Successful)
                {
                    Console.WriteLine("Registered!");
                }
            }
        }

        static async void RegisterClient()
        {
            Client client = new Client()
            {
                SystemInfo = await SystemInspector.GetSystemInfo()
            };

            if (await defaultChannel.Register(client) == RegisterResult.Successful)
                Console.WriteLine("Registered!");
        }
    }
}
