using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

		public static ModelBuilder UseSnakeCaseNamingConvention(this ModelBuilder builder)
		{
			// Todo implement not yet;
			builder.Model.GetEntityTypes();

			return builder;
		}

		public static ModelBuilder UseNamingConvention(this ModelBuilder builder, Action<ConventionBuilder> configure)
		{
			var conventionBuilder = new ConventionBuilder();
			configure(conventionBuilder);
			if (!conventionBuilder.Validate(out var message))
				throw new ValidationException($"Failed to validate convention builder(cause : {message})");
			conventionBuilder.Apply(builder);
			return builder;
		}

		public static ChangeTracker UpdateCreatedAtFields(this ChangeTracker tracker)
		{
			return tracker.UpdateCreatedAtFields(DateTime.UtcNow);
		}

		public static ChangeTracker UpdateCreatedAtFields(this ChangeTracker tracker, DateTime now)
		{
			foreach (var entry in tracker.Entries<ICreatedAt>().Where(e => e.State == EntityState.Added))
				entry.Entity.CreatedAt = now;

			return tracker;
		}

		public static ChangeTracker UpdateUpdatedAtEntities(this ChangeTracker tracker)
		{
			return tracker.UpdateUpdatedAtEntities(DateTime.UtcNow);
		}

		public static ChangeTracker UpdateUpdatedAtEntities(this ChangeTracker tracker, DateTime now)
		{
			foreach (var entry in tracker.Entries<IUpdatedAt>().Where(e => e.State == EntityState.Modified))
				entry.Entity.UpdatedAt = now;

			return tracker;
		}

		public static ChangeTracker UpdateStateEntities(this ChangeTracker tracker)
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