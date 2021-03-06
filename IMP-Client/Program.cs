﻿using IMP_Lib;
using IMP_Lib.Contracts;
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

        private static DuplexChannelFactory<IServerContract> DefaultChannelFactory { get; set; }
        public static ChannelFactory<IFileTransferContract> FileChannelFactory { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            DefaultChannelFactory = new DuplexChannelFactory<IServerContract>(new ClientServiceProvider(), "Main");
            FileChannelFactory = new ChannelFactory<IFileTransferContract>("FileTransfer");
            defaultChannel = DefaultChannelFactory.CreateChannel();

            Connect().Wait();
            Console.ReadLine();
        }

        static async Task Connect()
        {
            switch (await defaultChannel.Connect(SystemInspector.MachineName, SystemInspector.MachineSID))
            {
                case ConnectResult.Successful:
                    Console.WriteLine("Connected!");
                    break;

                case ConnectResult.NotRegistered:
                    await Register();
                    break;

                case ConnectResult.AlreadyConnected:
                    Console.WriteLine("Already connected, sadchamp");
                    //Do nothing? Kick from server and retry?
                    break;

                default:
                    //Do some logging?
                    break;
            }
        }

        static async Task Register()
        {
            string Username = Environment.UserName;
            SystemInfo sysInfo = await SystemInspector.GetSystemInfo();

            switch (await defaultChannel.Register(Username, sysInfo))
            {
                case RegisterResult.Successful:
                    Console.WriteLine("Registered! Connecting...");
                    await Connect();
                    break;
                case RegisterResult.AlreadyExists:
                    Console.WriteLine("Already registered. Connecting...");
                    break;
                case RegisterResult.Failed:
                    //Log?
                    break;
            }
        }
    }
}
