using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Lib.Models
{
    [DataContract]
    public class AntiVirus
    {
        [DataMember]
        public int AntiVirusId { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public string PathToFile { get; set; }
        [DataMember]
        public string ProductState { get; set; }

        [Required]
        [DataMember]
        public virtual SystemInfo SystemInfo { get; set; }
    }
}
