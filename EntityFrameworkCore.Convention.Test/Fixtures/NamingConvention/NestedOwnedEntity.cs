using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Test.Fixtures.NamingConvention
{
	[Owned]
	public class NestedOwnedEntity
	{
		public OwnedEntity NestedFieldA { get; set; }
        
		public OwnedEntity NestedFieldB { get; set; }
	}
}