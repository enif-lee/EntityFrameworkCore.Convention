using System;
using System.Linq;
using EntityFrameworkCore.Convention.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EntityFrameworkCore.Convention
{
    public sealed class ConventionBuilder
    {
        private Func<EntityType, string> _tablePrefix;
        private Func<EntityType, string> _tableSuffix;
        private Func<Property, string> _columnPrefix;
        private INamingConvention _columnNamingConvention;
        private INamingConvention _columnTableNamingConvention;

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
        ///     Setup global table prefix from entity type, but when you use TableAttribute to entity class, this convention will be ignored.
        /// </summary>
        /// <param name="prefix">Return prefix string from entity type, Don't input split character like -_|whitespace</param>
        /// <returns></returns>
        public ConventionBuilder UseGlobalTablePrefix(Func<EntityType, string> prefix)
        {
            _tablePrefix = prefix;
            return this;
        }

        /// <summary>
        ///     Setup global table suffix from entity type, but when you use TableAttribute to entity class, this convention will be ignored.
        /// </summary>
        /// <param name="suffix">Return suffix string from entity type, Don't input split character like -_|whitespace</param>
        /// <returns></returns>
        public ConventionBuilder UseGlobalTableSuffix(Func<EntityType, string> suffix)
        {
            _tableSuffix = suffix;
            return this;
        }

        /// <summary>
        ///     Setup global column suffix converter from Property type, but when you declare ColumnPrefixAttribute to
        ///     entity class, this convention will be ignored.
        /// </summary>
        /// <param name="columnPrefix"></param>
        /// <returns></returns>
        public ConventionBuilder UseGlobalColumnSuffix(Func<Property, string> columnPrefix)
        {
            _columnPrefix = columnPrefix;
            return this;
        }

        /// <summary>
        ///     Setup alphabets of words from entity name global column prefix<br/>
        ///     For example, If entity name is UserDetail, the column prefix is "ud"
        /// </summary>
        /// <param name="alphabetCount"></param>
        /// <returns></returns>
        public ConventionBuilder UseGlobalColumnSuffixAsAlphabetOfEachWordsFromEntityName(int alphabetCount)
        {
            UseGlobalColumnSuffix(property =>
            {
                var chars = property
                    .DeclaringEntityType
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
            _columnNamingConvention = convention;
            return this;
        }

        /// <summary>
        ///     Setup naming strategy how to join words for table name.
        /// </summary>
        /// <param name="convention"></param>
        /// <returns></returns>s
        public ConventionBuilder UseTableNamingConvention(INamingConvention convention)
        {
            _columnTableNamingConvention = convention;
            return this;
        }

        /// <summary>
        ///     Apply
        /// </summary>
        internal void Apply(ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = ProcessTableName(entity);


                foreach (var prop in entity.GetProperties())
                {
                    if (entity.IsOwned() && prop.IsPrimaryKey())
                        continue;

                    prop.Relational().ColumnName = ProcessColumnName(prop);
                }

                foreach (var key in entity.GetKeys())
                {
                }

                foreach (var index in entity.GetIndexes())
                {

                }
            }
        }

        private string ProcessColumnName(IMutableProperty prop)
        {
            IEntityType propOwner = prop.DeclaringEntityType;
            string propName = prop.Name, columnName = null;
            do
            {
                columnName = columnName == null ? propName : propName + "_" + columnName;
                propName = propOwner.DefiningNavigationName;
                propOwner = propOwner.DefiningEntityType;
            }
            while (propName != null);

            throw new NotImplementedException();
        }

        private string ProcessTableName(IMutableEntityType relational)
        {
            throw new NotImplementedException();
        }

        public bool Validate(out string message)
        {
            throw new NotImplementedException();
        }
    }
}