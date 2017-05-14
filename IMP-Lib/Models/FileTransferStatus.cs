using IMP_Lib.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Lib.Models
{
    [MessageContract]
    public class FileTransferStatus
    {
        [MessageHeader]
        public string FileTransferID { get; set; }
        [MessageHeader]
        public string FileName { get; set; }
        [MessageHeader]
        public string ClientID { get; set; }
        [MessageHeader]
        public DateTime StartTime { get; set; }
        [MessageHeader]
        public string ConnectionID { get; set; }
        [Range(0, 100)]
        public double Progress { get; set; }
        [MessageHeader]
        public FileTransferType TransferType { get; set; }

        public string FriendlyProgress
        {
            get
            {
                return Math.Round(Progress, 0).ToString() + "%";
            } set { }
        }
    }
}
