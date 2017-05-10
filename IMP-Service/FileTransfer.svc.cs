using IMP_Lib.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using IMP_Lib.Models;
using System.Threading.Tasks;
using System.IO;
using IMP_Lib;
using IMP_Data.Repositories;
using Microsoft.AspNet.SignalR;
using IMP_Service.Hubs;

namespace IMP_Service
{
    public class FileTransferService : IFileTransferContract
    {
        public void Download()
        {
            throw new NotImplementedException();
        }

        public async Task Upload(FileTransfer file)
        {
            Client client = await ClientRepository.GetClient(file.ClientID);

            GlobalHost.ConnectionManager.GetHubContext<ClientControlHub>().Clients.Client(file.ConnectionID).newFileUpload(string.Format("{0} has started uploading {1}", client.Username, file.FileName));

            using (Stream output = File.Create(@"C:\Output\" + file.FileName))
            {
                byte[] buffer = new byte[4 * 1024];
                int length;


                while ((length = await file.Data.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await output.WriteAsync(buffer, 0, length);

                    GlobalHost.ConnectionManager.GetHubContext<ClientControlHub>().Clients.Client(file.ConnectionID).fileProgessUpdate(file.FileName, length);
                }
            }
        }
    }
}
