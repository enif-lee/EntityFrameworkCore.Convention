using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Audit
{
	public interface IAuditProcessor<TDbContext> where TDbContext : DbContext
	{
		Task HandleAuditsAsync(IEnumerable<Audit> audits);
	}
}