namespace Logic.Common.Interfaces
{
    public interface IConnectionstringService
    {
        (string server, string database) GetConnectionDetails();
        void SaveConnectionDetails((string Server, string Database) details);
    }
}
