using WeatherReadingsAPI.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static BCrypt.Net.BCrypt;

namespace WeatherReadingsAPI.Data
{
    public static class DbUtilities
    {
        public const int BcryptWorkfactor = 10;
        public static void SeedData(WeatherReadingsAPIContext context)
        {
            context.Database.EnsureCreated();
            addAdminUser(context);
            addWeatherStations(context);
        }

        private static void addAdminUser(WeatherReadingsAPIContext context)
        {
            UserDto regUser = new UserDto();
            regUser.Email = ("Admin@admin.dk").ToLower();
            regUser.FirstName = "Admin";
            regUser.LastName = "Admin";
            regUser.Password = "admin";
            var emailExist = context.User.FirstOrDefault(u => u.Email == regUser.Email);
            if (emailExist != null)
            {return;}
            User user = new User()
            {
                Email = regUser.Email,
                FirstName = regUser.FirstName,
                LastName = regUser.LastName
            };
            user.Role = Role.Admin;
            user.PwHash = HashPassword(regUser.Password, BcryptWorkfactor);
            context.User.Add(user);
            context.SaveChanges();
        }
        private static void addWeatherStations(WeatherReadingsAPIContext context)
        {
            UserDto regUser = new UserDto();
            regUser.Email = ("WS1@WS1.dk").ToLower();
            regUser.FirstName = "WeatherStation1";
            regUser.LastName = "WeatherStation1";
            regUser.Password = "admin";
            var emailExist = context.User.Where(u =>
                u.Email == regUser.Email).FirstOrDefault();
            if (emailExist == null)
            {
                User user = new User()
                {
                    Email = regUser.Email,
                    FirstName = regUser.FirstName,
                    LastName = regUser.LastName
                };
                user.Role = Role.WeatherStation;
                user.PwHash = HashPassword(regUser.Password, BcryptWorkfactor);
                context.User.Add(user);
            }

            UserDto regUser2 = new UserDto();
            regUser2.Email = ("WS2@WS2.dk").ToLower();
            regUser2.FirstName = "WeatherStation2";
            regUser2.LastName = "WeatherStation2";
            regUser2.Password = "admin";
            var emailExist2 = context.User.Where(u =>
                u.Email == regUser2.Email).FirstOrDefault();
            if (emailExist2 == null)
            {
                User user = new User()
                {
                    Email = regUser2.Email,
                    FirstName = regUser2.FirstName,
                    LastName = regUser2.LastName
                };
                user.Role = Role.WeatherStation;
                user.PwHash = HashPassword(regUser2.Password, BcryptWorkfactor);
                context.User.Add(user);
            }

            UserDto regUser3 = new UserDto();
            regUser3.Email = ("WS3@WS3.dk").ToLower();
            regUser3.FirstName = "WeatherStation3";
            regUser3.LastName = "WeatherStation3";
            regUser3.Password = "admin";
            var emailExist3 = context.User.Where(u =>
                u.Email == regUser3.Email).FirstOrDefault();
            if (emailExist3 == null)
            {
                User user = new User()
                {
                    Email = regUser3.Email,
                    FirstName = regUser3.FirstName,
                    LastName = regUser3.LastName
                };
                user.Role = Role.WeatherStation;
                user.PwHash = HashPassword(regUser3.Password, BcryptWorkfactor);
                context.User.Add(user);
            }

            Place place1 = new Place();
            place1.Name = ("Lolland").ToLower();
            place1.Lat = 54.768333;
            place1.Lon = 11.421139;
            var placeExists1 = context.Place.Where(u => u.Name == place1.Name).FirstOrDefault();
            if(placeExists1 == null)
            {
                Place place = new Place()
                {
                    Name = place1.Name,
                    Lat = place1.Lat,
                    Lon = place1.Lon
                };
                context.Place.Add(place);
            }
           

            Place place2 = new Place();
            place2.Name = ("Legoland").ToLower();
            place2.Lat = 55.44539;
            place2.Lon = 9.072100;
            var placeExists2 = context.Place.Where(u => u.Name == place1.Name).FirstOrDefault();
            if (placeExists2 == null)
            {
                Place place = new Place()
                {
                    Name = place2.Name,
                    Lat = place2.Lat,
                    Lon = place2.Lon
                };
                context.Place.Add(place);
            }

            Place place3 = new Place();
            place3.Name = ("Randers").ToLower();
            place3.Lat = 55.245999;
            place3.Lon = 10.026000;
            var placeExists3 = context.Place.Where(u => u.Name == place1.Name).FirstOrDefault();
            if (placeExists3 == null)
            {
                Place place = new Place()
                {
                    Name = place3.Name,
                    Lat = place3.Lat,
                    Lon = place3.Lon
                };
                context.Place.Add(place);
            }



            context.SaveChanges();
        }
    }
}