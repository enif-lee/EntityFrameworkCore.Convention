using EntityFrameworkCore.Convention.Test.Fixtures.Audit;
using EntityFrameworkCore.Convention.Test.TestHelpers;
using FluentAssertions;
using NUnit.Framework;
using SQLitePCL;

namespace EntityFrameworkCore.Convention.Test
{
	public class AuditLoggingTest
	{
		public SpyAuditProccesor SpyAuditProccesor { get; set; }
		public AuditLoggingTestDb Db { get; set; }

		public AuditLoggingTest()
		{
			SpyAuditProccesor = new SpyAuditProccesor();
			Db = new AuditLoggingTestDb(SpyAuditProccesor, InMemoryConnectionHelper.Generate());
			Db.Database.EnsureCreated();
		}

		[Test]
		public void Test()
		{
			// Given
			var entity = Db.Entities.Add(new AuditLoggingTestEntity
			{
				Text = "Some text"
			}).Entity;
			Db.SaveChanges();
			entity.Text = "Update text";
			Db.SaveChanges();
			Db.Entities.Remove(entity);
			Db.SaveChanges();
			
			// When
			SpyAuditProccesor.Audits.Should().HaveCount(3);
		}
	}
}