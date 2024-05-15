using Mc2.CrudTest.AcceptanceTests.Services;
using Mc2.CrudTest.DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            // Perform validation
            var validationResults = await CustomerValidator.ValidateCustomerAsync(customer, _customerService.IsEmailUniqueAsync);
            if (validationResults.Count > 0)
            {
                return BadRequest(validationResults);
            }

            // Add the customer
            await _customerService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerId }, customer);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            // Perform validation
            var validationResults = await CustomerValidator.ValidateCustomerAsync(customer, _customerService.IsEmailUniqueAsync);
            if (validationResults.Count > 0)
            {
                return BadRequest(validationResults);
            }

            // Update the customer
            await _customerService.UpdateCustomerAsync(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            // Delete the customer
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
