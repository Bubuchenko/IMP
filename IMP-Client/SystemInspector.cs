using DirectShowLib;
using IMP_Lib.Enums;
using IMP_Lib.Models;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMP_Client
{
    public static class SystemInspector
    {
        public static async Task<SystemInfo> GetSystemInfo()
        {
            SystemInfo newSystemInfo = new SystemInfo
            {
                AntiVirus = AntiVirus(),
                CPU = CPU,
                DefaultBrowser = DefaultBrowser,
                Drives = DiskDrives,
                GPU = GPU,
                InputDevices = InputDevices,
                MachineName = MachineName,
                MachineSID = MachineSID,
                Monitors = Monitors,
                OperatingSystem = OperatingSystem,
                RAM = RAM,
                SystemLocale = SystemLocale,
                SystemType = SystemType,
                X64_Bit = Is64BitArchitecture,
            };
            return newSystemInfo;
        }


        public static AntiVirus AntiVirus()
        {
            ManagementObject AntiVirusInfo = new ManagementObjectSearcher("root\\SecurityCenter2", "SELECT displayName, pathToSignedProductExe, productState FROM AntiVirusProduct").
            Get().Cast<ManagementObject>().FirstOrDefault();

            return new AntiVirus
            {
                ProductName = AntiVirusInfo.GetPropertyValue("displayName").ToString(),
                PathToFile = AntiVirusInfo.GetPropertyValue("pathToSignedProductExe").ToString(),
                ProductState = AntiVirusInfo.GetPropertyValue("productState").ToString()
            };
        }

        public static string GPU
        {
            get
            {
                return new ManagementObjectSearcher("SELECT Description FROM Win32_DisplayConfiguration")
                    .Get().Cast<ManagementObject>().FirstOrDefault().GetPropertyValue("Description").ToString();
            }
        }
        public static string CPU
        {
            get
            {
                return new ManagementObjectSearcher("root\\CIMV2", "SELECT Name FROM Win32_Processor")
                    .Get().Cast<ManagementObject>().FirstOrDefault().GetPropertyValue("Name").ToString();
            }
        }
        public static int RAM
        {
            get

            {
                return Convert.ToInt32((new ComputerInfo { }).TotalPhysicalMemory / 1024 / 1024);
            }

        }
        public static bool Is64BitArchitecture
        {
            get

            {
                return Environment.Is64BitOperatingSystem;
            }
        }

        public static string MachineSID
        {
            get
            {
                return new SecurityIdentifier((byte[])new DirectoryEntry(string.Format("WinNT://{0},Computer", Environment.MachineName)).Children.Cast<DirectoryEntry>().First().InvokeGet("objectSID"), 0).AccountDomainSid.Value;
            }
        }

        public static string Username
        {
            get
            {
                return Environment.UserName;
            }
        }
        public static string MachineName
        {
            get
            {
                return Environment.MachineName;
            }
        }
        public static string Domain
        {
            get
            {
                return Environment.UserDomainName;
            }
        }
        public static string OperatingSystem
        {
            get
            {
                return (new ComputerInfo { }).OSFullName;
            }
        }

        public static string SystemLocale
        {
            get
            {
                return RegionInfo.CurrentRegion.DisplayName;
            }
        }


        #region Extended code to determine system type
        public enum ChassisTypes
        {
            Other = 1,
            Unknown,
            Desktop,
            LowProfileDesktop,
            PizzaBox,
            MiniTower,
            Tower,
            Portable,
            Laptop,
            Notebook,
            Handheld,
            DockingStation,
            AllInOne,
            SubNotebook,
            SpaceSaving,
            LunchBox,
            MainSystemChassis,
            ExpansionChassis,
            SubChassis,
            BusExpansionChassis,
            PeripheralChassis,
            StorageChassis,
            RackMountChassis,
            SealedCasePC
        }

        public static ChassisTypes GetCurrentChassisType()
        {
            ManagementClass systemEnclosures = new ManagementClass("Win32_SystemEnclosure");

            foreach (ManagementObject obj in systemEnclosures.GetInstances())
            {

                foreach (int i in (UInt16[])(obj["ChassisTypes"]))
                {
                    if (i > 0 && i < 25)
                    {
                        return (ChassisTypes)i;
                    }
                }
            }
            return ChassisTypes.Unknown;
        }
        #endregion
        public static string SystemType
        {
            get
            {
                return GetCurrentChassisType().ToString();
            }
        }


        public static ICollection<DiskDrive> DiskDrives
        {
            get
            {
                List<DiskDrive> diskdrives = new List<DiskDrive>();

                foreach(DriveInfo drive in DriveInfo.GetDrives())
                {
                    diskdrives.Add(new DiskDrive
                    {
                        AvailableFreeSpace = drive.AvailableFreeSpace,
                        DriveType = drive.DriveType,
                        FileSystem = drive.DriveFormat,
                        Name = drive.Name,
                        TotalSpace = drive.TotalSize,
                        VolumeLabel = drive.VolumeLabel
                    });
                }

                return diskdrives;
            }
        }

        public static ICollection<InputDevice> InputDevices
        {
            get
            {
                List<InputDevice> inputDevices = new List<InputDevice>();

                //Add video input devices
                DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice).ToList()
                    .ForEach(f => inputDevices.Add(new InputDevice { Name = f.Name, Type = InputDeviceType.Video}));

                //Get audio input devices
                DsDevice.GetDevicesOfCat(FilterCategory.AudioInputDevice).ToList()
                    .ForEach(f => inputDevices.Add(new InputDevice { Name = f.Name, Type = InputDeviceType.Audio }));


                //Get keyboards
                new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Keyboard").Get().Cast<ManagementObject>().ToList().
                    ForEach(f => inputDevices.Add(new InputDevice { Name = f.GetPropertyValue("Description").ToString(), Type = InputDeviceType.Keyboard }));


                //TODO
                //Other input devices?

                return inputDevices;
            }
        }

        public static string DefaultBrowser
        {
            get
            {
                const string userChoice = @"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice";
                string progId;
                using (RegistryKey userChoiceKey = Registry.CurrentUser.OpenSubKey(userChoice))
                {
                    if (userChoiceKey == null)
                    {
                        return "Unknown";
                    }
                    object progIdValue = userChoiceKey.GetValue("Progid");
                    if (progIdValue == null)
                    {
                        return "Unknown";
                    }
                    progId = progIdValue.ToString();
                    switch (progId)
                    {
                        case "IE.HTTP":
                            return "Internet Explorer";
                        case "FirefoxURL":
                            return "Firefox";
                        case "ChromeHTML":
                            return "Google Chrome";
                        default:
                            return "Unknown";
                    }

                }
            }
        }

        public static List<Monitor> Monitors
        {
            get
            {
                List<Monitor> monitors = new List<Monitor>();

                foreach(Screen screen in Screen.AllScreens)
                {
                    monitors.Add(new Monitor
                    {
                        IsPrimary = screen.Primary,
                        Resolution = string.Format("{0}x{1}", screen.Bounds.Width, screen.Bounds.Height),
                        Type = screen.DeviceFriendlyName()
                    });
                }
                return monitors;
            }
        }
    }
}
