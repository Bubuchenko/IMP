using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Lib.Models
{
    [DataContract]
    public class PersonalInformation
    {
        public int PersonalInformationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string City { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public Gender Gender { get; set; }

        [Required]
        public virtual Client Client { get; set; }

    }
    public enum Gender
    {
        Unknown,
        Male,
        Female
    }
}
