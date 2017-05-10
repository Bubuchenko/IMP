using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Lib.Models
{
    [DataContract]
    public class DiskDrive
    {
        [DataMember]
        public int DiskDriveId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string VolumeLabel { get; set; }
        [DataMember]
        public string FileSystem { get; set; }
        [DataMember]
        public DriveType DriveType { get; set; }
        [DataMember]
        public float TotalSpace { get; set; }
        [DataMember]
        public float AvailableFreeSpace { get; set; }
        [DataMember]
        public virtual SystemInfo SystemInfo { get; set; }

        [DataMember]
        public float PercentFreeSpace
        {
            get
            {
                return (AvailableFreeSpace / TotalSpace) * 100;
            }
            protected set { }
        }
    }
}
