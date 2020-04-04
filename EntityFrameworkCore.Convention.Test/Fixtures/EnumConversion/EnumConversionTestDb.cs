using EntityFrameworkCore.Convention.EnumConversion;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Test.Fixtures.EnumConversion
{
	public class EnumConversionTestDb : DbContext
	{
		public EnumConversionTestDb(DbContextOptions options) : base(options)
		{
		}

		public DbSet<EnumConversionTestEntity> Entities { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyEnumValueConverter<EntityTypes>();
			base.OnModelCreating(modelBuilder);
		}
	}
}