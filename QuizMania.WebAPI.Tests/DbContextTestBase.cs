using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Data;
using Microsoft.Data.Sqlite;

namespace QuizMania.WebAPI.Tests
{
    public class DbContextTestBase
    {
        private DbContextOptionsBuilder<DatabaseContext> _dbContextOptBuilder;
        private DatabaseContext _dbContext;

        [OneTimeSetUp]
        public void Setup()
        {
            var connection = new SqliteConnection("DataSource = file::memory:?cache = shared");
            connection.Open();

            _dbContextOptBuilder = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite(connection);
            _dbContext = new DatabaseContext(_dbContextOptBuilder.Options);

            DatabaseInitializer.ContextSeederAsync(_dbContext).Wait();
        }

        protected DatabaseContext DbContext => new DatabaseContext(_dbContextOptBuilder.Options);
    }
}
