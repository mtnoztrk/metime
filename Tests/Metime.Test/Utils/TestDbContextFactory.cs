using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Metime.Test.Utils
{
    public class TestDataContextFactory
    {
        public TestDataContextFactory()
        {
            var builder = new DbContextOptionsBuilder<TestDbContext>();
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            builder.UseSqlite(connection);

            using (var ctx = new TestDbContext(builder.Options))
            {
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();
            }

            _options = builder.Options;
        }

        private readonly DbContextOptions<TestDbContext> _options;

        public TestDbContext Create() => new TestDbContext(_options);
    }
}
