using IMP_Data.Models;
using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Data.Extensions
{
    public static class AntivirusExtensionMethods
    {
        public static AntivirusState GetAntivirusState(this AntiVirus antivirus)
        {
            return new AntivirusState(antivirus.ProductState);
        }
    }
}
