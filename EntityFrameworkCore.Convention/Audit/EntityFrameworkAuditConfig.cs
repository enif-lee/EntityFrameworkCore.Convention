using System;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Audit
{
	internal static class EntityFrameworkAuditConfig<TDbContext> where TDbContext : DbContext
	{
		/// <summary>
		///     엔티티 타입의 Attribute 존재 여부 캐싱을 위한 Map
		/// </summary>
		internal static ConcurrentDictionary<Type, EntityAuditAttribute> TypeAuditMetaCache { get; } = new ConcurrentDictionary<Type, EntityAuditAttribute>();

		/// <summary>
		///     엔티티 타입에 대한 데이터베이스 내 테이블 이름 Map
		/// </summary>
		internal static ConcurrentDictionary<Type, string> TypeTableNameMap { get; } = new ConcurrentDictionary<Type, string>();

		public static Type DbContextType { get; } = typeof(TDbContext);

		public static IAuditEventValueResolver EventValueResolver { get; set; } = new DefaultAuditEventNameResolver();
	}
}