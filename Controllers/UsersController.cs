using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Premia_API.Data;
using Premia_API.Entities;

namespace Premia_API.Controllers
{
    /// <summary>
    /// Controller class for managing user-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>A list of users.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific user from the database based on the provided id.
        /// </summary>
        /// <param name="id">The id of the user to retrieve.</param>
        /// <returns>The user with the specified id.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        /// <summary>
        /// Retrieves usernames, and ID of all supervisors from the database where isSupervisor is true.
        /// </summary>
        /// <returns>A list of supervisor usernames.</returns>
        [HttpGet("supervisors/names")]
        public async Task<ActionResult<IEnumerable<object>>> GetSupervisorNames()
        {
            var supervisorNames = await _context.Users
                                                .Where(u => u.isSupervisor)
                                                .Select(u => new { Id = u.Id, Username = u.Username })
                                                .ToListAsync();

            if (supervisorNames == null || !supervisorNames.Any())
            {
                return NotFound("No supervisors found.");
            }

            return supervisorNames;
        }

        /// <summary>
        /// Updates an existing user in the database based on the provided id.
        /// </summary>
        /// <param name="id">The id of the user to update.</param>
        /// <param name="user">The updated user object.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

        /// <summary>
        /// Creates a new user in the database.
        /// </summary>
        /// <param name="user">The user object to create.</param>
        /// <returns>The created user object.</returns>
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        /// <summary>
        /// Deletes a user from the database based on the provided id.
        /// </summary>
        /// <param name="id">The id of the user to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.DeleteDate = DateTime.Now;
            user.isDeleted = true;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
