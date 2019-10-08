using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EntityFrameworkCore.Convention.Helpers;

namespace EntityFrameworkCore.Convention
{
    public sealed class NamingConvention : INamingConvention
    {
        private static readonly Regex Spliter = new Regex(@"[\s|\-|_]+");

        internal static readonly NamingConvention ForwardCase = new NamingConvention(words => string.Join("", words));

        public static NamingConvention CamelCase = new NamingConvention(StringHelper.ToCamelCase);

        public static NamingConvention LowerSnakeCase = new NamingConvention(StringHelper.ToLowerSnakeCase);

        public static NamingConvention UpperSnakeCase = new NamingConvention(StringHelper.ToUpperSnakeCase);

        public static NamingConvention PascalCase = new NamingConvention(StringHelper.ToPascalCase);

        private readonly Func<string[], string> _joiner;

        internal NamingConvention(Func<string[], string> joiner)
        {
            _joiner = words => joiner(words.SelectMany(StringHelper.SplitWords).ToArray());
        }

        public string Convert(NameMeta nameMeta)
        {
            return _joiner(SplitAsWords(nameMeta).ToArray());
        }

        private static IEnumerable<string> SplitAsWords(NameMeta nameMeta)
        {
	        if (!string.IsNullOrEmpty(nameMeta.Prefix))
				yield return nameMeta.Prefix;

            foreach (var word in Spliter.Split(nameMeta.Name)) yield return word;

            if (!string.IsNullOrEmpty(nameMeta.Suffix))
				yield return nameMeta.Suffix;
        }
    }
}