using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EntityFrameworkCore.Convention.Test.Vendor
{
    public class TestBaseTest : TestBase
    {
        [Test]
        public void InitializeTest()
        {
            Context.Companies.Should().HaveCount(1);
            Context.Users.Should().HaveCount(1);
        }

        [Test]
        public void CommandTest()
        {
            using (var connection = Context.Database.GetDbConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT name FROM sqlite_master WHERE type='table'";
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TestContext.WriteLine(reader[0]);
                }
            }
        }
    }
}