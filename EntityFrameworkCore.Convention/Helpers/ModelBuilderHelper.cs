using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFrameworkCore.Convention.Helpers
{
	public static class ModelBuilderHelper
	{
		private static readonly IDictionary<EntityState, State> StateMap = new Dictionary<EntityState, State>
		{
			[EntityState.Added] = State.Created,
			[EntityState.Modified] = State.Updated,
			[EntityState.Deleted] = State.Deleted
		};

		/// <summary>
		/// 	Configure naming convention for entity frameworks.
		/// </summary>
		/// <param name="builder">Model builder of entity entity framework</param>
		/// <param name="configure">Convention configure setter</param>
		/// <returns>Origin model builder</returns>
		public static ModelBuilder UseNamingConvention(this ModelBuilder builder, Action<ConventionBuilder> configure)
		{
			var conventionBuilder = new ConventionBuilder();
			configure(conventionBuilder);

			conventionBuilder.Apply(builder);
			return builder;
		}

		/// <summary>
		/// 	Apply specific naming convention for all settable name file of entities.
		/// </summary>
		/// <param name="builder">Model Builder</param>
		/// <param name="convention">Convention</param>
		/// <returns>origin model builder</returns>
		public static ModelBuilder UseNamingConvention(this ModelBuilder builder, INamingConvention convention)
		{
			return builder.UseNamingConvention(cb => cb.UseNamingConvention(convention));
		}

		/// <summary>
		/// 	Update CreatedAt field to UtcNow of entities that implement ICreatedAt interface.
		/// </summary>
		/// <param name="tracker"></param>
		/// <returns></returns>
		public static ChangeTracker UpdateCreatedAtFields(this ChangeTracker tracker)
		{
			return tracker.UpdateCreatedAtFields(DateTime.UtcNow);
		}

		/// <summary>
		/// 	Update CreatedAt field to provided datetime
		/// </summary>
		/// <param name="tracker"></param>
		/// <param name="now"></param>
		/// <returns></returns>
		public static ChangeTracker UpdateCreatedAtFields(this ChangeTracker tracker, DateTime now)
		{
			foreach (var entry in tracker.Entries<ICreatedAt>().Where(e => e.State == EntityState.Added))
				entry.Entity.CreatedAt = now;

			return tracker;
		}

		/// <summary>
		/// 	Update UpdatedAt field to UtcNow
		/// </summary>
		/// <param name="tracker"></param>
		/// <returns></returns>
		/// <remarks>This is only working with modified entities.</remarks>
		public static ChangeTracker UpdateUpdatedAtFields(this ChangeTracker tracker)
		{
			return tracker.UpdateUpdatedAtFields(DateTime.UtcNow);
		}

		/// <summary>
		/// 	Update UpdatedAtFields to provided DateTime
		/// </summary>
		/// <param name="tracker"></param>
		/// <param name="now"></param>
		/// <returns></returns>
		/// <remarks>This is only working with modified entities.</remarks>
		public static ChangeTracker UpdateUpdatedAtFields(this ChangeTracker tracker, DateTime now)
		{
			foreach (var entry in tracker.Entries<IUpdatedAt>().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added))
				entry.Entity.UpdatedAt = now;

			return tracker;
		}

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