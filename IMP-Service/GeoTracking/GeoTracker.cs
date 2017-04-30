using IMP_Lib.Models;
using IMP_Service.GeoTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace IMP_Service
{
    public static class GeoTracker
    {
        const string IPINFO_API_ADDRESS = "http://ipinfo.io/";

        private static async Task<IpInfo> GetGeoInfoByIPAddress(IPAddress IPAddress)
        {
            IpInfo ipInfo = new IpInfo();
            string jsonResult = await new WebClient().DownloadStringTaskAsync(new Uri(IPINFO_API_ADDRESS + IPAddress.ToString()));
            ipInfo = JsonConvert.DeserializeObject<IpInfo>(jsonResult);
            RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
            ipInfo.Country = myRI1.EnglishName;

            return ipInfo;
        }

        public static async Task<PersonalInformation> GetPersonalInformationByIPAddress(IPAddress IPAddress)
        {
            IpInfo IpInfo = await GetGeoInfoByIPAddress(IPAddress);

            PersonalInformation personalInformation = new PersonalInformation
            {
                City = IpInfo.City,
                Country = IpInfo.Country,
                ISP = IpInfo.Org,
                Location = IpInfo.Loc,
                PostalCode = IpInfo.Postal,
                Region = IpInfo.Region
            };

            return personalInformation;
        }
    }
}