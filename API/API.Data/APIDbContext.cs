using Microsoft.EntityFrameworkCore;
using API.Domain.Entities;

namespace API.Data
{
    public class APIDbContext : DbContext
    {
        // Example: 
        public DbSet<BaseEntity<long>> Entities { get; set; }

        public APIDbContext() : base()
        {
        }

        public APIDbContext(DbContextOptions<APIDbContext> options)
            : base(options)
        {
            // Here you will add the options and configurations
            // Also add your entities as you can see on top
        }
    }
}
