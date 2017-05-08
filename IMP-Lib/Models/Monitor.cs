using IMP_Lib.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [DataMember]
        public int Width
        {
            get
            {
                return int.Parse(Resolution.Split('x')[0]);
            }
        }

        [DataMember]
        public int Height
        {
            get
            {
                return int.Parse(Resolution.Split('x')[1]);
            }
        }

        [DataMember]
        public int Size
        {
            get
            {
                return Width * Height;
            }
        }
    }
}
