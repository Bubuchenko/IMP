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
        public int DiskDriveId { get; set; }
        public string Name { get; set; }
        public string VolumeLabel { get; set; }
        public string FileSystem { get; set; }
        public DriveType DriveType { get; set; }
        public float TotalSpace { get; set; }
        public float AvailableFreeSpace { get; set; }


        public virtual SystemInfo SystemInfo { get; set; }
    }
}
