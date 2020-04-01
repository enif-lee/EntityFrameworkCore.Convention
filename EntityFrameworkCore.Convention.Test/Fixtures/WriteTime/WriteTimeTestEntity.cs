using System;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.Convention.WriteTime;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Test.Fixtures.WriteTime
{
	public class WriteTimeTestEntity : ICreatedAt, IUpdatedAt
	{
		public long Id { get; set; }
		public string Message { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
	}

	public class WriteTimeTestDb : DbContext
	{
		public WriteTimeTestDb(DbContextOptions options) : base(options)
		{
		}

		public DbSet<WriteTimeTestEntity> Entities { get; set; }

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			ChangeTracker.UpdateWriteTimeFields();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
		{
			ChangeTracker.UpdateWriteTimeFields();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}
	}
}