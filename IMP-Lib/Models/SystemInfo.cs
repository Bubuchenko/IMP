using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace IMP_Lib.Models
{
    [DataContract]
    public class SystemInfo
    {
        [DataMember]
        public int SystemInfoId { get; set; }
        [DataMember]
        public string MachineName { get; set; }
        [DataMember]
        public string MachineSID { get; set; }
        [DataMember]
        public string SystemType { get; set; }
        [DataMember]
        public string SystemLocale { get; set; }
        [DataMember]
        public string GPU { get; set; }
        [DataMember]
        public string CPU { get; set; }
        [DataMember]
        public int RAM { get; set; }
        [DataMember]
        public bool X64_Bit { get; set; }
        [DataMember]
        public string DefaultBrowser { get; set; }
        [DataMember]
        public string OperatingSystem { get; set; }


        [Required]
        [DataMember]
        public virtual Client Client { get; set; }

        [DataMember]
        public virtual ICollection<AntiVirus> AntiVirus { get; set; }
        [DataMember]
        public virtual ICollection<InputDevice> InputDevices { get; set; }
        [DataMember]
        public virtual ICollection<Monitor> Monitors { get; set; }
        [DataMember]
        public virtual ICollection<DiskDrive> Drives { get; set; }

        [NotMapped]
        public string FriendlySystemType
        {
            get
            {
                return X64_Bit == true ? "64-bit" : "32-bit";
            }
        }
    }
}
