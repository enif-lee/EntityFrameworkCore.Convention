using System;
using System.Threading.Tasks;
using EntityFrameworkCore.Convention.Helpers;
using EntityFrameworkCore.Convention.Test.Helpers;
using EntityFrameworkCore.Convention.Test.Infrastructure;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NUnit.Framework;

namespace EntityFrameworkCore.Convention.Test
{
	public class FieldConventionTest : IDisposable
	{
		private FieldTestDb _db;

		public FieldConventionTest()
		{
			_db = CreateTestDb();
		}

		private FieldTestDb CreateTestDb(Action<ChangeTracker> changeTrack = null, Action<ModelBuilder> modelBuilder = null)
		{
			var connection = new SqliteConnection("Data Source=:memory:");
			connection.Open();
			var options = new DbContextOptionsBuilder()
				.UseSqlite(connection)
				.Options;
			var db = new FieldTestDb(options, changeTrack, modelBuilder);
			db.Database.EnsureCreated();
			return db;
		}

		[Test]
		public async Task UpdateCreatedAtFields_Should_BeUpdatedCreatedAtField_When_EntityIsCreated()
		{
			// Given
			_db.OnSaveChanges = c => c.UpdateCreatedAtFields();

			// When
			var entityEntry = _db.Entities.Add(new FieldTestEntity());
			await _db.SaveChangesAsync();

			// Then
			var entity = entityEntry.Entity;
			entity.CreatedAt.Should().NotBe(default);
		}
		
		[Test]
		public async Task UpdateUpdatedAtFields_Should_BeUpdatedUpdatedAtField_When_EntityIsCreated()
		{
			// Given
			_db.OnSaveChanges = c => c.UpdateUpdatedAtFields();

			// When
			var entityEntry = _db.Entities.Add(new FieldTestEntity());
			await _db.SaveChangesAsync();

			// Then
			var entity = entityEntry.Entity;
			entity.UpdatedAt.Should().NotBe(default);
		}		
		
		[Test]
		public async Task UpdateUpdatedAtFields_Should_BeUpdatedUpdatedAtField_When_EntityIsModified()
		{
			// Given
			_db.OnSaveChanges = c => c.UpdateUpdatedAtFields();

			// When
			var entity = new FieldTestEntity();
			_db.Entities.Add(entity);
			await _db.SaveChangesAsync();

			var whenCreated = entity.UpdatedAt;
			
			await Task.Delay(100);

			entity.Text = "new text";
			await _db.SaveChangesAsync();

			// Then
			entity.UpdatedAt.Should().BeAfter(whenCreated);
		}

		[Test]
		public async Task UpdateStateEntities_Should_BeSetStateFieldAsCreated_When_EntityIsAdded()
		{
			// Given
			_db.OnSaveChanges = c => c.UpdateStateFields();

			// When
			var entity = new FieldTestEntity();
			_db.Entities.Add(entity);
			await _db.SaveChangesAsync();

			// Then
			entity.State.Should().Be(State.Created);
		}
		
		[Test]
		public async Task UpdateStateEntities_Should_BeSetStateFieldAsUpdated_When_EntityIsUpdated()
		{
			// Given
			_db.OnSaveChanges = c => c.UpdateStateFields();

			// When
			var entity = new FieldTestEntity();
			_db.Entities.Add(entity);
			await _db.SaveChangesAsync();

			entity.Text = "new value";
			await _db.SaveChangesAsync();
			
			// Then
			entity.State.Should().Be(State.Updated);
		}
		
		[Test]
		public async Task UpdateStateEntities_Should_BeSetStateFieldAsDeleted_When_EntityIsDeleted()
		{
			// Given
			_db.OnSaveChanges = c => c.UpdateStateFields();

			// When
			var entity = new FieldTestEntity();
			_db.Entities.Add(entity);
			await _db.SaveChangesAsync();

			_db.Entities.Remove(entity);
			await _db.SaveChangesAsync();
			
			// Then
			entity.State.Should().Be(State.Deleted);
		}

		[Test]
		public async Task UpdateStateEntities_Should_BeIgnoredDeletedEntitiesByDefault_EntityIsDeleted()
		{
			// Given
			var db = CreateTestDb(c => c.UpdateStateFields(), b => b.ApplyIgnoreDeletedStateEntitiesFromQuery());

			// When
			var entity = new FieldTestEntity();
			db.Entities.Add(entity);
			await db.SaveChangesAsync();

			db.Entities.Remove(entity);
			await db.SaveChangesAsync();

			// Then
			var count = await db.Entities.CountAsync();
			count.Should().Be(0);
		}

		public void Dispose()
		{
			_db?.Dispose();
		}
	}
}