using Logic.Common.Interfaces;
using System;
using System.Linq;

namespace Logic.Common.Services
{
    public class ConnectionstringService : IConnectionstringService
    {
        private readonly Func<string> getConnectionstring;
        private readonly Action<string> saveConnectionstring;

        public ConnectionstringService(Func<string> getConnectionstring, Action<string> saveConnectionstring)
        {
            this.getConnectionstring = getConnectionstring ?? throw new ArgumentNullException(nameof(getConnectionstring));
            this.saveConnectionstring = saveConnectionstring ?? throw new ArgumentNullException(nameof(saveConnectionstring));
        }

        public (string server, string database) GetConnectionDetails()
        {
            var connectionstringDetails = getConnectionstring().ToLower().Split(';');
            var server = connectionstringDetails.SingleOrDefault(d => d.StartsWith("server=") || d.StartsWith("data source="));
            var database = connectionstringDetails.SingleOrDefault(d => d.StartsWith("database=") || d.StartsWith("initial catalog="));

            return (
                server.Substring(server.IndexOf('=') + 1),
                database.Substring(database.IndexOf('=') + 1)
                );
        }
    }
}
