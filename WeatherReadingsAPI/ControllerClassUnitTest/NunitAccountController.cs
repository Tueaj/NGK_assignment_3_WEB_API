using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using WeatherReadingsAPI.Controllers;
using WeatherReadingsAPI.Data;
using WeatherReadingsAPI.Models;

namespace ControllerClassUnitTest
{
    public class NunitAccountController
    {
        private AccountController _uut;
        private DatabaseController databaseController;
        private IOptions<AppSettings> _options;


        [SetUp]
        public void Setup()
        {
            databaseController = Substitute.For<DatabaseController>();
            _options = Substitute.For<IOptions<AppSettings>>();
            _uut = new AccountController(databaseController, _options);
        }

        [Test] //Alt context skal trækkes ud af controllere, så de kan substitudes
        public void UserCreated_Register_NoErrorDetected()
        {
            //Arrange
            var UserUnderCreation = new UserDto()
            {
                FirstName = "Lars",
                LastName = "MobbeDreng",
                Password = "123",
                Email = "Lars@MD.dk"
            };
           // _wrApiContext.User.Where(u =>
                //u.Email == UserUnderCreation.Email).FirstOrDefaultAsync().ReturnsNull();

           // _wrApiContext.User.Add(Arg.Any<User>()).ReturnsNull();
            //_wrApiContext.SaveChangesAsync().ReturnsNull();
            //Act
            var result = _uut.Register(UserUnderCreation);

            //Assert

            Assert.That(result, Is.Not.EqualTo(typeof(BadRequestObjectResult)));
        }
    }
}

