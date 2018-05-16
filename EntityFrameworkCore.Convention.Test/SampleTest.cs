using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntityFrameworkCore.Convention.Test
{
    [TestClass]
    public class SampleTest
    {

        [TestMethod]
        public void OnePlusOneIsTwo()
        {
            (1 + 1).Should().Be(2);
        }
        
    }
}