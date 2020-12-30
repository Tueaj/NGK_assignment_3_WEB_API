using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using WeatherReadingsAPI.Models;

namespace WeatherReadingsAPI.Hubs
{
    public class ServerSignal : Hub
    {

        public async Task SendReport(WeatherReportDto report)
        {

            await Clients.All.SendAsync("SendReport", report);



        }
    }
}
