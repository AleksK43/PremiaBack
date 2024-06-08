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
    /**
         * @class CustomersController
         * @brief This class handles the API endpoints related to customers.
         */
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DataContext _context;

        /**
         * @brief Initializes a new instance of the CustomersController class.
         * @param context The DataContext object used for accessing the database.
         */
        public CustomersController(DataContext context)
        {
            _context = context;
        }

        /**
         * @brief Retrieves all customers from the database.
         * @return A list of Customer objects.
         */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {
            return await _context.Customer.ToListAsync();
        }

        /**
         * @brief Retrieves a specific customer from the database.
         * @param id The ID of the customer to retrieve.
         * @return The Customer object with the specified ID.
         */
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        /**
         * @brief Updates a specific customer in the database.
         * @param id The ID of the customer to update.
         * @param customer The updated Customer object.
         * @return NoContent if the update is successful, BadRequest if the ID does not match the customer's ID, or NotFound if the customer does not exist.
         */
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        /**
         * @brief Creates a new customer in the database.
         * @param customer The Customer object to create.
         * @return The created Customer object.
         */
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        /**
         * @brief Deletes a specific customer from the database.
         * @param id The ID of the customer to delete.
         * @return NoContent if the deletion is successful, or NotFound if the customer does not exist.
         */
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /**
         * @brief Checks if a customer with the specified ID exists in the database.
         * @param id The ID of the customer to check.
         * @return True if the customer exists, False otherwise.
         */
        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
