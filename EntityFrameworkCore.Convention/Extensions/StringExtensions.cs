using System.Linq;
using System.Text.RegularExpressions;

namespace EntityFrameworkCore.Convention.Extensions
{
    public static class StringExtensions
    {
        private static Regex _regex = new Regex(@"[\s|\-|_]+");

        public static string ToCamelCase(this string original)
        {
            if (string.IsNullOrEmpty(original?.Trim()))
                return string.Empty;

            var words = _regex.Split(original);

            return words[0].ToLower() + string.Join(
                       "",
                       words.Skip(1).Select(word => word.Substring(0, 1).ToUpper() + word.Substring(1))
                   );
        }
    }
}