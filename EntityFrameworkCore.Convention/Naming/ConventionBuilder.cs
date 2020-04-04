using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using EntityFrameworkCore.Convention.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EntityFrameworkCore.Convention.Naming
{
	/// <summary>
	///     Naming convention builder for entity framework core.
	/// </summary>
	public sealed class ConventionBuilder
	{
		[field: NotNull]
		private INamingConvention ColumnNamingConvention { get; set; } = NamingConvention.PascalCase;

		[field: NotNull]
		private INamingConvention IndexNamingConvention { get; set; } = NamingConvention.PascalCase;

		[field: NotNull]
		private INamingConvention KeyNamingConvention { get; set; } = NamingConvention.PascalCase;

		[field: NotNull]
		private INamingConvention TableNamingConvention { get; set; } = NamingConvention.PascalCase;

		private Func<Property, string> _columnPrefix;

		private Func<IEntityType, string> _tablePrefix;

		private Func<IEntityType, string> _tableSuffix;

		internal ConventionBuilder()
		{
		}

		/// <summary>
		///     Setup global table prefix, but when you use TableAttribute to entity class, this convention will be ignored.
		/// </summary>
		/// <param name="prefix">Prefix string, Don't input split character like -_|whitespace</param>
		/// <returns></returns>
		public ConventionBuilder UseGlobalTablePrefix(string prefix)
		{
			_tablePrefix = _ => prefix;
			return this;
		}

		/// <summary>
		///     Setup global table suffix, but when you use TableAttribute to entity class, this convention will be ignored.
		/// </summary>
		/// <param name="suffix">Suffix string, Don't input split character like -_|whitespace</param>
		/// <returns></returns>
		public ConventionBuilder UseGlobalTableSuffix(string suffix)
		{
			_tableSuffix = _ => suffix;
			return this;
		}


		/// <summary>
		///     Setup global table prefix from entity type, but when you use TableAttribute to entity class, this convention will
		///     be ignored.
		/// </summary>
		/// <param name="prefix">Return prefix string from entity type, Don't input split character like -_|whitespace</param>
		/// <returns></returns>
		public ConventionBuilder UseGlobalTablePrefix(Func<IEntityType, string> prefix)
		{
			_tablePrefix = prefix;
			return this;
		}

		/// <summary>
		///     Setup global table suffix from entity type, but when you use TableAttribute to entity class, this convention will
		///     be ignored.
		/// </summary>
		/// <param name="suffix">Return suffix string from entity type, Don't input split character like -_|whitespace</param>
		/// <returns></returns>
		public ConventionBuilder UseGlobalTableSuffix(Func<IEntityType, string> suffix)
		{
			_tableSuffix = suffix;
			return this;
		}

		/// <summary>
		///     Setup global column prefix converter from Property type, but when you declare ColumnPrefixAttribute to
		///     entity class, this convention will be ignored.
		/// </summary>
		/// <param name="columnPrefix"></param>
		/// <returns></returns>
		public ConventionBuilder UseGlobalColumnPrefix(Func<Property, string> columnPrefix)
		{
			_columnPrefix = columnPrefix;
			return this;
		}

		/// <summary>
		///     Setup global column prefix string, but when you declare ColumnPrefixAttribute to
		///     entity class, this convention will be ignored.
		/// </summary>
		/// <param name="columnPrefix">The word will be set as column prefix</param>
		/// <returns></returns>
		public ConventionBuilder UseGlobalColumnPrefix(string columnPrefix)
		{
			_columnPrefix = _ => columnPrefix;
			return this;
		}

		/// <summary>
		///     Setup alphabets of words from entity name global column prefix<br />
		///     For example, If entity name is UserDetail, the column prefix is "ud"
		/// </summary>
		/// <param name="alphabetCount"></param>
		/// <returns></returns>
		public ConventionBuilder UseGlobalColumnPrefixAsAlphabetOfEachWordsFromEntityName(int alphabetCount)
		{
			UseGlobalColumnPrefix(property =>
			{
				var chars = property
					.DeclaringEntityType
					.ClrType
					.Name
					.IgnoreLowercase()
					.Take(alphabetCount)
					.ToArray();
				return new string(chars);
			});
			return this;
		}

		/// <summary>
		///     Setup naming strategy how to join words for column name.
		/// </summary>
		/// <param name="convention"></param>
		/// <returns></returns>
		public ConventionBuilder UseColumnNamingConvention(INamingConvention convention)
		{
			ColumnNamingConvention = convention;
			return this;
		}

		/// <summary>
		///     Setup naming strategy how to join words for table name.
		/// </summary>
		/// <param name="convention"></param>
		/// <returns></returns>
		/// s
		public ConventionBuilder UseTableNamingConvention(INamingConvention convention)
		{
			TableNamingConvention = convention;
			return this;
		}

		/// <summary>
		///     Setup naming strategy how to join words for key constraint.
		/// </summary>
		/// <param name="convention"></param>
		/// <returns></returns>
		/// s
		public ConventionBuilder UseKeyNamingConvention(INamingConvention convention)
		{
			KeyNamingConvention = convention;
			return this;
		}

		/// <summary>
		///     Setup naming strategy how to join words for index name.
		/// </summary>
		/// <param name="convention"></param>
		/// <returns></returns>
		/// s
		public ConventionBuilder UseIndexNamingConvention(INamingConvention convention)
		{
			IndexNamingConvention = convention;
			return this;
		}

		/// <summary>
		///     Setup naming strategy how to join words for all names(table, column, key, index).
		/// </summary>
		/// <param name="convention"></param>
		/// <returns></returns>
		/// s
		public ConventionBuilder UseNamingConvention(INamingConvention convention)
		{
			TableNamingConvention = convention;
			ColumnNamingConvention = convention;
			IndexNamingConvention = convention;
			KeyNamingConvention = convention;
			return this;
		}

		/// <summary>
		/// 
		/// </summary>
		internal void Apply(ModelBuilder builder)
		{
			foreach (var entity in builder.Model.GetEntityTypes())
			{
				if (!entity.IsOwned())
					entity.SetTableName(ProcessTableName(entity));

				foreach (var prop in entity.GetProperties())
				{
					if (entity.IsOwned() && prop.IsPrimaryKey())
						continue;

					prop.SetColumnName(ProcessColumnName(prop));
				}

				foreach (var key in entity.GetKeys())
					key.SetName(KeyNamingConvention.Convert(key.GetName()));


				foreach (var index in entity.GetIndexes())
					index.SetName(IndexNamingConvention.Convert(index.GetName()));
			}
		}

		private string ProcessColumnName(IMutableProperty prop)
		{
			string GetName(IEntityType currentType, string currentName)
			{
				var hasParentType = currentType.DefiningEntityType != null;
				return hasParentType
					? GetName(currentType.DefiningEntityType, currentType.DefiningNavigationName) + "_" + currentName
					: currentName;
			}

			var convention = prop.PropertyInfo.GetCustomAttribute<ColumnConventionAttribute>()
			                 ?? prop.DeclaringEntityType.ClrType.GetCustomAttribute<ColumnConventionAttribute>();

			var name = GetName(prop.DeclaringEntityType, prop.Name);
			return ColumnNamingConvention.Convert(new NameMeta
			{
				Prefix = convention?.Prefix ?? _columnPrefix?.Invoke(prop.AsProperty()),
				Name = name,
				Suffix = convention?.Suffix ?? string.Empty
			});
		}

		private string ProcessTableName(IEntityType relational)
		{
			var convention = relational.ClrType.GetCustomAttribute<TableConventionAttribute>();

			return TableNamingConvention.Convert(new NameMeta
			{
				Prefix = convention?.Prefix ?? _tablePrefix?.Invoke(relational) ?? string.Empty,
				Name =  relational.GetTableName(),
				Suffix = convention?.Suffix ?? _tableSuffix?.Invoke(relational) ?? string.Empty
			});
		}

	}
}