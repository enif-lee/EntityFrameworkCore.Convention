using EntityFrameworkCore.Convention.Test.Fixture.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Test.Fixture
{
    public class TestDb : DbContext
    {
        public DbSet<TestEntity> TestEntities { get; set; }
    }
}