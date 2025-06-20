using Microsoft.EntityFrameworkCore;

namespace ProviderAPI.Models
{
    public class ITIContext : DbContext
    {
        public ITIContext(DbContextOptions<ITIContext> options) :base(options) 
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
