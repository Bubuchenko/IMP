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
        public int SystemInfoId { get; set; }
        public string MachineName { get; set; }
        public string SystemType { get; set; }
        public string SystemLocale { get; set; }
        public int MonitorsCount { get; set; }
        public bool Webcam { get; set; }
        public string GPU { get; set; }
        public string CPU { get; set; }
        public int RAM { get; set; }
        public bool X64_Bit { get; set; }
        public string Harddrives { get; set; }
        public string AntiVirus { get; set; }
        public string DefaultBrowser { get; set; }
        public string CPUID { get; set; }
        public string DriveID { get; set; }
        public string OperatingSystem { get; set; }

        [Required]
        public virtual Client Client { get; set; }
    }
}
