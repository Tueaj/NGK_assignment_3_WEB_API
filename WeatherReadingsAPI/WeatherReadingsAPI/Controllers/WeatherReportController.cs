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

        // GET api/<WeatherReport>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
        [HttpGet("{Date}")]
        public async Task<ActionResult> GetReportsFromDate(DateTime date)
        {
            var WRepotrs = _context.WReport.Where(u => u.Time == date).ToListAsync();
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
        // GET api/<startDate>/<EndDate>
        [HttpGet("{Start}/{end}")]
        public async Task<ActionResult> getReportsBetweenTwoDates(DateTime start, DateTime end)
        {
            var WRepotrs = _context.WReport.Where(u => u.Time >= start && u.Time <= end).Include(u => u.Place).ToListAsync();
            if (WRepotrs == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(WRepotrs);
            }
        }

        // POST api/<WeatherReport>
        [HttpPost("postReport")]
        public async Task<ActionResult<WeatherRepport>> PostReport([FromBody] WeatherRepport report)
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
                newReport.Place = report.Place;
                newReport.PlaceId = report.PlaceId;
                newReport.AirPressure = report.AirPressure;
                newReport.Humidity = report.Humidity;
                newReport.Temp = report.Temp;
                newReport.Time = DateTime.Now;
            }
            _context.WReport.Add(newReport);
            await _context.SaveChangesAsync();
            //string json = JsonConvert.SerializeObject(newReport, Formatting.Indented, JsonSerializerOptions);
            return Ok();

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
