using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.Convention.Helpers;
using EntityFrameworkCore.Convention.Test.Fixture.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Test.Fixture
{
    public class TestDb : DbContext
    {
        public DbSet<TestEntity> TestEntities { get; set; }

        public override int SaveChanges()
        {
	        ChangeTracker
		        .UpdateCreatedAtFields()
		        .UpdateUpdatedAtEntities()
		        .UpdateStateEntities();

	        return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
	        ChangeTracker
		        .UpdateCreatedAtFields()
		        .UpdateUpdatedAtEntities()
		        .UpdateStateEntities();

	        return base.SaveChangesAsync(cancellationToken);
        }
    }
}