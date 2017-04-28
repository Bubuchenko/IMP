using IMP_Data.Repositories;
using IMP_Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMP_TestClient
{
    class Program
    {
        static void Main(string[] args)
        {

            TestRegister().Wait();
            
        }

        async static Task TestRegister()
        {
            Client client = new Client{
                Fingerprint = "ABABABABBABABABAB12121212",
                CreationDate = DateTime.Now,
                Username = Environment.UserName,
                SystemInfo = new SystemInfo
                {
                    AntiVirus = "Norton 360",
                    CPU = "i7 2600k",
                    CPUID = "123",
                    DefaultBrowser = "Google Chrome",
                    X64_Bit = true,
                    Webcam = true,
                    GPU = "980 GTX Ti",
                    MachineName = Environment.MachineName,
                    RAM = 16,
                    MonitorsCount = 4,
                    SystemLocale = "EN-us",
                    SystemType = "Desktop",
                    DriveID = "ABC",
                    Harddrives = "sad"
                    
                },
                PersonalInformation = new PersonalInformation
                {
                    Age = 25,
                    City = "Amsterdam",
                    Country = "Netherlands",
                    Description = "A really friendly girl",
                    FirstName = "Bubu",
                    LastName = "Kirichenko",
                    Gender = Gender.Female
                }

            };

            ClientRepository cr = new ClientRepository();
            await cr.RegisterClient(client);
        }
    }
}
