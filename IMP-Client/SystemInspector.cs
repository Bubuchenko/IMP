using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
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
                CPU = GetCpu()
            };
            Console.Write(newSystemInfo.CPU);
            return newSystemInfo;
        }

        public string GetCpu()
        {
            return new ManagementObjectSearcher("root\\CIMV2", "SELECT Name FROM Win32_Processor")
                .Get().Cast<ManagementObject>().FirstOrDefault().GetPropertyValue("Name").ToString();
        }
    }
}
