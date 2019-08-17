using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using EntityFrameworkCore.Convention.Extensions;

namespace EntityFrameworkCore.Convention
{
    public sealed class NamingConvention : INamingConvention
    {
        private static readonly Regex Spliter = new Regex(@"[\s|\-|_]+");

        public static NamingConvention CamelCase = new NamingConvention(StringExtensions.ToCamelCase);
        public static NamingConvention LowerSnakeCase = new NamingConvention(StringExtensions.ToLowerSnakeCase);
        public static NamingConvention UpperSnakeCase = new NamingConvention(StringExtensions.ToUpperSnakeCase);
        private readonly Func<string[], string> _joiner;

        internal NamingConvention(Func<string[], string> joiner)
        {
            _joiner = joiner;
        }

        public string Convert(NameMeta nameMeta)
        {
            return _joiner(SplitAsWords(nameMeta).ToArray());
        }

        private static IEnumerable<string> SplitAsWords(NameMeta nameMeta)
        {
            yield return nameMeta.Prefix;

            foreach (var word in Spliter.Split(nameMeta.Name)) yield return word;

            yield return nameMeta.Suffix;
        }
    }
}