using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProviderAPI.Models
{
    public class ITIContext : IdentityDbContext<ApplicationUser>
    {
        public ITIContext(DbContextOptions<ITIContext> options) :base(options) 
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
