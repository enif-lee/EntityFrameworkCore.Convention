using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Linq.Expressions.Expression;

namespace EntityFrameworkCore.Convention.Helpers
{
	public static class ModelBuilderHelper
	{
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
	}
}