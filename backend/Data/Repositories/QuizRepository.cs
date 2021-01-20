using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<QuizFeedback>> GetQuizFbByCharAsync(long id)
        {
            return await _context.QuizFeedbacks.Where(qfb => qfb.Character.Id == id)
                                               .Include(qfb => qfb.Character)
                                               .Include(qfb => qfb.Quiz)
                                               .Include(qfb => qfb.QuestionAnswers)
                                                   .ThenInclude(qa => qa.Question)
                                                        .ThenInclude(q => q.Answers)
                                               .Include(qfb => qfb.QuestionAnswers)
                                                   .ThenInclude(qa => qa.ChosenAnswers)
                                               .ToListAsync();
        }

        public void AddQuiz(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
        }

        public async Task DeleteQuizAsync(Quiz quiz)
        {
            await _context.QuizFeedbacks.Where(qb =>qb.QuizId == quiz.Id)
                                        .Include(qb => qb.QuestionAnswers)
                                        .LoadAsync();
                                        
            _context.Quizzes.Remove(quiz);
        }

        public async Task DeleteQuestionAsync(Question question)
        {
            await _context.QuestionAnswers.Where(qa => qa.QuestionId == question.Id)
                                          .LoadAsync();

            _context.Questions.Remove(question);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}