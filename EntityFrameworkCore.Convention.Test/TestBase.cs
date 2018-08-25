using System.Collections.Generic;
using EntityFrameworkCore.Convention.Test.Fixture;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EntityFrameworkCore.Convention.Test
{
    public abstract class TestBase
    {
        protected TestDbContext Context { get; set; }

        [SetUp]
        public void Setup()
        {
            var optionBuilder = new DbContextOptionsBuilder().UseSqlite("Data Source=Test.db");
            Context = new TestDbContext(optionBuilder.Options);
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
            Context.Companies.Add(new Company
            {
                Name = "Testers",
                Users = new List<User>
                {
                    new User
                    {
                        Name = "Tester",
                        Email = "tester01@test.com",
                        Phone = "010-1234-5678"
                    }
                }
            });
            Context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}