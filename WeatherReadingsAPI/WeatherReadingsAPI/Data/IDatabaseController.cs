using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherReadingsAPI.Models;

namespace WeatherReadingsAPI.Data
{
    public interface IDatabaseController
    {
        public User FindUserByEmail(string email);

        public void AddAndSaveUser(User user);

        public List<User> GetUsers();

        public User FindUserByID(long id);


        public void SaveDB();

        public void ChangeUserState(User user, EntityState entityState);

        public void RemoveUser(User user);

        public bool UserExists(long id);

        public List<WeatherRepport> GetthreeNewestReports();

        public List<WeatherRepport> GetReportsFromDate(DateTime date);

        public List<WeatherRepport> GetReportsBetweenTwoDates(DateTime dateStart, DateTime dateEnd);

        public Task<Place> FindPlaceById(int id);
        public void AddAndSaveWeatherReport(WeatherRepport repport);
    }
}