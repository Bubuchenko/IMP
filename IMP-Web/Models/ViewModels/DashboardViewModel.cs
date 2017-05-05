using IMP_Data;
using IMP_Data.Models;
using IMP_Lib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMP_Web.Models.ViewModels
{
    public class DashboardViewModel
    {
        public List<Client> Clients { get; set; }

        public string SerializedClients
        {
            get
            {
                return Clients.Serialize();
            }
        }
    }
}