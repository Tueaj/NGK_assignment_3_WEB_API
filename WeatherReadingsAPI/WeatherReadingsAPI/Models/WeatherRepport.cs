using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.SignalR;

namespace WeatherReadingsAPI.Models
{
    public class WeatherRepport
    {
        [Key]
        public int Id { get; set; }

        public DateTime Time { get; set; }
        public double Temp { get; set; }
        public int Humidity { get; set; }
        public double AirPressure { get; set; }

        //Relation
        public Place Place { get; set; }
        public int PlaceId { get; set; }
    }
}