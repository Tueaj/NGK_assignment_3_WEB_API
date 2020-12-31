using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WeatherReadingsAPI.Data;
using WeatherReadingsAPI.Hubs;
using WeatherReadingsAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherReadingsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WeatherReportController : ControllerBase
    {
        private IDatabaseController _dbController;
        private readonly IHubContext<ServerSignal> _hub;

        public WeatherReportController(IDatabaseController dbController, IHubContext<ServerSignal> hub)
        {
            _dbController = dbController;

           _hub = hub;
        }

      

        //Den er ikke testet, så ved ikke om den virker, men det er noget at arbejde ud fra - Jacob
        //Get api/newest
        [HttpGet("newest")]
       
        public async Task<ActionResult> GetthreeNewestReports()
        {
            var WRepotrs = _dbController.GetthreeNewestReports();
            if (WRepotrs == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(WRepotrs);
            }
        }

        //Den er ikke testet, så ved ikke om den virker, men det er noget at arbejde ud fra - Jacob
        //Get api/<date>
        [HttpGet("SpecificDate")]
        
        public async Task<ActionResult<List<WeatherRepport>>> GetReportsFromDate([FromBody] DateDto dates)
        {
            var compareDate = DateTime.Parse(dates.StartDate);


            var WRepotrs = _dbController.GetReportsFromDate(compareDate);
            if (WRepotrs == null)
            {
                return NotFound();
            }
            else
            {
                return Created(WRepotrs.ToString(), WRepotrs);
            }
        }
        

        //Den er ikke testet, så ved ikke om den virker, men det er noget at arbejde ud fra - Jacob
        // GET api/<startDate>/<EndDate>
        [HttpGet("DateRange")]
        [Authorize]
        public async Task<ActionResult<List<WeatherRepport>>> getReportsBetweenTwoDates([FromBody] DateDto dates)
        {
            var compareStartDate = DateTime.Parse(dates.StartDate);
            var compareEndDate = DateTime.Parse(dates.EndDate);

            var WRepotrs = _dbController.GetReportsBetweenTwoDates(compareStartDate, compareEndDate);
            if (WRepotrs == null)
            {
                return NotFound();
            }
            else
            {
                return Created(WRepotrs.ToString(), WRepotrs);
            }
        }

        // POST api/<WeatherReport>
        [HttpPost("postReport")]
        [Authorize(Roles = "WeatherStation")]
        public async Task<ActionResult<WeatherRepport>> PostReport([FromBody] WeatherReportDto report)
        {
            if (report == null)
            {
                return BadRequest(new {errormessage = "Bad report"});
            }
            var place = await _dbController.FindPlaceById(report.PlaceId);

            if (place.Name == null )
            {
                return BadRequest(new {errormessage = "Place doesnt exist"});
            }
            WeatherRepport newReport = new WeatherRepport();
            {
                newReport.Place = await _dbController.FindPlaceById(report.PlaceId);
                newReport.PlaceId = report.PlaceId;
                newReport.AirPressure = report.AirPressure;
                newReport.Humidity = report.Humidity;
                newReport.Temp = report.Temp;

                if(report.Time == default)
                {
                    newReport.Time = DateTime.Now;
                }
                else
                {
                    newReport.Time = report.Time;
                }
            }
            report.Time = newReport.Time;
            report.PlaceName = newReport.Place.Name;

            await _hub.Clients.All.SendAsync("SendReport", report);

             _dbController.AddAndSaveWeatherReport(newReport);

             return Created(newReport.ToString(), newReport);
        }
        
        // PUT api/<WeatherReport>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WeatherReport>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
