using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Audit
{
	public class EntityAuditAttribute : Attribute
	{
		private readonly EntityState[] _states;

		public EntityAuditAttribute(params EntityState[] states)
		{
			_states = states.Length == 0
				? new[] {EntityState.Added, EntityState.Modified, EntityState.Deleted}
				: states;
		}

		public bool IsLoggable(EntityState state)
		{
			return _states.Contains(state);
		}
	}
}