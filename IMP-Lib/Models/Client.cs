using IMP_Lib.Models;
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
    public class Client
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string ClientId { get; set; }
        public DateTime CreationDate { get; set; }
        public string IPAddress { get; set; }
        public bool IsOnline { get; set; }
        public bool IsFavorite { get; set; }
        public virtual SystemInfo SystemInfo { get; set; }
        public virtual PersonalInformation PersonalInformation { get; set; }

    }
}
