using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Linq.Expressions.Expression;

namespace EntityFrameworkCore.Convention.StateExtension
{
	public static class StateHelper
	{
		///  <summary>
		/// 		Apply ignore logical delete rows for all entity types.
		///  </summary>
		///  <param name="builder"></param>
		///  <param name="excludeEntities">exclude types from entities.</param>
		///  <returns></returns>
		public static ModelBuilder ApplyIgnoreDeletedStateEntitiesFromQuery(this ModelBuilder builder, IEnumerable<Type> excludeEntities = null)
		{
			var excludeTypes = excludeEntities?.ToArray() ?? Enumerable.Empty<Type>().ToArray();
			foreach (var entity in builder.Model.GetEntityTypes())
			{
				var entityType = entity.ClrType;
				if (!entityType.GetInterfaces().Contains(typeof(IState)))
					continue;

				if (excludeTypes.Any() && excludeTypes.Contains(entityType))
					continue;

				var parameter = Parameter(entityType);
				var status = PropertyOrField(parameter, nameof(IState.State));

				entity.SetQueryFilter(Lambda(NotEqual(status, Constant(State.Deleted)), parameter));
			}

			return builder;
		}

		private static readonly IDictionary<EntityState, State> StateMap = new Dictionary<EntityState, State>
		{
			[EntityState.Added] = State.Created,
			[EntityState.Modified] = State.Updated,
			[EntityState.Deleted] = State.Deleted
		};

		/// <summary>
		/// 	Update state field value by entity's state.
		/// </summary>
		/// <param name="tracker"></param>
		/// <returns></returns>
		public static ChangeTracker UpdateStateFields(this ChangeTracker tracker)
		{
			foreach (var entry in tracker.Entries<IState>().Where(e => StateMap.ContainsKey(e.State)))
			{
				entry.Entity.State = StateMap[entry.State];

				if (entry.State == EntityState.Deleted)
					entry.State = EntityState.Modified;
			}

			return tracker;
		}
	}
}