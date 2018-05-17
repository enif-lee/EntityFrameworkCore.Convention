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

        [TestMethod]
        [DataRow("snake_case", "snake-case")]
        [DataRow("kebob-case", "kebob-case")]
        [DataRow("Title Case", "title-case")]
        [DataRow("Title     Case", "title-case")]
        [DataRow("      ", "")]
        public void ToKebobCaseTest(string original, string expected)
        {
            original.ToKebobCase().Should().Be(expected);
        }

        [TestMethod]
        [DataRow("snake_case", "snake_case")]
        [DataRow("kebob-case", "kebob_case")]
        [DataRow("Title Case", "title_case")]
        [DataRow("Title     Case", "title_case")]
        [DataRow("      ", "")]
        public void ToSnakeCaseTest(string original, string expected)
        {
            original.ToSnakeCase().Should().Be(expected);
        }
    }
}