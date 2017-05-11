using IMP_Data.Models;
using IMP_Data.Repositories;
using IMP_Lib.Enums;
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

        public async Task UploadFile(string ClientID, string Source, string Destination)
        {
            FileTransfer fileTransfer = new FileTransfer
            {
                ConnectionID = Context.ConnectionId,
                ClientID = ClientID,
                Source = Source,
                Destination = Destination,
                TransferType = FileTransferType.UPLOAD,
            };

            await WCFServer.Connections[ClientID].Upload(fileTransfer);
        }

        public async Task DownloadFile(string ClientID, string Source, string Destination)
        {
            FileTransfer fileTransfer = new FileTransfer
            {
                ConnectionID = Context.ConnectionId,
                ClientID = ClientID,
                Source = Source,
                Destination = Destination,
                TransferType = FileTransferType.UPLOAD,
            };

            await WCFServer.Connections[ClientID].Download(fileTransfer);
        }

    }
}