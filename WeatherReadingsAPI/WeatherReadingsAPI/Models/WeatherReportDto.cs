﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherReadingsAPI.Models
{
    public class WeatherReportDto
    {

        

       
        public double Temp { get; set; }

        
        public int Humidity { get; set; }

        
        public double AirPressure { get; set; }

        
        public int PlaceId { get; set; }
    }
}
