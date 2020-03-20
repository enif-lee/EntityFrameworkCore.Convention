using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFrameworkCore.Convention.WriteTime
{
	public static class WriteTimeHelper
	{
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

		/// <summary>e
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
	}
}