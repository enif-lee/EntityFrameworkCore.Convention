using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCore.Convention.Test.Fixture.Entities
{
    public class TestEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public int Age { get; set; }

        public OwnedEntity OwnedA { get; set; }

        public OwnedEntity OwnedB { get; set; }

        public NestedOwnedEntity NestedField { get; set; }
    }
}