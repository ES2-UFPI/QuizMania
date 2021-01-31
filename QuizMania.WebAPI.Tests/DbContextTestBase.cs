using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Data;
using QuizMania.WebAPI.Profiles;
using Microsoft.Data.Sqlite;
using AutoMapper;

namespace QuizMania.WebAPI.Tests {
    [TestFixture]
    public class DbContextTestBase {
        private DbContextOptionsBuilder<DatabaseContext> _dbContextOptBuilder;
        private DatabaseContext                          _dbContext;
        private IMapper                                  _mapper;

        protected DatabaseContext DbContext => _dbContext;
        protected IMapper Mapper => _mapper;

        [OneTimeSetUp]
        public void Setup() {
            var connection = new SqliteConnection("DataSource = file::memory:?cache = shared");
            connection.Open();

            _dbContextOptBuilder = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite(connection);
            _dbContext           = new DatabaseContext(_dbContextOptBuilder.Options);

            DatabaseInitializer.ContextSeederAsync(_dbContext).Wait();

            if (_mapper == null) {
                _mapper = new MapperConfiguration(mc => { mc.AddMaps(typeof(CharacterProfile).Assembly); }).CreateMapper();
            }
        }

        protected DatabaseContext GetUniqueDatabaseContext(string databaseName)
        {
            return new DatabaseContext(new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(databaseName).Options);
        }
    }
}