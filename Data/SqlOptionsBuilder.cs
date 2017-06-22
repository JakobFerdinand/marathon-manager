using Microsoft.EntityFrameworkCore;

namespace Data
{
    public static class SqlOptionsBuilder
    {
        public static DbContextOptions<RunnersContext> GetOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RunnersContext>();
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MarathonManager;Integrated Security=True");

            return optionsBuilder.Options;
        }
    }
}
