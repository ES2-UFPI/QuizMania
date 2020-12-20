using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public class SqlQuizRepository : IQuizAsyncRepository
    {
        private readonly QuizContext _context;

        public SqlQuizRepository(QuizContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Quiz>> GetAllQuizzesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Quiz> GetQuizAsync(long id)
        {
            return await _context.Quizzes.FindAsync(id);
        }

        public Task<Choice> GetChoiceAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Question> GetQuestionAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void SaveQuizFeedback(QuizFeedback quizFeedback)
        {
            throw new NotImplementedException();
        }
    }
}
