﻿using System.Collections.Generic;
using System.Threading.Tasks;
using QuizMania.WebAPI.Models;

namespace QuizMania.WebAPI
{
    public interface IQuizAsyncRepository
    {
        Task<IEnumerable<Quiz>> GetAllQuizzesAsync();
        Task<Quiz> GetQuizAsync(long id);
        Task<bool> DeleteQuizAsync(long id);
        Task<Question> GetQuestionAsync(long id);
        Task<Answer> GetAnswerAsync(long id);
        public void AddQuiz(Quiz quiz);
        Task<int> SaveChangesAsync();
    }
}
