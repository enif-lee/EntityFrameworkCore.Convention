using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Castle.DynamicProxy;
using EntityFrameworkCore.Convention.StateExtension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFrameworkCore.Convention.Audit
{
	public static class AuditHelper
	{
		private static readonly IDictionary<EntityState, State> StateMap = new Dictionary<EntityState, State>
		{
			[EntityState.Added] = State.Created,
			[EntityState.Modified] = State.Updated,
			[EntityState.Deleted] = State.Deleted
		};

		public static IEnumerable<Audit> GetAudits<TDbContext>(this ChangeTracker context) where TDbContext : DbContext
		{
			foreach (var entity in context.Entries())
			{
				if (TryGetAuditInfo<DbContext>(entity, out var table))
					continue;

				var keys = entity.Properties
					.Where((entry, i) => entity.IsKeySet)
					.Select(key => (property: key.Metadata.Name, value: key.CurrentValue))
					.ToArray();

				yield return new Audit
				{
					Table = table,
					Key = JsonSerializer.Serialize(keys),
					Snapshot = JsonSerializer.Serialize(entity.Entity),
					Event = EntityFrameworkAuditConfig<TDbContext>.EventValueResolver.Convert(entity.State),
					LoggedAt = DateTime.UtcNow
				};
			}
		}

		public static bool TryGetAuditInfo<TDbContext>(EntityEntry entityEntry, out string table) where TDbContext : DbContext
		{
			var typeMetaCache = EntityFrameworkAuditConfig<TDbContext>.TypeAuditMetaCache;
			var typeTableCache = EntityFrameworkAuditConfig<TDbContext>.TypeTableNameMap;

			var type = entityEntry.Entity.GetType();
			if (typeMetaCache.ContainsKey(type) && typeMetaCache[type] == null)
			{
				table = string.Empty;
				return false;
			}

			// When the dbcontext enable lazy-loading feature, the type is proxy-type.
			if (ProxyUtil.IsProxyType(type) && type.BaseType != null)
				type = type.BaseType;

			table = typeTableCache.GetOrAdd(type, _ => entityEntry.Metadata.GetTableName());
			var meta = typeMetaCache.GetOrAdd(type, _ => type.GetCustomAttribute<EntityAuditAttribute>());
			return meta.IsLoggable(entityEntry.State);
		}
	}
}