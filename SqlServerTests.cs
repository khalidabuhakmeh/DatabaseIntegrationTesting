using System;
using System.Data.SqlClient;
using ThrowawayDb;
using Xunit;
using Xunit.Abstractions;

namespace DatabaseIntegrationTesting
{
    public class SqlServerTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public SqlServerTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        private static class Settings
        {
            public const string Username = "sa";
            public const string Password = "Pass123!";
            public const string Host = "localhost,11433";
        }
        
        [Fact]
        public void Can_Select_1_From_Database()
        {
            using var database = ThrowawayDatabase.Create(
                Settings.Username, 
                Settings.Password, 
                Settings.Host
            );
            
            testOutputHelper.WriteLine($"Created database {database.Name}");

            using var connection = new SqlConnection(database.ConnectionString);
            connection.Open();
            using var cmd = new SqlCommand("SELECT 1", connection);
            var result = Convert.ToInt32(cmd.ExecuteScalar());
            
            testOutputHelper.WriteLine(result.ToString());
            
            Assert.Equal(1, result);
        }
    }
}
