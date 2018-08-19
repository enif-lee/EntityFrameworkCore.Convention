﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EntityFrameworkCore.Convention.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex Spliter = new Regex(@"[\s|\-|_]+");

        public static string ToCamelCase(this string original) => ConvertIfNotEmpty(original, ToCamelCase);

        public static string ToKebobCase(this string original) => ConvertIfNotEmpty(original, ToKebobCase);

        public static string ToSnakeCase(this string original) => ConvertIfNotEmpty(original, ToSnakeCase);

        private static string ConvertIfNotEmpty(string original, Func<IEnumerable<string>, string> processor)
        {
            return string.IsNullOrEmpty(original?.Trim())
                ? string.Empty
                : processor(Spliter.Split(original));
        }

        private static string ToCamelCase(this IEnumerable<string> words)
        {
            var wordsArray = words as string[] ?? words.ToArray();

            return wordsArray.First().ToLower() + string.Join(
                       "",
                       wordsArray.Skip(1).Select(word => word.Substring(0, 1).ToUpper() + word.Substring(1))
                   );
        }

        private static string ToKebobCase(this IEnumerable<string> words)
        {
            return string.Join("-", words.Select(word => word.ToLower()));
        }

        private static string ToSnakeCase(IEnumerable<string> words)
        {
            return string.Join("_", words.Select(word => word.ToLower()));
        }
    }
}