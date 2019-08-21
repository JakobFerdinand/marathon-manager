using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class Database : IDatabase
    {
        private readonly RunnerDbContext context;

        public Database(RunnerDbContext context)
            => this.context = context ?? throw new System.ArgumentNullException(nameof(context));

        public async Task<bool> CanConnectAsync()
        {
            try
            {
                return await context.Database.CanConnectAsync();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ImmutableArray<string> GetAllDatabases(string server)
        {
            try
            {
                using (var connnection = new SqlConnection($"Server={server};Database=master;Trusted_Connection=True"))
                using (var command = connnection.CreateCommand())
                {
                    command.CommandText = @"
Select name 
from master.dbo.sysdatabases
where dbid > 4
";

                    connnection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        var result = new List<string>();
                        while (reader.Read())
                            result.Add(reader["name"] as string);
                        return result.ToImmutableArray();
                    }
                }
            }
            catch (Exception)
            {
                return ImmutableArray.Create<string>();
            }
        }

        public ImmutableArray<string> GetAvailableServers()
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = "cmd.exe",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        Arguments = "/C sqlcmd -L"
                    }
                };
                process.Start();
                var stream = process.StandardOutput;

                return stream
                    .ReadAllLines()
                    .Skip(2)
                    .Select(l => l.Trim())
                    .ToImmutableArray();
            }
            catch (Exception)
            {
                return ImmutableArray.Create<string>();
            }
        }

        public bool IsServerOnline(string serverName)
        {
            try
            {
                using (var connection = new SqlConnection($"Server={serverName};Database=master;Trusted_Connection=true;Connection Timeout=5"))
                    connection.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
