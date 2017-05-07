using IMP_Data;
using IMP_Data.Extensions;
using IMP_Data.Models;
using IMP_Lib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMP_Web.Models.ViewModels
{
    public class DashboardViewModel
    {
        public SelectList AntivirusStatuses
        {
            get
            {
                return new SelectList(Clients.OrderBy(x => x.SystemInfo.AntiVirus.FirstOrDefault().GetAntivirusState().Status)
               .Select(f => f.SystemInfo.AntiVirus.FirstOrDefault().GetAntivirusState().Status).Distinct());
            }
        }

        public SelectList AntivirusNames
        {
            get
            {
                return new SelectList(Clients.OrderBy(x => x.SystemInfo.AntiVirus.FirstOrDefault().ProductName)
               .Select(f => f.SystemInfo.AntiVirus.FirstOrDefault().ProductName).Distinct());
            }
        }

        public SelectList SystemTypes
        {
            get
            {
                return new SelectList(Clients.OrderBy(x => x.SystemInfo.SystemType)
               .Select(f => f.SystemInfo.SystemType).Distinct());
            }
        }
        public SelectList OperatingSystems
        {
            get
            {
                return new SelectList(Clients.OrderBy(x => x.SystemInfo.OperatingSystem)
               .Select(f => f.SystemInfo.OperatingSystem).Distinct());
            }
        }

        public SelectList Statuses
        {
            get
            {
                return new SelectList(new List<string> { "Online", "Offline" });
            }
        }

        public List<SelectListItem> CreationDateSelections
        {
            get
            {
                DateTime date = DateTime.Now;

                return new List<SelectListItem>
                    {
                    new SelectListItem{
                        Text = "Today",
                        Value = new DateTime(date.Year, date.Month, date.Day).ToString()
                    },
                    new SelectListItem{
                        Text = "Yesterday",
                        Value = new DateTime(date.Year, date.Month, date.Day).AddDays(-1).ToString()
                    },
                    new SelectListItem{
                        Text = "The past week",
                        Value = new DateTime(date.Year, date.Month, date.Day).AddDays(-7).ToString()
                    },
                    new SelectListItem{
                        Text = "This month",
                        Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString()
                    }
                };
            }
        }



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