using System.Linq;
using EntityFrameworkCore.Convention.StateExtension;
using EntityFrameworkCore.Convention.Test.Fixtures.StateExtension;
using EntityFrameworkCore.Convention.Test.TestHelpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EntityFrameworkCore.Convention.Test
{
	public class EntityStateTest
	{
		private DbContextOptions _options;

		public EntityStateTest()
		{
			_options = InMemoryConnectionHelper.Generate();
		}

		[Test]
		public void UpdateStateFields_Should_SetStateFieldAsCreated_When_EntityIsAdded()
		{
			// Given
			using var db = CreateDb(true);
			
			// When
			var entity = new StateTestEntity();
			db.Entities.Add(entity);
			db.SaveChanges();
			
			// When
			entity.State.Should().Be(State.Created);
		}
		
		[Test]
		public void UpdateStateFields_Should_SetStateFieldAsUpdated_When_EntityIsModified()
		{
			// Given
			using var db = CreateDb(true);
			var entity = new StateTestEntity();
			db.Entities.Add(entity);
			db.SaveChanges();
			
			// When
			entity.Message = "update field";
			db.SaveChanges();
			
			// When
			entity.State.Should().Be(State.Updated);
		}

		[Test]
		public void UpdateStateFields_Should_SetStateFieldAsDeleted_When_EntityIsRemoved()
		{
			// Given
			using var db = CreateDb(true);
			var entity = new StateTestEntity();
			db.Entities.Add(entity);
			db.SaveChanges();
			
			// When
			db.Entities.Remove(entity);
			db.SaveChanges();

			// When
			entity.State.Should().Be(State.Deleted);
		}

		
		[Test]
		public void ApplyIgnoreDeletedStateEntitiesFromQuery_Should_BeQueriedDeletedEntities_When_ExtensionMethodIsNotCalled()
		{
			// Given
			using var db = CreateDb(false);
			var entity = new StateTestEntity();
			db.Entities.Add(entity);
			db.SaveChanges();
			
			// When
			db.Entities.Remove(entity);
			db.SaveChanges();

			// When
			db.Entities.Count().Should().Be(1);
		}
		
		[Test]
		public void ApplyIgnoreDeletedStateEntitiesFromQuery_Should_IghnoreFromQuery_When_EmtotuIsRemoved()
		{
			// Given
			using var db = CreateDb(true);
			var entity = new StateTestEntity();
			db.Entities.Add(entity);
			db.SaveChanges();
			
			// When
			db.Entities.Remove(entity);
			db.SaveChanges();

			// When
			db.Entities.Count().Should().Be(0);
		}

		public EntityStateDb CreateDb(bool ignoreLogicalDeletedEntities)
		{
			var db = new EntityStateDb(_options, ignoreLogicalDeletedEntities);
			db.Database.EnsureCreated();
			return db;
		}
	}
}