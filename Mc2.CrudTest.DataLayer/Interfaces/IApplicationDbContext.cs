using System.Data.Entity;

namespace Mc2.CrudTest.DataLayer.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }
        int SaveChanges();
    }
}
