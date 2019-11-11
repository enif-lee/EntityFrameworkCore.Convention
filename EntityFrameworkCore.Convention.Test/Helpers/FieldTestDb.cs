﻿using System;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.Convention.Test.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFrameworkCore.Convention.Test.Helpers
{
	public class FieldTestDb : DbContext
	{
		public FieldTestDb(DbContextOptions options,
			Action<ChangeTracker> changeTrack = null,
			Action<ModelBuilder> modelBuilder = null) : base(options)
		{
			OnSaveChanges ??= changeTrack;
			OnModelBuilder ??= modelBuilder;
		}

		public Action<ChangeTracker> OnSaveChanges { get; set; } = c => { };

		public Action<ModelBuilder> OnModelBuilder { get; set; } = c => { };

		public DbSet<FieldTestEntity> Entities { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			OnModelBuilder(modelBuilder);
		}

		public override int SaveChanges()
		{
			OnSaveChanges(ChangeTracker);
			return base.SaveChanges();
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			OnSaveChanges(ChangeTracker);
			return base.SaveChangesAsync(cancellationToken);
		}
	}
}