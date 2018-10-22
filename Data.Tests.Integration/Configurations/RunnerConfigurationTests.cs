using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Data.Tests.Integration.Configurations
{
    public class RunnerConfigurationTests
    {
        [Fact]
        [Trait("Integration", "Sql Server")]
        public async Task Database_should_have_a_table_called_Runners()
        {
            var databaseName = nameof(Database_should_have_a_table_called_Runners);
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

                        Assert.Contains("Runners", names);
                    }
                }
            }

            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureDeletedAsync();
        }

        [Fact]
        [Trait("Integration", "Sql Server")]
        public async Task Runners_should_have_13_columns()
        {
            var databaseName = nameof(Runners_should_have_13_columns);
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
    WHERE TABLE_NAME = 'Runners'";

                    var countColumns = (int)await command.ExecuteScalarAsync();

                    Assert.Equal(13, countColumns);
                }
            }

            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureDeletedAsync();
        }
        [Fact]
        [Trait("Integration", "Sql Server")]
        public async Task Runners_should_have_a_not_nullable_column_called_RunnerId_of_type_Integer()
        {
            var databaseName = nameof(Runners_should_have_a_not_nullable_column_called_RunnerId_of_type_Integer);
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
    WHERE TABLE_NAME = 'Runners'";

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
                        var containsWantedColum = results.Any(r => r.columnName.Equals("RunnerId", StringComparison.InvariantCultureIgnoreCase) &&
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
        public async Task Runners_should_have_a_nullable_column_called_Startnumber_of_type_Integer()
        {
            var databaseName = nameof(Runners_should_have_a_nullable_column_called_Startnumber_of_type_Integer);
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
    WHERE TABLE_NAME = 'Runners'";

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
                        var containsWantedColum = results.Any(r => r.columnName.Equals("Startnumber", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   r.typeName.Equals("Int", StringComparison.InvariantCultureIgnoreCase) &&
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
        public async Task Runners_should_have_a_not_nullable_column_called_Firstname_of_type_nvarchar_of_50()
        {
            var databaseName = nameof(Runners_should_have_a_not_nullable_column_called_Firstname_of_type_nvarchar_of_50);
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
    WHERE TABLE_NAME = 'Runners'";

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

                        var containsWantedColum = results.Any(r => r.columnName.Equals("Firstname", StringComparison.InvariantCultureIgnoreCase) &&
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
        public async Task Runners_should_have_a_not_nullable_column_called_Lastname_of_type_nvarchar_of_50()
        {
            var databaseName = nameof(Runners_should_have_a_not_nullable_column_called_Lastname_of_type_nvarchar_of_50);
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
    WHERE TABLE_NAME = 'Runners'";

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

                        var containsWantedColum = results.Any(r => r.columnName.Equals("Lastname", StringComparison.InvariantCultureIgnoreCase) &&
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
        public async Task Runners_should_have_a_nullable_column_called_Sportsclub_of_type_nvarchar_of_200()
        {
            var databaseName = nameof(Runners_should_have_a_nullable_column_called_Sportsclub_of_type_nvarchar_of_200);
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
    WHERE TABLE_NAME = 'Runners'";

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

                        var containsWantedColum = results.Any(r => r.columnName.Equals("Sportsclub", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   r.typeName.Equals("nvarchar", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   r.isNullable &&
                                                                   r.maxCharacters == 200);

                        Assert.True(containsWantedColum);
                    }
                }
            }

            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureDeletedAsync();
        }
        [Fact]
        [Trait("Integration", "Sql Server")]
        public async Task Runners_should_have_a_not_nullable_column_called_YearOfBirth_of_type_Integer()
        {
            var databaseName = nameof(Runners_should_have_a_not_nullable_column_called_YearOfBirth_of_type_Integer);
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
    WHERE TABLE_NAME = 'Runners'";

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
                        var containsWantedColum = results.Any(r => r.columnName.Equals("YearOfBirth", StringComparison.InvariantCultureIgnoreCase) &&
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
        public async Task Runners_should_have_a_nullable_column_called_ChipId_of_type_nchar_of_10()
        {
            var databaseName = nameof(Runners_should_have_a_nullable_column_called_ChipId_of_type_nchar_of_10);
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
    WHERE TABLE_NAME = 'Runners'";

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

                        var containsWantedColum = results.Any(r => r.columnName.Equals("ChipId", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   r.typeName.Equals("varchar", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   r.isNullable &&
                                                                   r.maxCharacters == 10);

                        Assert.True(containsWantedColum);
                    }
                }
            }

            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureDeletedAsync();
        }
        [Fact]
        [Trait("Integration", "Sql Server")]
        public async Task Runners_should_have_a_nullable_column_called_TimeAtDestination_of_type_datetimte2()
        {
            var databaseName = nameof(Runners_should_have_a_nullable_column_called_TimeAtDestination_of_type_datetimte2);
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
    WHERE TABLE_NAME = 'Runners'";

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

                        var containsWantedColum = results.Any(r => r.columnName.Equals("TimeAtDestination", StringComparison.InvariantCultureIgnoreCase) &&
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
        public async Task Runners_should_have_a_nullable_column_called_RunningTime_of_type_time()
        {
            var databaseName = nameof(Runners_should_have_a_nullable_column_called_RunningTime_of_type_time);
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
    WHERE TABLE_NAME = 'Runners'";

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

                        var containsWantedColum = results.Any(r => r.columnName.Equals("RunningTime", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   r.typeName.Equals("time", StringComparison.InvariantCultureIgnoreCase) &&
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
        public async Task Runners_should_have_a_not_nullable_column_called_Gender_of_type_tinyint()
        {
            var databaseName = nameof(Runners_should_have_a_not_nullable_column_called_Gender_of_type_tinyint);
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
    WHERE TABLE_NAME = 'Runners'";

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

                        var containsWantedColum = results.Any(r => r.columnName.Equals("Gender", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   r.typeName.Equals("tinyint", StringComparison.InvariantCultureIgnoreCase) &&
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
        public async Task Runners_should_have_a_nullable_column_called_City_of_type_nvarchar_of_50()
        {
            var databaseName = nameof(Runners_should_have_a_nullable_column_called_City_of_type_nvarchar_of_50);
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
    WHERE TABLE_NAME = 'Runners'";

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

                        var containsWantedColum = results.Any(r => r.columnName.Equals("City", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   r.typeName.Equals("nvarchar", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   r.isNullable &&
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
        public async Task Runners_should_have_a_nullable_column_called_Email_of_type_nvarchar_of_50()
        {
            var databaseName = nameof(Runners_should_have_a_nullable_column_called_Email_of_type_nvarchar_of_50);
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
    WHERE TABLE_NAME = 'Runners'";

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

                        var containsWantedColum = results.Any(r => r.columnName.Equals("Email", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   r.typeName.Equals("nvarchar", StringComparison.InvariantCultureIgnoreCase) &&
                                                                   r.isNullable &&
                                                                   r.maxCharacters == 50);

                        Assert.True(containsWantedColum);
                    }
                }
            }

            using (var context = new RunnerDbContext(options))
                await context.Database.EnsureDeletedAsync();
        }
    }
}
