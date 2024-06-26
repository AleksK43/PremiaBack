﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Premia_API.Data;
using Premia_API.DTO;
using Premia_API.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Premia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationRequestsController : ControllerBase
    {
        private readonly DataContext _context;

        public RegistrationRequestsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RegistrationRequest>> GetRegistrationRequests()
        {
            var unapprovedRequests = _context.RegistrationRequests
                                    .Where(r => !r.isApproved)
                                    .ToList();
            return unapprovedRequests;
        }

        [HttpPost]
        public async Task<ActionResult> AddRegistrationRequest(UserRegisterTaskDTO requestDTO)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(requestDTO.Password);
            var existingRequest = await _context.RegistrationRequests.FirstOrDefaultAsync(r => r.Email == requestDTO.Email);
            if (existingRequest != null)
            {
                return Conflict(new { message = "Email already exists." });
            }
            var request = new RegistrationRequest
            {
                Name = requestDTO.Name,
                LastName = requestDTO.LastName,
                Department = requestDTO.Department,
                SupervisorId = requestDTO.SupervisorId,
                Email = requestDTO.Email,
                PasswordHash = passwordHash
            };


            _context.RegistrationRequests.Add(request);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Registration request added successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegistrationRequest(int id)
        {
            var registrationRequest = await _context.RegistrationRequests.FindAsync(id);

            if (registrationRequest == null)
            {
                return NotFound();
            }

            registrationRequest.isApproved = true;

            try
            {
                // Save changes to mark the registration request as approved
                await _context.SaveChangesAsync();

                // Create a new user based on the registration request and supervisor information
                var newUser = new User
                {
                    Name = registrationRequest.Name,
                    LastName = registrationRequest.LastName,
                    Username = string.Concat(registrationRequest.Name, " ", registrationRequest.LastName),
                    Department = registrationRequest.Department,
                    SupervisorId = registrationRequest.SupervisorId,
                    Email = registrationRequest.Email,
                    PasswordHash = registrationRequest.PasswordHash
                };

                // Add the new user to the context and save changes
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Registration request approved successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationRequestExists(id))
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


        private bool RegistrationRequestExists(int id)
        {
            return _context.RegistrationRequests.Any(e => e.Id == id);
        }
    }
}
