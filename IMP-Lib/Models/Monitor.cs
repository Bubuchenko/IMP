using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Lib.Models
{
    [DataContract]
    public class Monitor
    {
        [DataMember]
        public int MonitorId { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Resolution { get; set; }
        [DataMember]
        public bool IsPrimary { get; set; }
        [DataMember]
        public virtual SystemInfo SystemInfo { get; set; }

    }
}
