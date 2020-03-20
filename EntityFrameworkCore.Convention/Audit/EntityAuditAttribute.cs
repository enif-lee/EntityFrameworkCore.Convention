using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Audit
{
	/// <summary>
	/// 	Audit logging strategy attribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class EntityAuditAttribute : Attribute
	{
		private readonly EntityState[] _states;

		/// <summary>
		/// 	Entity audit logging attribute
		/// </summary>
		/// <param name="states">Event types for audit logging, Added, Modified, Deleted be contain as default.</param>
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