
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCore.Convention.Test.Fixtures.EnumConversion
{
	public class EnumConversionTestEntity
	{
		[Key]
		public long Id { get; set; }

		public EntityTypes Type { get; set; }
	}
}