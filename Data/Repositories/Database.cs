using Core.Repositories;
using System;
using System.Collections.Immutable;
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
    }
}
