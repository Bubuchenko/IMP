using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Lib.Models
{
    [DataContract]
    public class Client
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string Fingerprint { get; set; }
        public ConnectionInfo ConnectionInfo { get; set; }
        public virtual SystemInfo SystemInfo { get; set; }
        public virtual PersonalInformation PersonalInformation { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
