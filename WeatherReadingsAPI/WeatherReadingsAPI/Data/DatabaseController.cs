using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WeatherReadingsAPI.Models;

namespace WeatherReadingsAPI.Data
{
    public class DatabaseController
    {
        private WeatherReadingsAPIContext _context;

        public DatabaseController()
        {
            _context = new WeatherReadingsAPIContext();
        }


        public User FindUserByEmail(string email)
        {
            return _context.User.FirstOrDefault(u => u.Email == email);
        }

        public void AddAndSaveUser(User user)
        {
            _context.User.Add(user);
            SaveDB();
        }

        public List<User> GetUsers()
        {
            return _context.User.ToList();
        }

        public User FindUserByID(long id)
        {
            return _context.User.Find(id);
        }
       

        public void SaveDB()
        {
            _context.SaveChanges();
        }

        public void ChangeUserState(User user, EntityState entityState)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public void RemoveUser(User user)
        {
            _context.User.Remove(user);
            SaveDB();
        }

        public bool UserExists(long id)
        {
            return _context.User.Any(e => e.UserId == id);
        }

        public List<WeatherRepport> GetthreeNewestReports()
        {
            return _context.WReport.OrderByDescending(u => u.Time).Take(3).ToList();
        }

        public List<WeatherRepport> GetReportsFromDate(DateTime date)
        {
            return _context.WReport.Where(u => u.Time.Date == date).ToList();
        }

        public List<WeatherRepport> GetReportsBetweenTwoDates(DateTime dateStart, DateTime dateEnd)
        {
            return _context.WReport.Where(u => u.Time.Date >= dateStart && u.Time.Date <= dateEnd).ToList();
        }

        public Place FindPlaceById(int id)
        {
            return _context.Find<Place>(id);
        }

        public void AddAndSaveWeatherReport(WeatherRepport repport)
        {
            _context.WReport.AddAsync(repport);
            SaveDB();
        }

        
    }



}