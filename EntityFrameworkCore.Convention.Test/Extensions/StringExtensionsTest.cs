using EntityFrameworkCore.Convention.Extensions;
using FluentAssertions;
using NUnit.Framework;

namespace EntityFrameworkCore.Convention.Test.Extensions
{
    public class StringExtensionsTest
    {
        private readonly string[] _words = {"i", "love", "you", "forever"};

        [Test]
        public void ToCamelCaseTest()
        {
            StringHelper.ToCamelCase(_words).Should().Be("iLoveYouForever");
        }

        [Test]
        public void ToLowerSnakeCaseTest()
        {
            StringHelper.ToLowerSnakeCase(_words).Should().Be("i_love_you_forever");
        }

        [Test]
        public void ToUpperSnakeCaseTest()
        {
            StringHelper.ToUpperSnakeCase(_words).Should().Be("I_LOVE_YOU_FOREVER");
        }
    }
}