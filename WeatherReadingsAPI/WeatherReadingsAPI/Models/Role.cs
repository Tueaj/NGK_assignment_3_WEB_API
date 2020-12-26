using Microsoft.AspNetCore.Authorization;

namespace WeatherReadingsAPI.Models
{
    public static class Role
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string WeatherStation = "WeatherStation";
    }
}