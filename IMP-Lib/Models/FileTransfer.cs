using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Lib.Models
{
    [MessageContract]
    public class FileTransfer
    {
        public FileTransfer()
        {
            StartTime = DateTime.Now;
        }

        public string FileName
        {
            get
            {
                return Path.GetFileName(FilePath);
            }
        }

        [MessageHeader]
        public string ClientID { get; set; }
        [MessageHeader]
        public string FilePath { get; set; }
        [MessageHeader]
        public long FileSize { get; set; }
        [MessageHeader]
        public string ConnectionID { get; set; }
        [MessageHeader]
        public DateTime StartTime { get; private set; }


        [MessageBodyMember]
        public Stream Data { get; set; }
    }
}
