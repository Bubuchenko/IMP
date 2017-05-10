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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiskDrive>().Ignore(f => f.PercentFreeSpace);
            modelBuilder.Entity<Monitor>().Ignore(f => f.Width);
            modelBuilder.Entity<Monitor>().Ignore(f => f.Height);
            modelBuilder.Entity<Monitor>().Ignore(f => f.Size);
            modelBuilder.Entity<SystemInfo>().Ignore(f => f.FriendlySystemType);
            base.OnModelCreating(modelBuilder);
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
        public DbSet<Session> Sessions { get; set; }
    }
}