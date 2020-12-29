using NUnit.Framework;
using WeatherReadingsAPI.Controllers;

namespace ControllerClassUnitTest
{
    [TestFixture]
    public class NunitWeatherReportController
    {
        private WeatherReportController _uut;

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