using Mc2.CrudTest.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.AcceptanceTests.Services
{
    public class CustomerService
    {
        private readonly ApplicationDbContext _context;
        private readonly Func<string, Task<bool>> _isEmailUnique;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ApplicationDbContext context, Func<string, Task<bool>> isEmailUnique, ILogger<CustomerService> logger)
        {
            _context = context;
            _isEmailUnique = isEmailUnique;
            _logger = logger;
        }

        public async Task<bool> IsCustomerUniqueAsync(string firstName, string lastName, DateTime dateOfBirth)
        {
            return await _context.Customers
                .AnyAsync(c => c.FirstName == firstName && c.LastName == lastName && c.DateOfBirth == dateOfBirth);
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return await _isEmailUnique(email);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
