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


        public async Task<IEnumerable<Quiz>> GetAllQuizzesAsync()
        {
            return await _context.Quizzes.ToListAsync();
        }

        public async Task<Quiz> GetQuizAsync(long id)
        {
            return await _context.Quizzes.FindAsync(id);
        }
    }
}
