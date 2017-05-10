using IMP_Data.Models;
using IMP_Data.Repositories;
using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IMP_Service.Hubs
{
    public class ClientControlHub : IMPHub
    {
        public async Task<string> UploadFile(string ClientID, string filePath)
        {
            filePath = @"C:\IMP\Test.txt";
            return await WCFServer.Connections[ClientID].Upload(filePath, Context.ConnectionId, ClientID);
        }

    }
}