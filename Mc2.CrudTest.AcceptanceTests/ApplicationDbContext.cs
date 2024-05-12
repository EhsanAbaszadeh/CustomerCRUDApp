using Microsoft.EntityFrameworkCore;
using Mc2.CrudTest.AcceptanceTests.Models;

namespace Mc2.CrudTest.AcceptanceTests.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}