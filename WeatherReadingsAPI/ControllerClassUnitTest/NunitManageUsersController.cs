using NUnit.Framework;
using WeatherReadingsAPI.Controllers;

namespace ControllerClassUnitTest
{
    [TestFixture]
    public class NunitManageUsersController
    {
        private ManageUsersController _uut;

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