using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Audit
{
	public abstract class AuditDbContextBase<TDbContext> : DbContext where TDbContext : DbContext
	{
		protected AuditDbContextBase()
		{
		}

		protected AuditDbContextBase(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Audit> Audits { get; set; }

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			SaveAudits();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
		{
			SaveAudits();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		private void SaveAudits()
		{
			foreach (var audit in ChangeTracker.GetAudits<TDbContext>())
			{
				Audits.Add(audit);
			}
		}
	}
}