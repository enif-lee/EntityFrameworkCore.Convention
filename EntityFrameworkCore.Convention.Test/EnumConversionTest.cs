using System;
using EntityFrameworkCore.Convention.Test.Fixtures.EnumConversion;
using EntityFrameworkCore.Convention.Test.TestHelpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EntityFrameworkCore.Convention.Test
{
	public class EnumConversionTest
	{
		private DbContextOptions _options;

		public EnumConversionTest()
		{
			_options = InMemoryConnectionHelper.Generate();
		}

		[Test] 
		public void ApplyEnumValueConverter_Should_ProvideEnumValueConverterToProperty_When_EnumClassesValueAndMemberAreUniqueEachOther()
		{
			// Given
			var db = new EnumConversionTestDb(_options);
			
			// When
			var entity = db.Model.FindEntityType(typeof(EnumConversionTestEntity));
			var valueConverter = entity.FindProperty(nameof(EnumConversionTestEntity.Type)).GetValueConverter();

			// Then
			var toProvider = valueConverter.ConvertToProvider;
			var fromProvider = valueConverter.ConvertFromProvider;
			toProvider(EntityTypes.A).Should().Be("A_Persist");
			toProvider(EntityTypes.B).Should().Be("B_Persist");
			fromProvider("A_Persist").Should().Be(EntityTypes.A);
			fromProvider("B_Persist").Should().Be(EntityTypes.B);
		}

		[Test]
		public void
			ApplyEnumValueConverter_Should_ThrowInvalidOperationException_When_PartOfEnumMemberDoesNotHaveEnumValueAttribute()
		{
			// Given
			// When
			Action action = () =>
			{
				var db = new IncompleteAttributeEnumTestDb(_options);
				db.Entities.Add(new IncompleteAttributeEnumTestEntity {Type = IncompleteAttributeEnumTypes.A});
			};
			
			// Then
			action.Should().Throw<InvalidOperationException>();
		}

		[Test]
		public void ApplyEnumValueonverter_ThrowInvalidOperationException_When_SomeEnumMemberValuesAreDuplicated()
		{
			// Given
			// When
			Action action = () =>
			{
				var db = new NotUniqEnumTypeTestDb(_options);
				db.Entities.Add(new NotUniqaEnumTypeTestEntity {Type = NotUniqEnumTypes.A});
			};
			
			// Then
			action.Should().Throw<InvalidOperationException>();
		}
	}
}