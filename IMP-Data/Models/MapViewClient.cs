using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Data.Models
{
    public class MapViewClient
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public float[] Location { get; set; }

        public MapViewClient()
        {
            //Mark client offline by default
            Status = "Offline";
        }
    }
}
