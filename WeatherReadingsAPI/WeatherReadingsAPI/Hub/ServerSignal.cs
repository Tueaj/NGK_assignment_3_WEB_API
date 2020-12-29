using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WeatherReadingsAPI.Models;

namespace WeatherReadingsAPI.Hub
{
    public class ServerSignal : Microsoft.AspNetCore.SignalR.Hub
    {

        public Task SendReport(WeatherReportDto report)
        {
            return  Clients.All.SendAsync("Receivemessage", report);

        }
    }
}
