namespace Logic.Common.Interfaces
{
    public interface IConnectionstringService
    {
        (string server, string database) GetConnectionDetails();
    }
}
