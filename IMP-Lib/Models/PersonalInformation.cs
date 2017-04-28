using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Lib.Models
{
    [DataContract]
    public class PersonalInformation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public Gender Gender { get; set; }

        public virtual Client Client { get; set; }

    }
    public enum Gender
    {
        Unknown,
        Male,
        Female
    }
}
