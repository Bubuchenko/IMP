using IMP_Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMP_Web.Models.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<MapViewClient> Clients { get; set; }

        public string SerializedClients
        {
            get
            {
                return JsonConvert.SerializeObject(Clients);
            }
        }
    }
}