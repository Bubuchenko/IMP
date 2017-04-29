using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMP_Client
{
    class SystemInspector
    {
        public SystemInfo GetSystemInfo()
        {
            SystemInfo newSystemInfo = new SystemInfo();
            new SystemInfo
            {
                CPU = newSystemInfo.GetCpu()
            };
            Console.Write(newSystemInfo.CPU);
            return newSystemInfo;
        }
    }
}
