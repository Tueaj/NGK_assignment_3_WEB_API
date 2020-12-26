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
            regUser.Email = ("WS2@WS2.dk").ToLower();
            regUser.FirstName = "WeatherStation2";
            regUser.LastName = "WeatherStation2";
            regUser.Password = "admin";
            var emailExist2 = context.User.Where(u =>
                u.Email == regUser.Email).FirstOrDefault();
            if (emailExist2 == null)
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

            UserDto regUser3 = new UserDto();
            regUser.Email = ("WS3@WS3.dk").ToLower();
            regUser.FirstName = "WeatherStation3";
            regUser.LastName = "WeatherStation3";
            regUser.Password = "admin";
            var emailExist3 = context.User.Where(u =>
                u.Email == regUser.Email).FirstOrDefault();
            if (emailExist3 == null)
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


            context.SaveChanges();
        }
    }
}