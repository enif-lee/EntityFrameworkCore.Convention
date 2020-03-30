using System.ComponentModel.DataAnnotations;
using EntityFrameworkCore.Convention.Audit;

namespace EntityFrameworkCore.Convention.Test.Fixtures.Audit
{
	[EntityAudit]
	public class AuditLoggingTestEntity
	{
		[Key]
		public long Id { get; set; }

		public string Text { get; set; }
	}
}