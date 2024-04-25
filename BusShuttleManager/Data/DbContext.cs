using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using DomainModel;

namespace DomainModel
{
    public class DataContext : DbContext
    {
        public DbSet<Bus> Bus {get; set;}

        public DbSet<Driver> Driver {get; set;}

        public DbSet<Routes> Routes {get; set;}

        public DbSet<Stop> Stop {get; set;}
        
        public DbSet<Loop> Loop {get; set;}

        public DbSet<Entry> Entry {get; set;}


        public string DbPath {get;}

        public DataContext()
        {
            DbPath = "BusData.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
