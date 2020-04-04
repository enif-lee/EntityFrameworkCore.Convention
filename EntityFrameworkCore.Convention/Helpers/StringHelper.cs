using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EntityFrameworkCore.Convention.Helpers
{
    internal static class StringHelper
    {
        private static readonly Regex Splitter = new Regex(@"[\s|\-|_]+");

        public static IEnumerable<string> SplitWords(string original)
        {
	        var trimmed = original?.Trim();
	        return string.IsNullOrEmpty(trimmed)
                ? Enumerable.Empty<string>()
                : Splitter.Split(Regex.Replace(trimmed, @"([a-z0-9])([A-Z])", "$1_$2").ToLower());
        }

        internal static string ToCamelCase(IEnumerable<string> words)
        {
            var wordsArray = words as string[] ?? words.ToArray();

            return wordsArray.First().ToLower() + string.Join(
                       string.Empty,
                       wordsArray.Skip(1).Select(word => word.Substring(0, 1).ToUpper() + word.Substring(1))
                   );
        }

        internal static string ToPascalCase(IEnumerable<string> words)
        {
            return string.Join(string.Empty, words.Select(word => word.Substring(0, 1).ToUpper() + word.Substring(1)));
        }

        internal static string ToLowerSnakeCase(IEnumerable<string> words)
        {
            return string.Join("_", words.Select(word => word.ToLower()));
        }

        internal static string ToUpperSnakeCase(IEnumerable<string> words)
        {
            return string.Join("_", words.Select(word => word.ToUpper()));
        }

        private static bool IsVowel(this char c)
        {
            return "aeiouAEIOU".Contains(c);
        }

        internal static bool IsConsonant(this char c)
        {
            return !c.IsVowel();
        }

        internal static string IgnoreLowercase(this string str)
        {
            return new string(str.Where(char.IsUpper).ToArray());
        }
    }
}