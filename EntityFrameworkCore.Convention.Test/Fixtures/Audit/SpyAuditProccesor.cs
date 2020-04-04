using System.Collections.Generic;
using System.Threading.Tasks;
using EntityFrameworkCore.Convention.Audit;

namespace EntityFrameworkCore.Convention.Test.Fixtures.Audit
{
	public class SpyAuditProccesor : IAuditProcessor<AuditLoggingTestDb>
	{
		public ICollection<Convention.Audit.Audit> Audits { get; set; } = new List<Convention.Audit.Audit>();

		public Task HandleAuditsAsync(IEnumerable<Convention.Audit.Audit> audits)
		{
			foreach (var audit in audits)
			{
				Audits.Add(audit);
			}

			return Task.CompletedTask;
		}
	}
}