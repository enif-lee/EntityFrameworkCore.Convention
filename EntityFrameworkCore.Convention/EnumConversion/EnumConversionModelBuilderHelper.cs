using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntityFrameworkCore.Convention.EnumConversion
{
	public static class EnumConversionModelBuilderHelper
	{
		/// <summary>
		/// 	Apply enum to string value conversion.
		/// </summary>
		/// <param name="modelBuilder"></param>
		/// <typeparam name="T">The enum that every fields have a EnumValueAttribute with each unique value.</typeparam>
		/// <returns></returns>
		public static ModelBuilder ApplyEnumValueConverter<T>(this ModelBuilder modelBuilder) where T : struct, Enum
		{
			var enumType = typeof(T);
			foreach (var entity in modelBuilder.Model.GetEntityTypes())
			foreach (var property in entity.GetProperties())
			{
				if (property?.PropertyInfo?.PropertyType != enumType)
					continue;

				property.SetValueConverter(FromEnum<T>(out var maxLength));
				property.SetMaxLength(maxLength);
			}

			return modelBuilder;
		}

		private static ValueConverter<T, string> FromEnum<T>(out int valueMaxLength) where T : struct, Enum
		{
			var enumToValue = new Dictionary<T, string>();
			var valueToEnum = new Dictionary<string, T>();
			var type = typeof(T);

			foreach (var name in type.GetEnumNames())
			{
				var memberInfo = type.GetMember(name).First(m => m.DeclaringType == type);

				var value = memberInfo.GetCustomAttribute<EnumMemberAttribute>()?.Value
				            ?? throw new InvalidOperationException($"The field({name}) of type({type.Name}) must have a EnumValueAttribute");

				var enumKey = Enum.Parse<T>(name);
				enumToValue[enumKey] = value;

				if (valueToEnum.ContainsKey(value))
					throw new InvalidOperationException($"{value} is must be defined for only one field on enum." +
					                                    $"if you want to use Enum({typeof(T).Name}) as value conversion, All value of EnumValue are unique for each fields.");

				valueToEnum[value] = enumKey;
			}

			valueMaxLength = enumToValue.Values.Max(v => v.Length);
			return new DictionaryValueConverter<T, string>(enumToValue, valueToEnum);
		}
	}
}