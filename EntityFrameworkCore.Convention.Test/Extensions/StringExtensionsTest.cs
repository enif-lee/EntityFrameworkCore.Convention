using EntityFrameworkCore.Convention.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace EntityFrameworkCore.Convention.Test.Extensions
{
    public class StringExtensionsTest
    {
        [Test]
        [TestCase("snake_case", "snakeCase")]
        [TestCase("kebob-case", "kebobCase")]
        [TestCase("Title Case", "titleCase")]
        [TestCase("Title     Case", "titleCase")]
        [TestCase("      ", "")]
        public void ToCamelCaseTest(string original, string expected)
        {
            original.ToCamelCase().Should().Be(expected);
        }

        [Test]
        [TestCase("snake_case", "snake-case")]
        [TestCase("kebob-case", "kebob-case")]
        [TestCase("Title Case", "title-case")]
        [TestCase("Title     Case", "title-case")]
        [TestCase("      ", "")]
        public void ToKebobCaseTest(string original, string expected)
        {
            original.ToKebobCase().Should().Be(expected);
        }

        [Test]
        [TestCase("snake_case", "snake_case")]
        [TestCase("kebob-case", "kebob_case")]
        [TestCase("Title Case", "title_case")]
        [TestCase("Title     Case", "title_case")]
        [TestCase("      ", "")]
        public void ToSnakeCaseTest(string original, string expected)
        {
            original.ToSnakeCase().Should().Be(expected);
        }
    }
}