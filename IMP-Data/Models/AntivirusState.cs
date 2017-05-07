using IMP_Data.Enums;
using IMP_Lib.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Data.Models
{
    public class AntivirusState
    {
        private string ProductState { get; set; }

        public AntivirusState(string antivirusstate)
        {
            this.ProductState = antivirusstate;
        }

        public AntivirusStateTypes Type
        {
            get
            {
                string hex = "0" + int.Parse(ProductState).ToString("X");
                switch (hex.Substring(0, 2))
                {
                    case "01":
                        return AntivirusStateTypes.Firewall;
                    case "04":
                        return AntivirusStateTypes.Antivirus;
                    case "06":
                        return AntivirusStateTypes.Antivirus;
                    case "08":
                        return AntivirusStateTypes.Antispyware;
                    case "00":
                        return AntivirusStateTypes.None;
                    default:
                        return AntivirusStateTypes.Unknown;
                }
            }
        }

        public AntivirusStatus Status
        {
            get
            {
                string hex = "0" + int.Parse(ProductState).ToString("X");
                switch (hex.Substring(2, 2))
                {
                    case "16":
                        return AntivirusStatus.Enabled;
                    case "11":
                        return AntivirusStatus.Enabled;
                    case "21":
                        return AntivirusStatus.Disabled;
                    default:
                        return AntivirusStatus.Unknown;
                }
            }
        }

        public bool IsUpToDate
        {
            get
            {
                string hex = "0" + int.Parse(ProductState).ToString("X");
                switch (hex.Substring(4, 2))
                {
                    case "00":
                        return true;
                    default:
                        return false;
                }
            }
        }
    }
}
