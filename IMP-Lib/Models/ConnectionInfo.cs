using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Lib.Models
{
    public class ConnectionInfo
    {
        public string SessionID { get; set; }
        public DateTime ConnectedSince { get; set; }
        public IPAddress IPAddress { get; set; }
    }
}
