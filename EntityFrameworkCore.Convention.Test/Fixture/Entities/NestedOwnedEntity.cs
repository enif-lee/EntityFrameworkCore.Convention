using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Test.Fixture.Entities
{
    [Owned]
    public class NestedOwnedEntity
    {
        public OwnedEntity NestedField { get; set; }
    }
}