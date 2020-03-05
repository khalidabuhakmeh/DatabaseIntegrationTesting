using System;
using Npgsql;
using ThrowawayDb.Postgres;
using Xunit;
using Xunit.Abstractions;

namespace DatabaseIntegrationTesting
{
    public class PostgreSqlTests
    {
        private static class Settings
        {
            public const string Username = "postgres";
            public const string Password = "Pass123!";
            public const string Host = "localhost";
        }
        
        private readonly ITestOutputHelper testOutputHelper;
        
        public PostgreSqlTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
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
            using var connection = new NpgsqlConnection(database.ConnectionString);
            connection.Open();
                
            using var cmd = new NpgsqlCommand("SELECT 1", connection);
            var result = Convert.ToInt32(cmd.ExecuteScalar());
            
            Assert.Equal(1, result); 
            
            testOutputHelper.WriteLine(result.ToString());
        }
    }
}