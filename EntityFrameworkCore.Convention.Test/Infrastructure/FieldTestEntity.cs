﻿using System;
using System.ComponentModel.DataAnnotations;
using EntityFrameworkCore.Convention.StateExtension;
using EntityFrameworkCore.Convention.WriteTime;

namespace EntityFrameworkCore.Convention.Test.Infrastructure
{
	public class FieldTestEntity : ICreatedAt, IUpdatedAt, IState
	{
		[Key]
		public int Id { get; set; }

		public string Text { get; set; }
		
		public DateTime CreatedAt { get; set; }
		
		public DateTime UpdatedAt { get; set; }
		
		public State State { get; set; }
	}
}