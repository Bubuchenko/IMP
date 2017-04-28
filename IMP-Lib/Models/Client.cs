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
        public IPAddress IPAddress { get; set; }
        public SystemInfo SystemInfo { get; set; }
        public PersonalInformation PersonalInformation { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
