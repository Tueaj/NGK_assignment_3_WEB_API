using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherReadingsAPI.Models;

namespace WeatherReadingsAPI.Data
{
    public class WeatherReadingsAPIContext : DbContext
    {
        public WeatherReadingsAPIContext (DbContextOptions<WeatherReadingsAPIContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherRepport>()
                .HasOne(w => w.Place)
                .WithMany(p => p.WeatherRepports)
                .HasForeignKey(w => w.PlaceId);
        }


        public DbSet<WeatherReadingsAPI.Models.User> User { get; set; }
    }
}
