using ApiProject.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Data
{
    public class ApiContext:DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<RefreshTokenTbl> RefreshToken { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
