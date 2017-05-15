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
        [MessageHeader]
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

        public string ETA
        {
            get
            {
                //Prevent our formula from dividing by zero
                if (Progress == 0)
                    Progress = 0.0001;

                return Convert.ToInt32((DateTime.UtcNow - StartTime).TotalSeconds / Convert.ToDouble(Progress / (double)100) - (DateTime.UtcNow - StartTime).TotalSeconds).ToString();
            }
            set { }
        }
    }
}
