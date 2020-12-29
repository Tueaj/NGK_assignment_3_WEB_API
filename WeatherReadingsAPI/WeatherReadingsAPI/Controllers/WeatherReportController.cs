using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WeatherReadingsAPI.Data;
using WeatherReadingsAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherReadingsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "WeatherStation")]
    public class WeatherReportController : ControllerBase
    {
        private readonly WeatherReadingsAPIContext _context;


        public WeatherReportController(WeatherReadingsAPIContext context)
        {
            _context = context;
        }

      

        //Den er ikke testet, så ved ikke om den virker, men det er noget at arbejde ud fra - Jacob
        //Get api/newest
        [HttpGet("newest")]
        public async Task<ActionResult> GetthreeNewestReports()
        {
            var WRepotrs = _context.WReport.OrderByDescending(u => u.Time).Take(3).ToListAsync();
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

            
            var WRepotrs = await _context.WReport.Where(u => u.Time.Date == compareDate.Date).ToListAsync();
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
        public async Task<ActionResult<List<WeatherRepport>>> getReportsBetweenTwoDates([FromBody] DateDto dates)
        {
            var compareStartDate = DateTime.Parse(dates.StartDate);
            var compareEndDate = DateTime.Parse(dates.EndDate);

            var WRepotrs =  await _context.WReport.Where(u => u.Time.Date >= compareStartDate.Date && u.Time.Date <= compareEndDate.Date).ToListAsync();
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
        public async Task<ActionResult<WeatherRepport>> PostReport([FromBody] WeatherReportDto report)
        {
            if (report == null)
            {
                return BadRequest(new {errormessage = "Bad report"});
            }

            var placeName = await _context.FindAsync<Place>(report.PlaceId);

            if (placeName.Name == null )
            {
                return BadRequest(new {errormessage = "Place doesnt exist"});
            }

            WeatherRepport newReport = new WeatherRepport();
            {
                newReport.Place = _context.Place.FirstOrDefault(u => u.Id == report.PlaceId);
                newReport.PlaceId = report.PlaceId;
                newReport.AirPressure = report.AirPressure;
                newReport.Humidity = report.Humidity;
                newReport.Temp = report.Temp;
                newReport.Time = DateTime.Now;
            }

            await _context.WReport.AddAsync(newReport);

            await _context.SaveChangesAsync();

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
