using EntityFrameworkCore.Convention.Audit;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Test.Fixtures.Audit
{
	public class AuditLoggingTestDb : AuditDbContextBase<AuditLoggingTestDb>
	{
		public DbSet<AuditLoggingTestEntity> Entities { get; set; }

		public DbSet<OnlyAddedAuditLoggingEntity> OnlyAddedEntities { get; set; }
		
		public AuditLoggingTestDb(IAuditProcessor<AuditLoggingTestDb> processor, DbContextOptions options) : base(options, processor)
		{
		}
	}
}