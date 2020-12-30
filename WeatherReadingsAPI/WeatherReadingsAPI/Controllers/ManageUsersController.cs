using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherReadingsAPI.Data;
using WeatherReadingsAPI.Models;

namespace WeatherReadingsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ManageUsersController : ControllerBase
    {
        private DatabaseController _dbController;

        public ManageUsersController(DatabaseController dbController)
        {
            _dbController = dbController;
        }

        // GET: api/ManageUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return _dbController.GetUsers();
        }

        // GET: api/ManageUsers/5
        [HttpGet("{id}")]
        //[Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = _dbController.FindUserByID(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/ManageUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _dbController.ChangeUserState(user, EntityState.Modified);
            try
            {
                _dbController.SaveDB();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ManageUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _dbController.AddAndSaveUser(user);

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/ManageUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = _dbController.FindUserByID(id);
            if (user == null)
            {
                return NotFound();
            }
            
            _dbController.RemoveUser(user);

            return NoContent();
        }

        private bool UserExists(long id)
        {
            return _dbController.UserExists(id);
        }
    }
}
