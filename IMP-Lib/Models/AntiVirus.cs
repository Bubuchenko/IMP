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
        public int AntiVirusId { get; set; }
        public string ProductName { get; set; }
        public string PathToFile { get; set; }
        public string ProductState { get; set; }

        [Required]
        public virtual SystemInfo SystemInfo { get; set; }
    }
}
