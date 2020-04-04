using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.Convention.StateExtension;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Test.Fixtures.StateExtension
{
	public class EntityStateDb : DbContext
	{
		private readonly bool _ignoreLogicalDeletedEntities;

		public EntityStateDb(DbContextOptions options, bool ignoreLogicalDeletedEntities = true) : base(options)
		{
			_ignoreLogicalDeletedEntities = ignoreLogicalDeletedEntities;
		}

		public DbSet<StateTestEntity> Entities { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			if (_ignoreLogicalDeletedEntities)
				modelBuilder.ApplyIgnoreDeletedStateEntitiesFromQuery();
			
			base.OnModelCreating(modelBuilder);
		}

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			ChangeTracker.UpdateStateFields();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
		{
			ChangeTracker.UpdateStateFields();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}
	}
}