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
            return await _context.Quizzes.Include(o => o.Owner)
                                         .Include(qz => qz.Questions)
                                         .ThenInclude(q => q.Answers)
                                         .ToListAsync();
        }

        public async Task<Quiz> GetQuizAsync(long id)
        {
            return await _context.Quizzes.Include(o => o.Owner)
                                         .Include(qz => qz.Questions)
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

        public void AddQuiz(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
        }

        public void DeleteQuiz(Quiz quiz)
        {
            _context.Quizzes.Remove(quiz);
        }

        public void DeleteQuestion(Question question)
        {
            _context.Questions.Remove(question);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}