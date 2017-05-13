using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using IMP_Lib.Models;
using IMP_Data.Extensions;
using IMP_Data;
using IMP_Lib.Enums;
using System.Configuration;
using System.IO;

namespace IMP_Service.Hubs
{
    public class FileManageHub : IMPHub
    {
        public async Task<string> GetDirectoryContent(string ClientID, string Path)
        {
            List<WindowsItem> content = await WCFServer.Connections[ClientID].GetDirectoryContents(Path);
            return content.Serialize<List<WindowsItem>>();
        }
        public async Task UploadFile(string ClientID, string Source)
        {
            FileTransfer fileTransfer = new FileTransfer
            {
                ConnectionID = Context.ConnectionId,
                ClientID = ClientID,
                Target = Source,
                TransferType = FileTransferType.UPLOAD,
            };

            await WCFServer.Connections[ClientID].Upload(fileTransfer);
        }

        public async Task DownloadFile(string ClientID, string Destination)
        {
            FileTransfer fileTransfer = new FileTransfer
            {
                ConnectionID = Context.ConnectionId,
                ClientID = ClientID,
                Target = Destination,
                TransferType = FileTransferType.UPLOAD,
            };

            await WCFServer.Connections[ClientID].Download(fileTransfer);
        }

        public async Task<string> Delete(string ClientID, string Path)
        {
            return await WCFServer.Connections[ClientID].Delete(Path);
        }

        public async Task<string> Open(string ClientID, string Path)
        {
            return await WCFServer.Connections[ClientID].Open(Path);
        }
    }
}