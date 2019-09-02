using System;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCore.Convention.Entity
{
	public class Audit
	{
		[Key]
		public long Id { get; set; }

		[Required]
		public string Table { get; set; }

		[Required]
		public string Key { get; set; }

		[Required]
		public string Snapshot { get; set; }

		[Required]
		public State Event { get; set; }

		[Required]
		public DateTime LoggedAt { get; set; }
	}
}