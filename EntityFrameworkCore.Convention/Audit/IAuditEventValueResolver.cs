using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Audit
{
	public interface IAuditEventValueResolver
	{
		public string Convert(EntityState state);
	}
}