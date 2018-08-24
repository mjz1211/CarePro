﻿using FileUploadsInAspNetMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FileUploadsInAspNetMvc.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext():base("name=OpenTCLConnection")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DatabaseContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Image> Images { get; set; }

        public System.Data.Entity.DbSet<FileUploadsInAspNetMvc.Models.CareElderItem> CareElderItems { get; set; }
        public DbSet<CareElderImage> CareElderImages { get; set; }
        public DbSet<CareElderItemRecord> CareElderItemRecords { get; set; }
        public DbSet<CareElderMedicine> CareElderMedicines2 { get; set; }

        public DbSet<NightMarketStore> NightMarketStores { get; set; }

        public DbSet<TaipeiBabyCenter> TaipeiBabyCenters { get; set; }

        public DbSet<TaipeiKindergarten> TaipeiKindergartens { get; set; }

        public System.Data.Entity.DbSet<FileUploadsInAspNetMvc.Models.CareKidUser> CareKidUsers { get; set; }

        public DbSet<CaregiverDay> CaregiverDays { get; set; }

        public DbSet<CaregiverRecord> CaregiverRecords { get; set; }
    }
}