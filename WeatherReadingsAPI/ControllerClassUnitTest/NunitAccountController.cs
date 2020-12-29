using Microsoft.Extensions.Options;
using NUnit.Framework;
using WeatherReadingsAPI.Controllers;
using WeatherReadingsAPI.Data;
using WeatherReadingsAPI.Models;

namespace ControllerClassUnitTest
{
    public class NunitAccountController
    {
        private AccountController _uut;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}