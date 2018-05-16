using EntityFrameworkCore.Convention.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntityFrameworkCore.Convention.Test.Extensions
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        [DataRow("snake_case", "snakeCase")]
        [DataRow("kebob-case", "kebobCase")]
        [DataRow("Title Case", "titleCase")]
        [DataRow("Title     Case", "titleCase")]
        [DataRow("      ", "")]
        public void ToCamelCaseTest(string original, string expected)
        {
            original.ToCamelCase().Should().Be(expected);
        }
    }
}