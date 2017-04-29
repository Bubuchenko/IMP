using IMP_Data.Repositories;
using IMP_Lib;
using IMP_Lib.Contracts;
using IMP_Lib.Enums;
using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web;

namespace IMP_Service
{
    public class ServerServiceProvider : IServerContract
    {
        /// <summary>
        /// Called by the client when a client connects to the server.
        /// </summary>
        /// <param name="fingerprint">The clients fingerprint to determine their identity</param>
        /// <returns>A result that specifies whether the Connection was valid</returns>
        public async Task<StatusResults> Connect(string fingerprint)
        {
            if (!await ClientRepository.IsClientRegistered(fingerprint))
                return StatusResults.NotRegistered;

            if(!await ServerClientHandler.AcceptClientConnection(await ClientRepository.GetClient(fingerprint)))
                return StatusResults.Unknown;

            return StatusResults.Successful;
        }
    }
}