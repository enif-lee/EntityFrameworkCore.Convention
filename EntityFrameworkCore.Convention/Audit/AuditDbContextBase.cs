using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Audit
{
	/// <summary>
	/// 	Base db context for processing audit logs.
	/// </summary>
	/// <typeparam name="TDbContext"></typeparam>
	public abstract class AuditDbContextBase<TDbContext> : DbContext where TDbContext : DbContext
	{
		private readonly IAuditProcessor<TDbContext> _processor;

		protected AuditDbContextBase(IAuditProcessor<TDbContext> processor)
		{
			_processor = processor;
		}

		protected AuditDbContextBase(DbContextOptions options, IAuditProcessor<TDbContext> processors) : base(options)
		{
			_processor = processors;
		}

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			HandleAuditsAsync().Wait();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
			CancellationToken cancellationToken = new CancellationToken())
		{
			await HandleAuditsAsync();
			return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		private async Task HandleAuditsAsync()
		{
			var audits = ChangeTracker.GetAudits<TDbContext>().ToArray();
			await _processor.HandleAuditsAsync(audits).ConfigureAwait(false);
		}
	}
}