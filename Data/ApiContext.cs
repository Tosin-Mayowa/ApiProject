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
    }
}
