using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace IMP_Service.Hubs
{
    public class CmdHub : IMPHub
    {
        public async Task<string> CmdCommand(string ClientID, string Command)
        {
            return await WCFServer.Connections[ClientID].CMDCommand(Command);
        }
    }
}