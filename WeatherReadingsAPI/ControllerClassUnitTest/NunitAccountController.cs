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
using static BCrypt.Net.BCrypt;

namespace ControllerClassUnitTest
{
    public class NunitAccountController
    {
        private AccountController _uut;
        private IDatabaseController databaseController;
        private IOptions<AppSettings> _options;


        [SetUp]
        public void Setup()
        {
            databaseController = Substitute.For<IDatabaseController>();
            _options = Substitute.For<IOptions<AppSettings>>();
            _uut = new AccountController(databaseController, _options);
        }

        [Test]
        public void UserCreatedUserNotInDB_Register_NoErrorDetected()
        {
            //Arrange
            var UserUnderCreation = new UserDto()
            {
                FirstName = "Lars",
                LastName = "MobbeDreng",
                Password = "123",
                Email = "Lars@MD.dk"
            };

            databaseController.FindUserByEmail(UserUnderCreation.Email).ReturnsNull();


            //Act
            var result = ((_uut.Register(UserUnderCreation).Result.Result) as ObjectResult);

            //Assert

            Assert.That(result.StatusCode, Is.EqualTo(201));
        }

        [Test]
        public void UserCreatedUserInDB_Register_ErrorDetected()
        {
            //Arrange
            var UserUnderCreation = new UserDto()
            {
                FirstName = "Lars",
                LastName = "MobbeDreng",
                Password = "123",
                Email = "Lars@MD.dk"
            };

            databaseController.FindUserByEmail(Arg.Any<string>()).ReturnsForAnyArgs(new User());

            //Act
            var result = ((_uut.Register(UserUnderCreation).Result.Result) as ObjectResult);

            //Assert
            Assert.That(result.StatusCode, Is.EqualTo(400));

        }

        [Test]
        public void GetUserUserNotInDB_Get_ErrorDetected()
        {
            //Arrange
            var UserUnderTest = new User()
            {
                FirstName = "Lars",
                LastName = "MobbeDreng",
                Email = "Lars@MD.dk",
                UserId = 1
            };

            databaseController.FindUserByID(UserUnderTest.UserId).ReturnsNull();


            //Act
            var result = ((_uut.Get(UserUnderTest.UserId).Result.Result) as NotFoundResult);

            //Assert

            Assert.That(result.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public void GetUserUserInDB_Get_NoErrorDetected()
        {
            //Arrange
            var UserUnderTest = new User()
            {
                FirstName = "Lars",
                LastName = "MobbeDreng",
                Email = "Lars@MD.dk",
                UserId = 1
            };
            var userDto = new UserDto();
            userDto.Email = UserUnderTest.Email;
            userDto.FirstName = UserUnderTest.FirstName;
            userDto.LastName = UserUnderTest.LastName;

            databaseController.FindUserByID(UserUnderTest.UserId).ReturnsForAnyArgs(UserUnderTest);

            //Act
            var result = _uut.Get(UserUnderTest.UserId);

            //Assert all data inside object
            Assert.That(result.Result.Value.LastName, Is.EqualTo(userDto.LastName));
            Assert.That(result.Result.Value.FirstName, Is.EqualTo(userDto.FirstName));
            Assert.That(result.Result.Value.Email, Is.EqualTo(userDto.Email));

        }




    }
}

