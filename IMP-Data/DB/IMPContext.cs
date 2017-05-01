using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using IMP_Lib.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using IMP_Data.Models;

namespace IMP_Data
{
    public class IMPContext : IdentityDbContext<User>
    {

        public IMPContext() : base("IMP")
        {
        }

        public static IMPContext Create()
        {
            return new IMPContext();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<PersonalInformation> PersonalInfo { get; set; }
        public DbSet<SystemInfo> SystemInfo { get; set; }
        public DbSet<AntiVirus> AntiVirus { get; set; }
        public DbSet<Monitor> Monitors { get; set; }
        public DbSet<InputDevice> InputDevices { get; set; }
        public DbSet<DiskDrive> DiskDrives { get; set; }

    }
}