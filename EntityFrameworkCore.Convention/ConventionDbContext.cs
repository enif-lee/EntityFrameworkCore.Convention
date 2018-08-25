using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention
{
    public class ConventionDbContext : DbContext
    {
        protected ConventionDbContext()
        {
        }

        public ConventionDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}