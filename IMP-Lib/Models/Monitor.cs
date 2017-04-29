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
        public int MonitorId { get; set; }
        public string Type { get; set; }
        public string Resolution { get; set; }
        public bool IsPrimary { get; set; }

        public virtual SystemInfo SystemInfo { get; set; }

    }
}
