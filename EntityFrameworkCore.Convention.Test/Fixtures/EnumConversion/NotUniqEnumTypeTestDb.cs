using EntityFrameworkCore.Convention.EnumConversion;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Test.Fixtures.EnumConversion
{
	public class NotUniqEnumTypeTestDb : DbContext
	{
		public NotUniqEnumTypeTestDb(DbContextOptions options) : base(options)
		{
		}

		public DbSet<NotUniqaEnumTypeTestEntity> Entities { get; set; }
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyEnumValueConverter<NotUniqEnumTypes>();
			base.OnModelCreating(modelBuilder);
		}
	}
}