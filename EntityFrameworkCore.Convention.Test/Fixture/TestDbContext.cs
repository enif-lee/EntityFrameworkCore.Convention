using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Test.Fixture
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<User> Users { get; set; }
    }

    public class Company
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }

    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public virtual Company Company { get; set; }
    }
}