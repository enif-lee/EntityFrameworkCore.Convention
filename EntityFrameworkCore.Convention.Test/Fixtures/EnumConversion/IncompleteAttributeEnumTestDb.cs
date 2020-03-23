using EntityFrameworkCore.Convention.EnumConversion;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Test.Fixtures.EnumConversion
{
	public class IncompleteAttributeEnumTestDb : DbContext
	{
		public IncompleteAttributeEnumTestDb(DbContextOptions options) : base(options)
		{
		}

		public DbSet<IncompleteAttributeEnumTestEntity> Entities { get; set; }
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyEnumValueConverter<IncompleteAttributeEnumTypes>();
			base.OnModelCreating(modelBuilder);
		}
	}
}