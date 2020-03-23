namespace EntityFrameworkCore.Convention.Test.Fixtures.EnumConversion
{
	public enum IncompleteAttributeEnumTypes
	{
		A,
		
		[EnumValue("B")]
		B
	}
}