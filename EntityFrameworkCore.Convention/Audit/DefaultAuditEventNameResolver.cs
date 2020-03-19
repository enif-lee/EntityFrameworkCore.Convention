using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Audit
{
	public class DefaultAuditEventNameResolver : IAuditEventValueResolver
	{
		public string Convert(EntityState state)
		{
			return state switch
			{
				EntityState.Added => "Added",
				EntityState.Modified => "Modified",
				EntityState.Deleted => "Deleted",
				EntityState.Detached => "Detached",
				EntityState.Unchanged => "Unchanged",
			};
		}
	}
}