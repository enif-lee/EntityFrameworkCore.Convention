using System;
using EntityFrameworkCore.Convention.Helpers;
using EntityFrameworkCore.Convention.Naming;
using EntityFrameworkCore.Convention.Test.Fixture.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Test.Fixture
{
	internal class TestDb : DbContext
	{
		private readonly Action<ConventionBuilder> _builder;

		public TestDb(DbContextOptions options, Action<ConventionBuilder> builder) : base(options)
		{
			_builder = builder;
		}

		public DbSet<TestEntity> TestEntities { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.UseNamingConvention(builder => _builder?.Invoke(builder));
		}
	}
}