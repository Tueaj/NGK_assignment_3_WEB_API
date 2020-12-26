using Microsoft.AspNetCore.Authorization;

namespace WeatherReadingsAPI.Models
{
    public class Role : IAuthorizationRequirement
    {
        public Role(string role)
        {
            MyRole = role;
        }

        public string MyRole { get; }
    }
}