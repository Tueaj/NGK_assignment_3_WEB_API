using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using WeatherReadingsAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WeatherReadingsAPI.Models;
using static BCrypt.Net.BCrypt;

namespace WeatherReadingsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        const int BcryptWorkfactor = 10;
        private readonly AppSettings _appSettings;
        private IDatabaseController _dbController;


        public AccountController(IDatabaseController dbController, IOptions<AppSettings> appSettings)
        {
            _dbController = dbController;
            _appSettings = appSettings.Value;

        }


        [HttpPost("register"), AllowAnonymous]
        public async Task<ActionResult<UserDto>> Register(UserDto regUser)
        {
            regUser.Email = regUser.Email.ToLower();
            var emailExist = _dbController.FindUserByEmail(regUser.Email);
            if (emailExist != null)
                return BadRequest(new { errorMessage = "Email already in use" });
            User user = new User()
            {
                Email = regUser.Email,
                FirstName = regUser.FirstName,
                LastName = regUser.LastName
            };
            user.PwHash = HashPassword(regUser.Password, BcryptWorkfactor);
            user.Role = Role.User;
            _dbController.AddAndSaveUser(user);
            return CreatedAtAction("Get", new { id = user.UserId }, regUser);
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> Get(long id)
        {
            var user = _dbController.FindUserByID(id);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = new UserDto();
            userDto.Email = user.Email;
            userDto.FirstName = user.FirstName;
            userDto.LastName = user.LastName;
            return userDto;
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult<TokenDto>> Login(UserDto login)
        {
            login.Email = login.Email.ToLower();
            var user = _dbController.FindUserByEmail(login.Email);
            if (user != null)
            {
                var validPwd = Verify(login.Password, user.PwHash);
                if (validPwd)
                {
                    var token = new TokenDto();
                    token.JWT = GenerateToken(user);
                    return token;
                }
            }
            ModelState.AddModelError(string.Empty, "Forkert brugernavn eller password");
            return BadRequest(ModelState);
        }

        private string GenerateToken(User user)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim("UserId", user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,
                    new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }




}

