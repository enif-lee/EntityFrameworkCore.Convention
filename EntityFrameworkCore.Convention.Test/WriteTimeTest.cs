using System;
using System.Threading.Tasks;
using EntityFrameworkCore.Convention.Test.Fixtures.WriteTime;
using EntityFrameworkCore.Convention.Test.TestHelpers;
using FluentAssertions;
using NUnit.Framework;

namespace EntityFrameworkCore.Convention.Test
{
	public class WriteTimeTest
	{
		public WriteTimeTest()
		{
			Db = new WriteTimeTestDb(InMemoryConnectionHelper.Generate());
			Db.Database.EnsureCreated();
		}

		public WriteTimeTestDb Db { get; set; }

		[Test]
		public void UpdateWriteTimeFields_Should_UpdateCreatedAndUpdatedAtField_When_EntityIsAdded()
		{
			// Given
			var entity = new WriteTimeTestEntity
			{
				Message = "Hello"
			};
			
			// When
			Db.Entities.Add(entity);
			Db.SaveChanges();
			
			// Then
			entity.CreatedAt.Should().NotBe(default);
			entity.UpdatedAt.Should().NotBe(default);
		}
		
		[Test]
		public async Task UpdateWriteTimeFields_Should_UpdateOnlyUpdatedAtField_When_EntityIsModified()
		{
			// Given
			var entity = new WriteTimeTestEntity
			{
				Message = "Hello"
			};
			
			// When
			Db.Entities.Add(entity);
			Db.SaveChanges();
			
			await Task.Delay(100);
			entity.Message = "Modified Message";
			Db.SaveChanges();
			
			// Then
			entity.UpdatedAt.Should().BeAfter(entity.CreatedAt); 
		}
	}
}