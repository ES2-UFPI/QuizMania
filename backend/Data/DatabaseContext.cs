using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuizFeedback> QuizFeedbacks { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Guild> Guilds { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<ItemInfo> Items { get; set; }
        public DbSet<EffectBase> Effects { get; set; }
    }
}
