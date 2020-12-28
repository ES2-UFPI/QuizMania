using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public void DetachAllEntities()
        {
            ChangeTracker.Clear();
        }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuizFeedback> QuizFeedbacks { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers {get; set;}
    }
}
