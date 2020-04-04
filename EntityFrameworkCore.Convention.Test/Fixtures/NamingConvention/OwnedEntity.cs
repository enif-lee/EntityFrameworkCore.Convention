using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Test.Fixtures.NamingConvention
{
    [Owned]
    public class OwnedEntity
    {
        public string Code { get; set; }

        public string Value { get; set; }
    }
}