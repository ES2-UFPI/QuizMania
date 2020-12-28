using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public class CharacterContext : DbContext
    {
        public CharacterContext(DbContextOptions<CharacterContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public void DetachAllEntities()
        {
            ChangeTracker.Clear();
        }

        public DbSet<Character> Characters { get; set; }
    }
}
