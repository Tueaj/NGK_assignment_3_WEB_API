using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using WeatherReadingsAPI.Controllers;
using WeatherReadingsAPI.Data;
using WeatherReadingsAPI.Hubs;
using WeatherReadingsAPI.Models;

namespace ControllerClassUnitTest
{
    [TestFixture]
    public class NunitWeatherReportController
    {
        private WeatherReportController _uut;
        private IDatabaseController databaseController;
        private IHubContext<ServerSignal> hub;

        [SetUp]
        public void Setup()
        {
            databaseController = Substitute.For<IDatabaseController>();
            hub = Substitute.For<IHubContext<ServerSignal>>();
            _uut = new WeatherReportController(databaseController,hub);
        }

        [Test]
        public void GetthreeNewestReportstInDB_Newest_NoErrorDetected()
        {
            //Arrange
            var reportList = new List<WeatherRepport>();
            reportList.Add(new WeatherRepport{AirPressure = 5 , Humidity = 3, Id = 1, Place = new Place(), PlaceId = 1, Temp = 10, Time = DateTime.Now});
            reportList.Add(new WeatherRepport { AirPressure = 6, Humidity = 4, Id = 2, Place = new Place(), PlaceId = 2, Temp = 11, Time = DateTime.Now });
            reportList.Add(new WeatherRepport { AirPressure = 7, Humidity = 5, Id = 3, Place = new Place(), PlaceId = 3, Temp = 12, Time = DateTime.Now });


            databaseController.GetthreeNewestReports().Returns(reportList);


            //Act
            var result = (_uut.GetthreeNewestReports().Result as OkObjectResult);

            //Assert

            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void GetthreeNewestReportstNotInDB_Newest_ErrorDetected()
        {
            //Arrange

            databaseController.GetthreeNewestReports().ReturnsNull();


            //Act
            var result = (_uut.GetthreeNewestReports().Result as NotFoundResult);

            //Assert

            Assert.That(result.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public void GetReportsFromDateReportstInDB_SpecificDate_NoErrorDetected()
        {
            //Arrange
            var reportList = new List<WeatherRepport>();
            reportList.Add(new WeatherRepport { AirPressure = 5, Humidity = 3, Id = 1, Place = new Place(), PlaceId = 1, Temp = 10, Time = DateTime.Now });
            reportList.Add(new WeatherRepport { AirPressure = 6, Humidity = 4, Id = 2, Place = new Place(), PlaceId = 2, Temp = 11, Time = DateTime.Now });
            reportList.Add(new WeatherRepport { AirPressure = 7, Humidity = 5, Id = 3, Place = new Place(), PlaceId = 3, Temp = 12, Time = DateTime.Now });

            DateDto dt = new DateDto();
            dt.StartDate = "Dec 29, 2020";
            dt.EndDate = "Dec 31, 2020";


            databaseController.GetReportsBetweenTwoDates(DateTime.Parse(dt.StartDate), DateTime.Parse(dt.EndDate)).Returns(reportList);


            //Act
            var result = (_uut.getReportsBetweenTwoDates(dt).Result.Result as CreatedResult);

            //Assert

            Assert.That(result.StatusCode, Is.EqualTo(201));
        }

        [Test]
        public void GetReportsFromDateReportstNotInDB_SpecificDate_ErrorDetected()
        {
            //Arrange
            var reportList = new List<WeatherRepport>();
            reportList.Add(new WeatherRepport { AirPressure = 5, Humidity = 3, Id = 1, Place = new Place(), PlaceId = 1, Temp = 10, Time = DateTime.Now });
            reportList.Add(new WeatherRepport { AirPressure = 6, Humidity = 4, Id = 2, Place = new Place(), PlaceId = 2, Temp = 11, Time = DateTime.Now });
            reportList.Add(new WeatherRepport { AirPressure = 7, Humidity = 5, Id = 3, Place = new Place(), PlaceId = 3, Temp = 12, Time = DateTime.Now });

            DateDto dt = new DateDto();
            dt.StartDate = "Dec 29, 2020";
            dt.EndDate = "Dec 31, 2020";

            databaseController.GetReportsBetweenTwoDates(DateTime.Parse(dt.StartDate), DateTime.Parse(dt.EndDate)).ReturnsNull();


            //Act
            var result = (_uut.GetthreeNewestReports().Result as NotFoundResult);

            //Assert

            Assert.That(result.StatusCode, Is.EqualTo(404));
        }
    }
}