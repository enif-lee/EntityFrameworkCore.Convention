namespace EntityFrameworkCore.Convention.Test.Fixtures.EnumConversion
{
	public enum NotUniqEnumTypes
	{
		[EnumValue("NOT_UNIQ")]
		A,
		
		[EnumValue("NOT_UNIQ")]
		B
	}
}