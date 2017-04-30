using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Lib.Models
{
    [DataContract(IsReference = true)]
    public class Client
    {
        public int Id { get; set; }
        [DataMember]
        public string Username { get; set; }
        public string ClientId { get; set; }
        public DateTime CreationDate { get; set; }

        [NotMapped]
        public ConnectionInfo ConnectionInfo { get; set; }

        [DataMember]
        public virtual SystemInfo SystemInfo { get; set; }
        public virtual PersonalInformation PersonalInformation { get; set; }
    }
}
