using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherReadingsAPI.Models
{
    public class WeatherReportDto
    {

        public DateTime Time { get; set; }

        [MaxLength(32)]
        public double Temp { get; set; }

        [MaxLength(32)]
        public int Humidity { get; set; }

        [MaxLength(32)]
        public double AirPressure { get; set; }

        [MaxLength(32)]
        public string PlaceId { get; set; }
    }
}
