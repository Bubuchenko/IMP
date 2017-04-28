using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using IMP_Lib.Models;

namespace IMP_Data
{
    public class IMPContext : DbContext
    {

        public IMPContext() : base("IMPContext")
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<PersonalInformation> PersonalInfo { get; set; }
        public DbSet<SystemInfo> SystemInfo { get; set; }
    }
}