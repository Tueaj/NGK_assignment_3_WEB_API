using System.Collections.Generic;

namespace WeatherReadingsAPI.Models
{
    public class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }

        //Relation
        public List<WeatherRepport> WeatherRepports { get; set; }
    }
}