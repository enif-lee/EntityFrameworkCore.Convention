using System.Linq;
using EntityFrameworkCore.Convention.Test.Fixture;
using EntityFrameworkCore.Convention.Test.Fixture.Entities;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EntityFrameworkCore.Convention.Test
{
	public class TableNamingConventionTest
	{
		private readonly DbContextOptions _options;

		public TableNamingConventionTest()
		{
			var connection = new SqliteConnection("Data Source=:memory:");
			connection.Open();
			_options = new DbContextOptionsBuilder()
				.UseSqlite(connection)
				.Options;
		}

		[Test]
		public void UseTableNamingConvention_Should_ApplyNamingConventionForTableName_When_ParticularNamingConventionIsSpecified()
		{
			// Given
			var testDb = new TestDb(_options, builder => builder
				.UseTableNamingConvention(NamingConvention.LowerSnakeCase));

			// When
			var model = testDb.Model.FindEntityType(typeof(TestEntity));

			// Then
			model.GetTableName().Should().Be("test_entities");
		}

		[Test]
		public void UseColumnNamingConvention_Should_ApplyNamingConventionForTableNameForNestedOwnedType_When_ParticularNamingConventionIsSpecified()
		{
			// Given
			var db = new TestDb(_options, builder => builder.UseColumnNamingConvention(NamingConvention.LowerSnakeCase));

			// When
			var model = db.Model.FindEntityType(typeof(TestEntity));

			// Then

			db.Model
				.GetEntityTypes()
				.Where(e => e.ClrType == typeof(OwnedEntity))
				.SelectMany(p => p.GetProperties())
				.Where(p => !p.IsPrimaryKey() && p.DeclaringEntityType.DefiningEntityType.ClrType == typeof(NestedOwnedEntity))
				.Select(p => p.GetColumnName())
				.First()
				.Should().Be("nested_field_a_nested_field_a_code");
		}
	}
}