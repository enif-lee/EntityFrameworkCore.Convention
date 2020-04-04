using EntityFrameworkCore.Convention.Test.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EntityFrameworkCore.Convention.Test.TestHelpers
{
	public static class InMemoryConnectionHelper
	{
		public static DbContextOptions Generate()
		{
			var connection = new SqliteConnection("Data Source=:memory:");
			connection.Open();
			return new DbContextOptionsBuilder()
				.UseSqlite(connection)
				.UseInternalServiceProvider(new ServiceCollection()
					.AddEntityFrameworkSqlite()
					.Replace(ServiceDescriptor.Singleton<IModelSource, UncachedModelSource>())
					.BuildServiceProvider())
				.Options;
		}
	}
}