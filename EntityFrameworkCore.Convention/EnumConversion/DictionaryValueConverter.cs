using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntityFrameworkCore.Convention.EnumConversion
{
	internal class DictionaryValueConverter<TKey, TValue> : ValueConverter<TKey, TValue>
	{
		public DictionaryValueConverter(IDictionary<TKey, TValue> keyToValue, IDictionary<TValue, TKey> valueToKey)
			: base(key => keyToValue[key], value => valueToKey[value])
		{
		}
	}
}