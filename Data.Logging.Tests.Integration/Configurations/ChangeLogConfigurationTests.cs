using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xunit;

namespace Data.Logging.Tests.Integration.Configurations
{
    public class ChangeLogConfigurationTests
    {
        [Fact]
        [Trait("Integration", "Sql Server")]
        public async Task Database_should_have_a_table_called_ChangeLogs()
        {
            var databaseName = nameof(Database_should_have_a_table_called_ChangeLogs);
            var connectionString = string.Format(TestConfiguration.ConnectionString, databaseName);
            var options = new DbContextOptionsBuilder<LoggingDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            using (var context = new LoggingDbContext(options))
                await context.Database.EnsureCreatedAsync();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Name From sys.Tables";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var names = new List<string>();
                        while (await reader.ReadAsync())
                            names.Add(reader["Name"] as string);

                        Assert.Contains("ChangeLogs", names);
                    }
                }
            }

            using (var context = new LoggingDbContext(options))
                await context.Database.EnsureDeletedAsync();
        }
        [Fact]
        [Trait("Integration", "Sql Server")]
        public async Task ChangeLogs_should_have_7_columns()
        {
            var databaseName = nameof(ChangeLogs_should_have_7_columns);
            var connectionString = string.Format(TestConfiguration.ConnectionString, databaseName);
            var options = new DbContextOptionsBuilder<LoggingDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            using (var context = new LoggingDbContext(options))
                await context.Database.EnsureCreatedAsync();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
SELECT Count(*)
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'ChangeLogs'";

                    var countColumns = (int)await command.ExecuteScalarAsync();

                    Assert.Equal(7, countColumns);
                }
            }

            using (var context = new LoggingDbContext(options))
                await context.Database.EnsureDeletedAsync();
        }
    }
}
