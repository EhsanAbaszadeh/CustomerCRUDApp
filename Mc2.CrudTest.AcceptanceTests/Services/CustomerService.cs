using Mc2.CrudTest.AcceptanceTests.Data;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.AcceptanceTests.Services
{
    public class CustomerService
    {
        private readonly Func<string, Task<bool>> _isEmailUnique;
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context, Func<string, Task<bool>> isEmailUnique)
        {
            _context = context;
            _isEmailUnique = isEmailUnique;
        }

        public async Task<bool> IsCustomerUniqueAsync(string firstName, string lastName, DateTime dateOfBirth)
        {
            return await _context.Customers
                .AnyAsync(c => c.FirstName == firstName && c.LastName == lastName && c.DateOfBirth == dateOfBirth);
        }

        // Other methods for saving, fetching, updating, or deleting customers...

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return await _isEmailUnique(email);
        }
    }
}