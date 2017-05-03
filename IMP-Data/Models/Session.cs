using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Data.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        public string SessionID { get; set; }
        public string ClientID { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime? SessionEnd { get; set; }
    }
}
