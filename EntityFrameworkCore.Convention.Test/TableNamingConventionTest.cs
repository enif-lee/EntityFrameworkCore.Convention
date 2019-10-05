using EntityFrameworkCore.Convention.Helpers;
using EntityFrameworkCore.Convention.Test.Fixture.Entities;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EntityFrameworkCore.Convention.Test
{
	public class TableNamingConventionTest
	{
		private DbContextOptions<TestDb> _options;

		public class TestDb : DbContext
		{
			public TestDb(DbContextOptions options) : base(options)
			{
			}

			public DbSet<TestEntity> TestEntities { get; set; }

			protected override void OnModelCreating(ModelBuilder modelBuilder)
			{
				modelBuilder.UseNamingConvention(builder =>
					builder.UseTableNamingConvention(NamingConvention.LowerSnakeCase));
			}
		}

		public TableNamingConventionTest()
		{
			var connection = new SqliteConnection("Data Source=:memory:");
			connection.Open();
			_options = new DbContextOptionsBuilder<TestDb>()
				.UseSqlite(connection)
				.Options;
		}


		[Test]
		public void Test()
		{
			// Given
			var testDb = new TestDb(_options);

			// When
			var model = testDb.Model.FindEntityType(typeof(TestEntity));

			// Then
			model.GetTableName().Should().Be("test_entity");
		}
	}
}