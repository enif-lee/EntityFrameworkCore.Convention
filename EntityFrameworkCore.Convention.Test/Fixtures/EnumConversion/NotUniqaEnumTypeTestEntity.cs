using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCore.Convention.Test.Fixtures.EnumConversion
{
	public class NotUniqaEnumTypeTestEntity
	{
		[Key]
		public long Id { get; set; }
		
		public NotUniqEnumTypes Type { get; set; }
	}
}