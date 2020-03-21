namespace EntityFrameworkCore.Convention.Test.Fixtures.EnumConversion
{
	public enum EntityTypes
	{
		[EnumValue("A_Persist")]
		A,
		
		[EnumValue("B_Persist")]
		B
	}
}