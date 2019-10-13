using System;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.Convention.Test.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFrameworkCore.Convention.Test.Helpers
{
	public class FieldTestDb : DbContext
	{
		public Action<ChangeTracker> OnSaveChanges { get; set; } = c => { };

		public DbSet<FieldTestEntity> Entities { get; set; }
		
		public FieldTestDb(DbContextOptions options) : base(options)
		{
		}

		public override int SaveChanges()
		{
			OnSaveChanges(ChangeTracker);
			return base.SaveChanges();
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			OnSaveChanges(ChangeTracker);
			return base.SaveChangesAsync(cancellationToken);
		}
	}
}