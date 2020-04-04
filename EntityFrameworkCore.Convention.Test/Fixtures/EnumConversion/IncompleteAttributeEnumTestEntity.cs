using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCore.Convention.Test.Fixtures.EnumConversion
{
	public class IncompleteAttributeEnumTestEntity
	{
		[Key]
		public long Id { get; set; }

		public IncompleteAttributeEnumTypes Type { get; set; }
	}
}