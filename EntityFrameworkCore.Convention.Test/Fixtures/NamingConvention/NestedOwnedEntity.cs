using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Convention.Test.Fixture.Entities
{
    [Owned]
    public class NestedOwnedEntity
    {
        public OwnedEntity NestedFieldA { get; set; }
        
        public OwnedEntity NestedFieldB { get; set; }
    }
}