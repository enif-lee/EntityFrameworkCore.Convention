using System.Linq;
using EntityFrameworkCore.Convention.Naming;
using EntityFrameworkCore.Convention.Test.Fixtures.NamingConvention;
using EntityFrameworkCore.Convention.Test.TestHelpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EntityFrameworkCore.Convention.Test
{
	public class TableNamingConventionTest
	{
		private readonly DbContextOptions _options;

		public TableNamingConventionTest()
		{
			_options = InMemoryConnectionHelper.Generate();
		}

		[Test]
		public void UseTableNamingConvention_Should_ApplyNamingConventionForTableName_When_ParticularNamingConventionIsSpecified()
		{
			// Given
			using var testDb = new TestDb(_options, builder => builder
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
			using var db = new TestDb(_options, builder => builder.UseColumnNamingConvention(NamingConvention.LowerSnakeCase));

			// When
			var model = db.Model.FindEntityType(typeof(TestEntity));

			// Then
			db.Model
				.GetEntityTypes()
				.Where(e => e.ClrType == typeof(OwnedEntity))
				.SelectMany(p => p.GetProperties())
				.Where(p => !p.IsPrimaryKey() && p.DeclaringEntityType.DefiningEntityType.ClrType == typeof(NestedOwnedEntity) && p.Name == "Code")
				.Select(p => p.GetColumnName())
				.First()
				.Should().Be("nested_field_a_nested_field_a_code");
		}

		[Test]
		public void UseGlobalTablePrefix_Should_AttachPrefixToTableName_When_GlobalTablePrefixIsConfigured()
		{
			// Given
			using var db = new TestDb(_options, builder => builder
				.UseTableNamingConvention(NamingConvention.LowerSnakeCase)
				.UseGlobalTablePrefix("Cvtn"));

			// Then
			var model = db.Model.FindEntityType(typeof(TestEntity));

			// Then
			model.GetTableName().Should().StartWith("cvtn_");
		}

		[Test]
		public void UseGlobalTableSuffix_Should_AttachSuffixToTableName_When_GlobalTableSuffixIsConfigured()
		{
			// Given
			using var db = new TestDb(_options, builder => builder
				.UseTableNamingConvention(NamingConvention.LowerSnakeCase)
				.UseGlobalTableSuffix("tbl"));

			// Then
			var model = db.Model.FindEntityType(typeof(TestEntity));

			// Then
			model.GetTableName().Should().EndWith("_tbl");
		}


		[Test]
		public void UseGlobalColumnPrefix_Should_AttachPrefixToColumnName_When_GlobalColumnPrefixIsConfigured()
		{
			// Given
			using var db = new TestDb(_options, builder => builder
				.UseGlobalColumnPrefix("c"));

			// Then
			var model = db.Model.FindEntityType(typeof(TestEntity));
			var columnName = model.GetProperties().First().GetColumnName();

			// Then
			columnName.Should().StartWith("C");
		}

		[Test]
		public void UseGlobalColumnPrefixAsAlphabetOfEachWordsFromEntityName_Should_AttachPrefixToColumnName_When_SetupIsConfigured()
		{
			// Given
			using var db = new TestDb(_options, builder => builder
				.UseGlobalColumnPrefixAsAlphabetOfEachWordsFromEntityName(2));

			// Then
			var model = db.Model.FindEntityType(typeof(TestEntity));
			var columnName = model.GetProperties().First().GetColumnName();

			// Then
			columnName.Should().StartWith("Te");
		}

		[Test]
		public void UseGlobalTablePrefix_Should_AttachPrefixToColumnNameFromEntityType()
		{
			// Given
			using var db = new TestDb(_options, builder => builder
				.UseGlobalTablePrefix(type => new string(type.ClrType.Name.Take(3).ToArray())));

			// Then
			var model = db.Model.FindEntityType(typeof(TestEntity));

			// Then
			model.GetTableName().Should().StartWith("TesTestEntities");
		}

		[Test]
		public void UseGlobalTableSuffix_Should_AttachSuffixToColumnNameFromEntityType()
		{
			// Given
			using var db = new TestDb(_options, builder => builder
				.UseGlobalTableSuffix(type => new string(type.ClrType.Name.Take(3).ToArray())));

			// Then
			var model = db.Model.FindEntityType(typeof(TestEntity));

			// Then
			model.GetTableName().Should().StartWith("TestEntitiesTes");
		}
		
		// Todo add attributes tests 
	}
}