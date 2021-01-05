using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Data;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public class QuizRepository : IQuizAsyncRepository
    {
        private readonly DatabaseContext _context;

        public QuizRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Quiz>> GetAllQuizzesAsync()
        {
            return await _context.Quizzes.Include(qz => qz.Questions)
                                         .ThenInclude(q => q.Answers)
                                         .ToListAsync();
        }

        public async Task<Quiz> GetQuizAsync(long id)
        {
            return await _context.Quizzes.Include(qz => qz.Questions)
                                         .ThenInclude(q => q.Answers)
                                         .FirstOrDefaultAsync(qz => qz.Id == id);
        }

        public async Task<Question> GetQuestionAsync(long id)
        {
            return await _context.Questions.Include(q => q.Answers)
                                           .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Answer> GetAnswerAsync(long id)
        {
            return await _context.Answers.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}