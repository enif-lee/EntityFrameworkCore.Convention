using System.ComponentModel.DataAnnotations;
using EntityFrameworkCore.Convention.Audit;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Test.Fixtures.Audit
{
	[EntityAudit(EntityState.Added)]
	public class OnlyAddedAuditLoggingEntity
	{
		[Key]
		public long Id { get; set; }

		public string Text { get; set; }
	}
}