using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Data.Tests.Integration.Configurations
{
    public class CategoryConfigurationTests
    {
        [Fact]
        [Trait("Integration", "Sql Server")]
        public async Task Database_should_have_a_table_called_Categories()
        {
            var databaseName = nameof(Database_should_have_a_table_called_Categories);
            var connectionString = string.Format(TestConfiguration.ConnectionString, databaseName);
            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            using (var context = new RunnerDbContext(options))
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

                        Assert.Contains("Categories", names);
                    }
                }
            }

            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureDeletedAsync();
        }
        [Fact]
        [Trait("Integration", "Sql Server")]
        public async Task Categories_should_have_4_columns()
        {
            var databaseName = nameof(Categories_should_have_4_columns);
            var connectionString = string.Format(TestConfiguration.ConnectionString, databaseName);
            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureCreatedAsync();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
SELECT Count(*)
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Categories'";

                    var countColumns = (int)await command.ExecuteScalarAsync();

                    Assert.Equal(4, countColumns);
                }
            }

            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureDeletedAsync();
        }
        [Fact]
        [Trait("Integration", "Sql Server")]
        public async Task Categories_should_have_a_not_nullable_column_called_CategoryId_of_type_Integer()
        {
            var databaseName = nameof(Categories_should_have_a_not_nullable_column_called_CategoryId_of_type_Integer);
            var connectionString = string.Format(TestConfiguration.ConnectionString, databaseName);
            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureCreatedAsync();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
SELECT COLUMN_NAME
    , DATA_TYPE
    , IS_NULLABLE
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Categories'";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var results = new List<(string columnName, string typeName, bool isNullable)>();
                        while (await reader.ReadAsync())
                        {
                            var columnName = reader["COLUMN_NAME"] as string;
                            var typeName = reader["DATA_TYPE"] as string;
                            var isNullable = (reader["IS_NULLABLE"] as string).Equals("YES", StringComparison.InvariantCultureIgnoreCase);
                            results.Add((columnName, typeName, isNullable));
                        }
                        var containsWantedColum = results.Any(r => r.columnName.Equals("CategoryId", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   r.typeName.Equals("Int", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   !r.isNullable);

                        Assert.True(containsWantedColum);
                    }
                }
            }

            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureDeletedAsync();
        }
        [Fact]
        [Trait("Integration", "Sql Server")]
        public async Task Categories_should_have_a_not_nullable_column_called_Name_of_type_nvarchar_of_50()
        {
            var databaseName = nameof(Categories_should_have_a_not_nullable_column_called_Name_of_type_nvarchar_of_50);
            var connectionString = string.Format(TestConfiguration.ConnectionString, databaseName);
            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureCreatedAsync();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
SELECT COLUMN_NAME
    , DATA_TYPE
    , CHARACTER_MAXIMUM_LENGTH
    , IS_NULLABLE
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Categories'";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var results = new List<(string columnName, string typeName, int? maxCharacters, bool isNullable)>();
                        while (await reader.ReadAsync())
                        {
                            var columnName = reader["COLUMN_NAME"] as string;
                            var typeName = reader["DATA_TYPE"] as string;
                            var isNullable = (reader["IS_NULLABLE"] as string).Equals("YES", StringComparison.InvariantCultureIgnoreCase);
                            var maxCharactersResult = reader["CHARACTER_MAXIMUM_LENGTH"];

                            int? maxCharacters = null;
                            if (!(maxCharactersResult is DBNull))
                                maxCharacters = (int)maxCharactersResult;

                            results.Add((columnName, typeName, maxCharacters, isNullable));
                        }

                        var containsWantedColum = results.Any(r => r.columnName.Equals("Name", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   r.typeName.Equals("nvarchar", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   !r.isNullable &&
                                                                   r.maxCharacters == 50);

                        Assert.True(containsWantedColum);
                    }
                }
            }

            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureDeletedAsync();
        }
        [Fact]
        [Trait("Integration", "Sql Server")]
        public async Task Categories_should_have_a_nullable_column_called_Starttime_of_type_datetimte2()
        {
            var databaseName = nameof(Categories_should_have_a_nullable_column_called_Starttime_of_type_datetimte2);
            var connectionString = string.Format(TestConfiguration.ConnectionString, databaseName);
            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureCreatedAsync();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
SELECT COLUMN_NAME
    , DATA_TYPE
    , IS_NULLABLE
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Categories'";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var results = new List<(string columnName, string typeName, bool isNullable)>();
                        while (await reader.ReadAsync())
                        {
                            var columnName = reader["COLUMN_NAME"] as string;
                            var typeName = reader["DATA_TYPE"] as string;
                            var isNullable = (reader["IS_NULLABLE"] as string).Equals("YES", StringComparison.InvariantCultureIgnoreCase);

                            results.Add((columnName, typeName, isNullable));
                        }

                        var containsWantedColum = results.Any(r => r.columnName.Equals("Starttime", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   r.typeName.Equals("datetime2", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   r.isNullable);

                        Assert.True(containsWantedColum);
                    }
                }
            }

            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureDeletedAsync();
        }
        [Fact]
        [Trait("Integration", "Sql Server")]
        public async Task Categories_should_have_a_not_nullable_column_called_PlannedStarttime_of_type_datetimte2()
        {
            var databaseName = nameof(Categories_should_have_a_not_nullable_column_called_PlannedStarttime_of_type_datetimte2);
            var connectionString = string.Format(TestConfiguration.ConnectionString, databaseName);
            var options = new DbContextOptionsBuilder<RunnerDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureCreatedAsync();

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
SELECT COLUMN_NAME
    , DATA_TYPE
    , IS_NULLABLE
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Categories'";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var results = new List<(string columnName, string typeName, bool isNullable)>();
                        while (await reader.ReadAsync())
                        {
                            var columnName = reader["COLUMN_NAME"] as string;
                            var typeName = reader["DATA_TYPE"] as string;
                            var isNullable = (reader["IS_NULLABLE"] as string).Equals("YES", StringComparison.InvariantCultureIgnoreCase);

                            results.Add((columnName, typeName, isNullable));
                        }

                        var containsWantedColum = results.Any(r => r.columnName.Equals("PlannedStarttime", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   r.typeName.Equals("datetime2", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   !r.isNullable);

                        Assert.True(containsWantedColum);
                    }
                }
            }

            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureDeletedAsync();
        }
    }
}