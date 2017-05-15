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
using System.Configuration;

namespace IMP_Service
{
    public class FileTransferService : IFileTransferContract
    {
        public delegate void FileProgressUpdate(FileTransferStatus fileTransferStatus);
        public static event FileProgressUpdate OnFileProgressUpdate;

        public delegate void FileTransferStarted(FileTransferStatus fileTransferStatus);
        public static event FileTransferStarted OnFileTransferStarted;

        public delegate void FileTransferCompleted(FileTransferStatus fileTransferStatus);
        public static event FileTransferCompleted OnFileTransferCompleted;

        public Task<FileTransfer> Download(FileTransfer fileTransfer)
        {
            string fileSource = Path.Combine(ConfigurationManager.AppSettings["DownloadFileDirectory"], Path.GetFileName(fileTransfer.Target));
            OnFileTransferStarted.Invoke(fileTransfer.GetStatus());
            Stream sourceStream = File.OpenRead(fileSource);
            fileTransfer.SetFileStream(sourceStream);
            return Task.FromResult(fileTransfer);
        }

        public void ReportFileDownloadStatus(FileTransferStatus fileTransferStatus)
        {
            OnFileProgressUpdate.Invoke(fileTransferStatus);
        }

        public void ReportFileDownloadCompleted(FileTransferStatus fileTransferStatus)
        {
            OnFileTransferCompleted.Invoke(fileTransferStatus);
        }

        public async Task Upload(FileTransfer fileTransfer)
        {
            string fileDestination = Path.Combine(ConfigurationManager.AppSettings["UploadFileDirectory"], Path.GetFileName(fileTransfer.Target));

            OnFileTransferStarted.Invoke(fileTransfer.GetStatus());
            using (Stream output = File.Create(fileDestination))
            {
                byte[] buffer = new byte[4 * 1024];
                int length;
                double progressCheck = 0;

                while ((length = await fileTransfer.GetFileStream().ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await output.WriteAsync(buffer, 0, length);
                    fileTransfer.Progress = Math.Round(((double)output.Position / (double)fileTransfer.FileSize) * (double)100, 0);

                    if (fileTransfer.Progress > progressCheck)
                    {
                        OnFileProgressUpdate.Invoke(fileTransfer.GetStatus());
                        progressCheck += fileTransfer.ProgressPercentReport;
                    }
                }
            }

            OnFileTransferCompleted.Invoke(fileTransfer.GetStatus());
        }
    }
}
