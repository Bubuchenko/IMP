using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using IMP_Lib.Models;

namespace IMP_Service.Data
{
    public class IMPContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<PersonalInformation> PersonalInfo { get; set; }
        public DbSet<SystemInfo> SystemInfo { get; set; }
    }
}