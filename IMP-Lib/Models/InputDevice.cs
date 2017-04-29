using IMP_Lib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Lib.Models
{
    [DataContract]
    public class InputDevice
    {
        public int InputDeviceId { get; set; }
        public string Name { get; set; }
        public InputDeviceType Type { get; set; }
    }
}
