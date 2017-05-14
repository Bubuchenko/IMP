using IMP_Lib.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Lib.Models
{
    [MessageContract]
    public class FileTransfer :IDisposable
    {
        public FileTransfer()
        {
            //Stream must be initialized

            Data = Stream.Null;
            FileTransferID = Guid.NewGuid().ToString();
            StartTime = DateTime.UtcNow;
        }


        public string FileName
        {
            get
            {
                return Path.GetFileName(Target);
            }
            private set { }
        }

        [MessageHeader]
        public string FileTransferID { get; private set; }
        [MessageHeader]
        public string ClientID { get; set; }
        [MessageHeader]
        public string ConnectionID { get; set; }
        [MessageHeader]
        public string Target { get; set; }
        [MessageHeader]
        public long FileSize { get; private set; }
        [MessageHeader]
        public DateTime StartTime { get; set; }
        [MessageHeader]
        public DateTime EndTime { get; private set; }
        [MessageHeader]
        public FileTransferType TransferType { get; set; }
        [MessageHeader]
        public double Progress { get; set; }
        //Report progress every 5%
        [MessageHeader]
        public int ProgressPercentReport = 5;
        [MessageBodyMember]
        private Stream Data { get; set; }

        public Stream GetFileStream()
        {
            return Data;
        }

        public void SetFileStream(Stream stream)
        {
            Data = stream;
            FileSize = stream.Length;
        }

        public FileTransferStatus GetStatus()
        {
            return new FileTransferStatus
            {
                FileName = this.FileName,
                ClientID = this.ClientID,
                ConnectionID = this.ConnectionID,
                FileTransferID = this.FileTransferID,
                Progress = this.Progress,
                StartTime = this.StartTime,
                TransferType = this.TransferType
            };
        }

        public void Dispose()
        {
            Data.Dispose();
        }
    }
}
