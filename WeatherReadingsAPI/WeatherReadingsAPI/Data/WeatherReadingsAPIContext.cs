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

        public WeatherReadingsAPIContext(DbContextOptions<WeatherReadingsAPIContext> options)
            : base(options)
        {
        }

        public WeatherReadingsAPIContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            ob.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;TrustServerCertificate=False;MultiSubnetFailover=False;database=aspnet-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
       
   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherRepport>()
                .HasOne(w => w.Place)
                .WithMany(p => p.WeatherRepports)
                .HasForeignKey(w => w.PlaceId);
        }


        public DbSet<WeatherReadingsAPI.Models.User> User { get; set; }

        public DbSet<WeatherReadingsAPI.Models.Place> Place { get; set; }
        public DbSet<WeatherReadingsAPI.Models.WeatherRepport> WReport { get; set; }
    }
}
