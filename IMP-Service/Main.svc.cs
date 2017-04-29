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
using IMP_Data.Repositories;

namespace IMP_Service
{
    public class MainService : IServerContract
    {
        /// <summary>
        /// Called by the client when a client connects to the server.
        /// </summary>
        /// <param name="fingerprint">The clients fingerprint to determine their identity</param>
        /// <returns>A result that specifies whether the connection succeeded</returns>
        public async Task<ConnectResult> Connect(string fingerprint)
        {
            if (!await ClientRepository.IsClientRegistered(fingerprint))
                return ConnectResult.NotRegistered;

            if (!await ServerClientHandler.AcceptClientConnection(await ClientRepository.GetClient(fingerprint)))
                return ConnectResult.AlreadyConnected;

            return ConnectResult.Successful;
        }

        /// <summary>
        /// Registers a client to the database
        /// </summary>
        /// <param name="client">The client information to be stored in the database</param>
        /// <returns>A result that specifies whether the registration succeeded</returns>
        public async Task<RegisterResult> Register(Client client)
        {
            if (await ClientRepository.IsClientRegistered(client.Fingerprint))
                return RegisterResult.AlreadyExists;

            if (!await ClientRepository.RegisterClient(client))
                return RegisterResult.Failed;

            return RegisterResult.Successful;
        }
    }
}
