using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public class SqlQuizRepository : IQuizAsyncRepository
    {
        private readonly QuizContext _context;

        public SqlQuizRepository(QuizContext context) {
            _context = context;
        }


        public async Task<ActionResult<IEnumerable<Quiz>>> GetAllQuizzesAsync()
        {
            return await _context.Quizzes.ToListAsync();
        }

        public async Task<ActionResult<Quiz>> GetQuizAsync(long id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);

            if (quiz == null)
            {
                return new NotFoundResult();
            }

            return quiz;
        }
    }
}
