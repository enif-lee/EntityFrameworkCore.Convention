using EntityFrameworkCore.Convention.Test.Fixtures.EnumConversion;
using EntityFrameworkCore.Convention.Test.TestHelpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EntityFrameworkCore.Convention.Test
{
	public class EnumConversionTest
	{
		private EnumConversionTestDb _db;

		public EnumConversionTest()
		{
			var options = InMemoryConnectionHelper.Generate();
			_db = new EnumConversionTestDb(options);
		}

		[Test] public void ApplyEnumValueConverter_Should_ProvideEnumValueConverterToProperty_When_EnumClassesValueAndMemberAreUniqueEachOther()
		{
			// Given
			// When
			var entity = _db.Model.FindEntityType(typeof(EnumConversionTestEntity));
			var valueConverter = entity.FindProperty(nameof(EnumConversionTestEntity.Type)).GetValueConverter();

			// Then
			var toProvider = valueConverter.ConvertToProvider;
			var fromProvider = valueConverter.ConvertFromProvider;
			toProvider(EntityTypes.A).Should().Be("A_Persist");
			toProvider(EntityTypes.B).Should().Be("B_Persist");
			fromProvider("A_Persist").Should().Be(EntityTypes.A);
			fromProvider("B_Persist").Should().Be(EntityTypes.B);
		}
	}
}